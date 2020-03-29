using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TNDStudios.Spatial.Tests
{
    public class TestBase
    {
        /// <summary>
        /// Load data from an embedded resource to use for testing
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public String GetEmbeddedResource(String path)
        {
            // Get the current assembly information
            var assembly = typeof(TestBase).GetTypeInfo().Assembly;

            // Calculate the path to the resource in the assembly and 
            // fix any directory slashes
            path = $"{assembly.GetName().Name}/{path}".Replace("/", ".");

            // Load the resource stream from the assembly
            try
            {
                Stream resource = assembly.GetManifestResourceStream(path);
                if (resource == null)
                    throw new Exception("No resource found");

                using (TextReader reader = new StreamReader(resource))
                {
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                // A forced or unforced error occoured, return nothing ..
                return String.Empty;
            }
        }

        /// <summary>
        /// Gte embedded test data and map it to an object using the XML (de)serialiser
        /// </summary>
        /// <typeparam name="T">The type of object to create and map in to</typeparam>
        /// <param name="path">THe path to the resource in the assembly</param>
        /// <returns>An object of the correct type</returns>
        public T GetXMLData<T>(String path)
        {
            try
            {
                // Get a string representing the XML from the embedded resource
                String data = GetEmbeddedResource(path);
                if (data == null)
                    throw new Exception("No data");

                // Load the XML in to the object required 
                // If tagged mappings exist they should be mapped
                StringReader strReader = new StringReader(data);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextReader xmlReader = new XmlTextReader(strReader);

                return (T)serializer.Deserialize(xmlReader);
            }
            catch(Exception ex)
            {
                // Failure in getting data return default of T which will be 
                // empty causing the tests to fail anyway
                return default(T);
            }
        }
    }
}
