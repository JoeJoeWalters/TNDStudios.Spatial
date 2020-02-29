using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Package.Common
{
    public class XmlBase
    {
        [XmlAnyElement]
        public XmlElement[] Unsupported { get; set; }
    }

}
