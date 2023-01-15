using Spatial.Common;
using System.Xml.Serialization;

namespace Spatial.Documents
{
    public class TCXNextSport : XmlBase
    {
        [XmlElement("Transition")]
        public TCXActivityLap Transition { get; set; }

        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }
}
