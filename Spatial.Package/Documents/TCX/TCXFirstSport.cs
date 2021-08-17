using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXFirstSport : XmlBase
    {
        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }
}
