using System;
using System.Xml.Serialization;
using Spatial.Common;

namespace Spatial.Documents
{
    public class TCXHeartRateInBeatsPerMinute : XmlBase
    {
        [XmlElement("Value")]
        public Byte Value { get; set; }
    }
}
