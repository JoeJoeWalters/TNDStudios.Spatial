using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXBuild : XmlBase
    {
        [XmlElement("Version")]
        public TCXVersion Version { get; set; }
    }
}
