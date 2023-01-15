using System;
using System.Xml.Serialization;
using Spatial.Common;

namespace Spatial.Documents
{
    public class TCXPosition : XmlBase
    {
        [XmlElement("LatitudeDegrees")]
        public Double LatitudeDegrees { get; set; }

        [XmlElement("LongitudeDegrees")]
        public Double LongitudeDegrees { get; set; }
    }
}
