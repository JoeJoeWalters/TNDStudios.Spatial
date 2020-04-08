using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace TNDStudios.Spatial.Helpers
{
    public static class XmlHelper
    {
        public static String CleanTags = @"xsi:type=\""[a-zA-Z0-9:;\.\s\(\)\-_\""\,]*";

        /// <summary>
        /// Clean down tags we need to get rid of such as xsi:type for complex polymorphics such as device_t and application_t in TCX files
        /// </summary>
        /// <param name="value">The origional XML</param>
        /// <returns>The cleaned XML</returns>
        public static String CleanXML(this String value)
            => Regex.Replace(value, CleanTags, String.Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public static T DeserialiseXML<T>(String data)
        {
            try
            {
                // Clean down tags we need to get rid of such as xsi:type for complex polymorphics such as device_t and application_t in TCX files
                data = data.CleanXML();

                // Load the XML in to the object required 
                // If tagged mappings exist they should be mapped
                StringReader strReader = new StringReader(data);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextReader xmlReader = new XmlTextReader(strReader);

                return (T)serializer.Deserialize(xmlReader);
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
