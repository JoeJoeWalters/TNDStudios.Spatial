using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;
using TNDStudios.Spatial.Helpers;

/// <summary>
/// https://www8.garmin.com/xmlschemas/TrainingCenterDatabasev2.xsd
/// </summary>
namespace TNDStudios.Spatial.Documents
{
    /// <summary>
    /// Implementation of https://www8.garmin.com/xmlschemas/TrainingCenterDatabasev2.xsd
    /// Folders, workouts, courses element(s) not mapped because we only care about the core activity data we can extract right now
    /// </summary>
    [Serializable]
    [XmlRoot("TrainingCenterDatabase", Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public class TCXFile : XmlBase, IGeoFileConvertable
    {
        // ISO 8601 formatter instead of using roundtrip kind parsing as needed for read and write (get and set)
        public static String DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";

        [XmlElement("Activities")]
        public TCXActivities Activities { get; set; }

        [XmlElement("Author")]
        public TCXAbstractSource Author { get; set; }

        [XmlElement("Extensions")]
        public TCXExtensions Extensions { get; set; }

        public GeoFile ToGeoFile()
        {
            GeoFile result = new GeoFile();

            // Transform the activity to the route information
            result.Routes = this.Activities.Activity.Select(activity => new GeoFileRoute() { Name = activity.Id, Points = activity.ToCoords() }).ToList();

            return result;
        }

        /// <summary>
        /// Convert a GeoFile to the native format
        /// </summary>
        /// <param name="file">The GeoFile format to convert from</param>
        /// <returns>Success Or Failure flag</returns>
        public Boolean FromGeoFile(GeoFile file) => throw new NotImplementedException();
    }
}