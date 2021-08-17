using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXNextSport : XmlBase
    {
        [XmlElement("Transition")]
        public TCXActivityLap Transition { get; set; }

        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }
}
