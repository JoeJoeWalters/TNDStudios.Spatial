using Spatial.Common;
using System.Xml.Serialization;

namespace Spatial.Documents
{
    public class TCXBuild : XmlBase
    {
        [XmlElement("Version")]
        public TCXVersion Version { get; set; }
    }
}
