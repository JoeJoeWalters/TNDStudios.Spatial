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
    /// </summary>
    [Serializable]
    [XmlRoot("TrainingCenterDatabase", Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public class TCXFile : XmlBase, IGeoFileConvertable
    {
        [XmlElement("Activities")]
        public TCXActivities Activities { get; set; }

        public GeoFile ToGeoFile()
        {
            throw new NotImplementedException();
        }
    }

    public class TCXActivities : XmlBase
    {
        [XmlElement("Activity")]
        public List<TCXActivity> Items { get; set; }
    }

    public class TCXActivity : XmlBase
    {
        [XmlAttribute("Sport")]
        public String Sport { get; set; }
    }
}