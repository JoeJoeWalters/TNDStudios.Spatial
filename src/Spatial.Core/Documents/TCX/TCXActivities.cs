using Spatial.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spatial.Documents
{
    public class TCXActivities : XmlBase
    {
        [XmlElement("Activity")]
        public List<TCXActivity> Activity { get; set; }

        [XmlElement("MultiSportSession")]
        public TCXMultiSportSession MultiSportSession { get; set; }
    }
}
