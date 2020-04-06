using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;
using TNDStudios.Spatial.Helpers;

/// <summary>
/// https://www8.garmin.com/Sxmlschemas/TrainingCenterDatabasev2.xsd
/// </summary>
namespace TNDStudios.Spatial.Documents
{
    /// <summary>
    /// Implementation of https://www8.garmin.com/Sxmlschemas/TrainingCenterDatabasev2.xsd
    /// </summary>
    [Serializable]
    [XmlRoot("gpx", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class TCXFile : IGeoFileConvertable
    {
        public GeoFile ToGeoFile()
        {
            throw new NotImplementedException();
        }
    }
}