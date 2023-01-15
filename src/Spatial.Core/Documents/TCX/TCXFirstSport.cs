using System.Xml.Serialization;
using Spatial.Common;

namespace Spatial.Documents
{
    public class TCXFirstSport : XmlBase
    {
        [XmlElement("Activity")]
        public TCXActivity Activity { get; set; }
    }
}
