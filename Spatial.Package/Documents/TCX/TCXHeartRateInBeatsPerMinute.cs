using System;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXHeartRateInBeatsPerMinute : XmlBase
    {
        [XmlElement("Value")]
        public Byte Value { get; set; }
    }
}
