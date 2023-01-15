using Spatial.Common;
using System.Xml.Serialization;

namespace Spatial.Documents
{
    public class TCXFirstSport : XmlBase
    {
        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }
}
