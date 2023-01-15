using Spatial.Common;
using System;
using System.Xml.Serialization;

namespace Spatial.Documents
{
    public class TCXHeartRateInBeatsPerMinute : XmlBase
    {
        [XmlElement("Value")]
        public Byte Value { get; set; }
    }
}
