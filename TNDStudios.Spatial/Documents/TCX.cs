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
        [XmlElement("Folders")]
        public TCXFolders Folders { get; set; }

        [XmlElement("Activities")]
        public TCXActivities Activities { get; set; }

        [XmlElement("Workouts")]
        public TCXWorkouts Workouts { get; set; }

        [XmlElement("Courses")]
        public TCXCourses Courses { get; set; }

        [XmlElement("Author")]
        public TCXAbstractSource Author { get; set; }

        [XmlElement("Extensions")]
        public TCXExtensions Extensions { get; set; }

        public GeoFile ToGeoFile()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Extensions which is defined purposfully empty so XmlBase will
    /// pick up unmapped members
    /// </summary>
    public class TCXExtensions : XmlBase { }

    public class TCXFolders : XmlBase
    {

    }

    public class TCXActivities : XmlBase
    {
        [XmlElement("Activity")]
        public List<TCXActivity> Activity { get; set; }

        [XmlElement("MultiSportSession")]
        public TCXMultiSportSession MultiSportSession { get; set; }
    }

    public class TCXMultiSportSession : XmlBase
    {
        [XmlElement("Id")]
        public String Id { get; set; }

        [XmlElement("FirstSport")]
        public TCXFirstSport FirstSport { get; set; }

        [XmlElement("NextSport")]
        public List<TCXNextSport> NextSport { get; set; }

        [XmlElement("Notes")]
        public String Notes { get; set; }
    }

    public class TCXFirstSport : XmlBase
    {
        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }

    public class TCXNextSport : XmlBase
    {
        [XmlElement("Transition")]
        public TCXActivityLap Transition { get; set; }

        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }

    public class TCXActivityLap : XmlBase
    {
        [XmlAttribute("StartTime")]
        public String StartTime { get; set; }

        [XmlElement("TotalTimeSeconds")]
        public Double TotalTimeSeconds { get; set; }

        [XmlElement("DistanceMeters")]
        public Double DistanceMeters { get; set; }

        [XmlElement("MaximumSpeed")]
        public Double MaximumSpeed { get; set; }

        [XmlElement("Calories")]
        public Int32 Calories { get; set; }

        [XmlElement("AverageHeartRateBpm")]
        public TCXHeartRateInBeatsPerMinute AverageHeartRateBpm { get; set; }

        [XmlElement("MaximumHeartRateBpm")]
        public TCXHeartRateInBeatsPerMinute MaximumHeartRateBpm { get; set; }

        [XmlElement("Intensity")]
        public String Intensity { get; set; }

        [XmlElement("Cadence")]
        public Byte Cadence { get; set; }

        [XmlElement("TriggerMethod")]
        public String TriggerMethod { get; set; }

        [XmlElement("Track")]
        public TCXTrack Track { get; set; }

        [XmlElement("Notes")]
        public String Notes { get; set; }

        [XmlElement("Extensions")]
        public TCXExtensions Extensions { get; set; }
    }

    public class TCXHeartRateInBeatsPerMinute : XmlBase
    {
        [XmlElement("Value")]
        public Byte Value { get; set; }
    }

    public class TCXActivity : XmlBase
    {
        [XmlAttribute("Sport")]
        public String Sport { get; set; }

        [XmlElement("Id")]
        public String Id { get; set; }
    }
}