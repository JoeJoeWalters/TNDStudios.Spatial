using System.Xml;
using System.Xml.Serialization;

namespace TNDStudios.Spatial.Common
{
    public abstract class XmlBase
    {
        [XmlAnyElement]
        public XmlElement[] Unsupported { get; set; }
    }

}
