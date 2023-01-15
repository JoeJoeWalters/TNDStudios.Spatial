using System.Xml.Serialization;
using Spatial.Common;

namespace Spatial.Documents
{
    public class TCXBuild : XmlBase
    {
        [XmlElement("Version")]
        public TCXVersion Version { get; set; }
    }
}
