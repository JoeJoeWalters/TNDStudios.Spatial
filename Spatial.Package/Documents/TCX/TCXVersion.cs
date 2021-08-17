﻿using System;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXVersion : XmlBase
    {
        [XmlElement("VersionMajor")]
        public Byte VersionMajor { get; set; }

        [XmlElement("VersionMinor")]
        public Byte VersionMinor { get; set; }

        [XmlElement("BuildMajor")]
        public Byte BuildMajor { get; set; }

        [XmlElement("BuildMinor")]
        public Byte BuildMinor { get; set; }

    }
}