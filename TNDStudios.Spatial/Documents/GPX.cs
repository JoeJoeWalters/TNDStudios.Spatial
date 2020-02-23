using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TNDStudios.Spatial.Documents
{
    public class XmlBase
    {
        [XmlAnyElement]
        public XmlElement[] Unsupported { get; set; }
    }

    // http://www.topografix.com/GPX/1/1/gpx.xsd
    public class GPXFile : XmlBase
    {
        GPXType GPX { get; set; }
    }

    public class GPXType : XmlBase
    {
        [XmlAttribute("creator")]
        public String Creator { get; set; }

        [XmlAttribute("version")]
        public Decimal Version { get; set; }

        [XmlElement("metadata")]
        public GPXMetaData MetaData { get; set; }

        [XmlArray("wpt")]
        public GPXWaypoint[] Waypoints { get; set; }

        [XmlArray("rte")]
        public GPXRoute[] Routes { get; set; }

        [XmlArray("trk")]
        public GPXTrack[] Tracks { get; set; }

        [XmlArray("extensions")]
        public GPXExtension[] Extensions { get; set; }
    }

    public class GPXMetaData : XmlBase
    {
        /// <summary>
        /// The name of the GPX file.
        /// </summary>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// A description of the contents of the GPX file.
        /// </summary>
        [XmlAttribute("desc")]
        public String Description { get; set; }

        /// <summary>
        /// The person or organization who created the GPX file.
        /// </summary>
        [XmlAttribute("author")]
        public GPXAuthor Author { get; set; }

        /// <summary>
        /// Copyright and license information governing use of the file.
        /// </summary>
        [XmlAttribute("copyright")]
        public GPXCopyright Copyright { get; set; }

        /// <summary>
        /// URLs associated with the location described in the file.
        /// </summary>
        [XmlArray("link")]
        public GPXLink[] Links { get; set; }

        /// <summary>
        /// The creation date of the file.
        /// </summary>
        [XmlAttribute("time")]
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Keywords associated with the file. Search engines or databases can use this information to classify the data.
        /// </summary>
        [XmlAttribute("keywords")]
        public String Keywords { get; set; }

        /// <summary>
        /// Minimum and maximum coordinates which describe the extent of the coordinates in the file.
        /// </summary>
        [XmlAttribute("bounds")]
        public GPXBounds Bounds { get; set; }

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlArray("extensions")]
        public GPXExtension[] Extensions { get; set; }
    }

    /// <summary>
    /// wpt represents a waypoint, point of interest, or named feature on a map.
    /// </summary>
    public class GPXWaypoint : XmlBase
    {
        /// <summary>
        /// Elevation (in meters)
        /// </summary>
        [XmlElement("ele")]
        Decimal Elevation { get; set; }

        /// <summary>
        /// Creation/modification timestamp for element. Date and time in are in Univeral Coordinated Time (UTC), not local time! Conforms to ISO 8601 specification for date/time representation. Fractional seconds are allowed for millisecond timing in tracklogs.
        /// </summary>
        [XmlElement("time")]
        DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Magnetic variation (in degrees) at the point 
        /// </summary>
        [XmlElement("magvar")]
        public GPXDegrees MagneticVariation { get; set; }

        /// <summary>
        /// Height (in meters) of geoid (mean sea level) above WGS84 earth ellipsoid. As defined in NMEA GGA message.
        /// </summary>
        [XmlElement("geoidheight")]
        public Decimal Height { get; set; }

        /// <summary>
        /// The GPS name of the waypoint. This field will be transferred to and from the GPS. GPX does not place restrictions on the length of this field or the characters contained in it. It is up to the receiving application to validate the field before sending it to the GPS.
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; }

        /// <summary>
        /// GPS waypoint comment. Sent to GPS as comment.
        /// </summary>
        [XmlElement("cmt")]
        public String Comment { get; set; }

        /// <summary>
        /// A text description of the element. Holds additional information about the element intended for the user, not the GPS.
        /// </summary>
        [XmlElement("desc")]
        public String Description { get; set; }

        /// <summary>
        /// Source of data. Included to give user some idea of reliability and accuracy of data. "Garmin eTrex", "USGS quad Boston North", e.g.
        /// </summary>
        [XmlElement("src")]
        public String Source { get; set; }

        /// <summary>
        /// Link to additional information about the waypoint.
        /// </summary>
        [XmlElement("link")]
        public GPXLink[] Links { get; set; }

        /// <summary>
        /// Text of GPS symbol name. For interchange with other programs, use the exact spelling of the symbol as displayed on the GPS. If the GPS abbreviates words, spell them out.
        /// </summary>
        [XmlElement("sym")]
        public String Symbol { get; set; }

        /// <summary>
        /// Type (classification) of the waypoint.
        /// </summary>
        [XmlElement("type")]
        public String Type { get; set; }

        /*
        Accuracy Information
        */

        /// <summary>
        /// Type of GPX fix.
        /// </summary>
        [XmlElement("fix")]
        public GPXFix Fix { get; set; }

        /// <summary>
        /// Number of satellites used to calculate the GPX fix.
        /// </summary>
        [XmlElement("sat")]
        public Int32 Satellites { get; set; }

        /// <summary>
        /// Horizontal dilution of precision.
        /// </summary>
        [XmlElement("hdop")]
        public Decimal DilutionHorizontal { get; set; }

        /// <summary>
        /// Vertical dilution of precision.
        /// </summary>
        [XmlElement("vdop")]
        public Decimal DilutionVertical { get; set; }

        /// <summary>
        /// Position dilution of precision.
        /// </summary>
        [XmlElement("pdop")]
        public Decimal DilutionPosition { get; set; }

        /// <summary>
        /// Number of seconds since last DGPS update.
        /// </summary>
        [XmlElement("ageofdgpsdata")]
        public Decimal Age { get; set; }

        /// <summary>
        /// ID of DGPS station used in differential correction.
        /// </summary>
        [XmlElement("dgpsid")]
        public GPXStation Station { get; set; }

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlArray("extensions")]
        public GPXExtension[] Extensions { get; set; }

        [XmlElement("lat")]
        public GPXLatitude Latitude { get; set; }

        [XmlElement("lon")]
        public GPXLongitude Longitude { get; set; }
    }

    /// <summary>
    /// rte represents route - an ordered list of waypoints representing a series of turn points leading to a destination.
    /// </summary>
    public class GPXRoute : XmlBase 
    { 
    
    }

    /// <summary>
    /// 
    /// </summary>
    public class GPXTrack : XmlBase { }

    /// <summary>
    /// 
    /// </summary>
    public class GPXExtension : XmlBase { }

    public class GPXAuthor : XmlBase
    {
    }

    public class GPXCopyright : XmlBase
    {
    }

    public class GPXBounds : XmlBase
    {
    }

    public class GPXLink : XmlBase
    {
    }

    public class GPXDegrees : XmlBase
    {
    }

    public class GPXFix : XmlBase
    {
    }

    public class GPXStation : XmlBase
    {
    }

    public class GPXLatitude : XmlBase
    {
    }

    public class GPXLongitude : XmlBase
    {
    }
}
