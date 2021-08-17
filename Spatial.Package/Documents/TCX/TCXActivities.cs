using System.Collections.Generic;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXActivities : XmlBase
    {
        [XmlElement("Activity")]
        public List<TCXActivity> Activity { get; set; }

        [XmlElement("MultiSportSession")]
        public TCXMultiSportSession MultiSportSession { get; set; }
    }
}
