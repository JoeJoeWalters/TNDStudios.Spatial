using System.Xml.Serialization;
using Spatial.Common;

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
