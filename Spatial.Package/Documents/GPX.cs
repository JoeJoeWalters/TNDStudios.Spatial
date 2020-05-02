using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

/// <summary>
/// https://docs.microsoft.com/en-us/dotnet/standard/serialization/controlling-xml-serialization-using-attributes
/// </summary>
namespace TNDStudios.Spatial.Documents
{
    /// <summary>
    /// Implementation of http://www.topografix.com/GPX/1/1/gpx.xsd
    /// where GPXType is the base 1.1 version which hasn't changed since 2004
    /// but done in this way incase it should be system future proof
    /// </summary>
    [Serializable]
    [XmlRoot("gpx", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class GPXFile : GPXType, IGeoFileConvertable { }

    [Serializable]
    public class GPXType : XmlBase
    {
        // ISO 8601 formatter instead of using roundtrip kind parsing as needed for read and write (get and set)
        public static String DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";

        [XmlAttribute("creator")]
        public String Creator { get; set; } = String.Empty;

        [XmlAttribute("version")]
        public Decimal Version { get; set; } = 1.1M;

        [XmlElement("metadata")]
        public GPXMetaData MetaData { get; set; } = new GPXMetaData();

        [XmlElement("wpt")]
        public List<GPXWaypoint> Waypoints { get; set; } = new List<GPXWaypoint>();

        [XmlElement("rte")]
        public List<GPXRoute> Routes { get; set; } = new List<GPXRoute>();

        [XmlElement("trk")]
        public List<GPXTrack> Tracks { get; set; } = new List<GPXTrack>();

        [XmlElement("extensions")]
        public List<GPXExtension> Extensions { get; set; } = new List<GPXExtension>();

        /// <summary>
        /// Convert this GPX Type to the common GeoFile format
        /// </summary>
        /// <returns>The GPX converted to GeoFile format</returns>
        public GeoFile ToGeoFile()
        {
            // Create a blank new file with the basic information
            GeoFile result = new GeoFile()
            {
                Name = this.MetaData.Name,
                Author = this.Creator,
                Routes = new List<GeoFileRoute>()
            };

            // Loop the routes and add them to the GeoFile version
            result.Routes = this.Routes.Select(route =>
                new GeoFileRoute()
                {
                    Name = route.Name,
                    Points = route.ToCoords()
                }).ToList();

            // Loop the tracks and add them to the GeoFile version
            result.Routes.AddRange(this.Tracks.Select(track =>
                new GeoFileRoute()
                {
                    Name = track.Name,
                    Points = track.ToCoords()
                }).ToList());

            return result;
        }

        /// <summary>
        /// Convert a GeoFile to the native format
        /// </summary>
        /// <param name="file">The GeoFile format to convert from</param>
        /// <returns>Success Or Failure flag</returns>
        public Boolean FromGeoFile(GeoFile file) => throw new NotImplementedException();
    }

    [Serializable]
    public class GPXMetaData : XmlBase
    {
        /// <summary>
        /// The name of the GPX file.
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// A description of the contents of the GPX file.
        /// </summary>
        [XmlElement("desc")]
        public String Description { get; set; } = String.Empty;

        /// <summary>
        /// The person or organization who created the GPX file.
        /// </summary>
        [XmlElement("author")]
        public GPXPerson Author { get; set; } = new GPXPerson();

        /// <summary>
        /// Copyright and license information governing use of the file.
        /// </summary>
        [XmlElement("copyright")]
        public GPXCopyright Copyright { get; set; } = new GPXCopyright();

        /// <summary>
        /// URLs associated with the location described in the file.
        /// </summary>
        [XmlElement("link")]
        public GPXLink Link { get; set; } = new GPXLink();

        /// <summary>
        /// The creation date of the file.
        /// </summary>
        [XmlIgnore]
        public DateTime CreatedDateTime = DateTime.MinValue;
        [XmlElement("time")]
        public String Time { get; set; }

        /// <summary>
        /// Keywords associated with the file. Search engines or databases can use this information to classify the data.
        /// </summary>
        [XmlElement("keywords")]
        public String Keywords { get; set; } = String.Empty;

        /// <summary>
        /// Minimum and maximum coordinates which describe the extent of the coordinates in the file.
        /// </summary>
        [XmlElement("bounds")]
        public GPXBounds Bounds { get; set; } = new GPXBounds();

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlElement("extensions")]
        public List<GPXExtension> Extensions { get; set; } = new List<GPXExtension>();
    }

    /// <summary>
    /// wpt represents a waypoint, point of interest, or named feature on a map.
    /// </summary>
    [Serializable]
    public class GPXWaypoint : XmlBase
    {
        /// <summary>
        /// Elevation (in meters)
        /// </summary>
        [XmlElement("ele")]
        public Decimal Elevation { get; set; } = 0.0M;

        /// <summary>
        /// Creation/modification timestamp for element. Date and time in are in Univeral Coordinated Time (UTC), not local time! Conforms to ISO 8601 specification for date/time representation. Fractional seconds are allowed for millisecond timing in tracklogs.
        /// </summary>
        [XmlIgnore]
        public DateTime CreatedDateTime = DateTime.MinValue;
        [XmlElement("time")]
        public String Time
        {
            get { return CreatedDateTime.ToString(GPXType.DateTimeFormat); }
            set { this.CreatedDateTime = DateTime.Parse(value); }
        }

        /// <summary>
        /// Magnetic variation (in degrees) at the point (0.0 to 360.0)
        /// </summary>
        [XmlElement("magvar")]
        public Decimal MagneticVariation { get; set; } = 0.0M;

        /// <summary>
        /// Height (in meters) of geoid (mean sea level) above WGS84 earth ellipsoid. As defined in NMEA GGA message.
        /// </summary>
        [XmlElement("geoidheight")]
        public Decimal Height { get; set; } = 0.0M;

        /// <summary>
        /// The GPS name of the waypoint. This field will be transferred to and from the GPS. GPX does not place restrictions on the length of this field or the characters contained in it. It is up to the receiving application to validate the field before sending it to the GPS.
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// GPS waypoint comment. Sent to GPS as comment.
        /// </summary>
        [XmlElement("cmt")]
        public String Comment { get; set; } = String.Empty;

        /// <summary>
        /// A text description of the element. Holds additional information about the element intended for the user, not the GPS.
        /// </summary>
        [XmlElement("desc")]
        public String Description { get; set; } = String.Empty;

        /// <summary>
        /// Source of data. Included to give user some idea of reliability and accuracy of data. "Garmin eTrex", "USGS quad Boston North", e.g.
        /// </summary>
        [XmlElement("src")]
        public String Source { get; set; } = String.Empty;

        /// <summary>
        /// Link to additional information about the waypoint.
        /// </summary>
        [XmlElement("link")]
        public GPXLink Link { get; set; } = new GPXLink();

        /// <summary>
        /// Text of GPS symbol name. For interchange with other programs, use the exact spelling of the symbol as displayed on the GPS. If the GPS abbreviates words, spell them out.
        /// </summary>
        [XmlElement("sym")]
        public String Symbol { get; set; } = String.Empty;

        /// <summary>
        /// Type (classification) of the waypoint.
        /// </summary>
        [XmlElement("type")]
        public String Type { get; set; } = String.Empty;

        /*
        Accuracy Information
        */

        /// <summary>
        /// Type of GPX fix. (Types can be none, 2d, 3d, dgps, pps)
        /// </summary>
        [XmlElement("fix")]
        public String Fix { get; set; } = String.Empty;

        /// <summary>
        /// Number of satellites used to calculate the GPX fix.
        /// </summary>
        [XmlElement("sat")]
        public Int32 Satellites { get; set; } = 0;

        /// <summary>
        /// Horizontal dilution of precision.
        /// </summary>
        [XmlElement("hdop")]
        public Decimal DilutionHorizontal { get; set; } = 0.0M;

        /// <summary>
        /// Vertical dilution of precision.
        /// </summary>
        [XmlElement("vdop")]
        public Decimal DilutionVertical { get; set; } = 0.0M;

        /// <summary>
        /// Position dilution of precision.
        /// </summary>
        [XmlElement("pdop")]
        public Decimal DilutionPosition { get; set; } = 0.0M;

        /// <summary>
        /// Number of seconds since last DGPS update.
        /// </summary>
        [XmlElement("ageofdgpsdata")]
        public Decimal Age { get; set; } = 0.0M;

        /// <summary>
        /// ID of DGPS station used in differential correction.
        /// </summary>
        [XmlElement("dgpsid")]
        public GPXStation Station { get; set; } = new GPXStation();

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlElement("extensions")]
        public List<GPXExtension> Extensions { get; set; } = new List<GPXExtension>();

        /// <summary>
        /// The latitude of the point.This is always in decimal degrees, and always in WGS84 datum.
        /// </summary>
        [XmlAttribute("lat")]
        public Decimal Latitude { get; set; } = 0.0M;

        /// <summary>
        /// The longitude of the point. This is always in decimal degrees, and always in WGS84 datum.
        /// </summary>
        [XmlAttribute("lon")]
        public Decimal Longitude { get; set; } = 0.0M;

        /// <summary>
        /// Convert point to native coordinate
        /// </summary>
        /// <returns></returns>
        public GeoCoordinateExtended ToCoord()
        {
            return new GeoCoordinateExtended(
                (Double)this.Latitude,
                (Double)this.Longitude,
                (Double)this.Elevation,
                this.CreatedDateTime
                );
        }
    }

    /// <summary>
    /// rte represents route - an ordered list of waypoints representing a series of turn points leading to a destination.
    /// </summary>
    [Serializable]
    public class GPXRoute : XmlBase
    {
        /// <summary>
        /// GPS name of route.
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// GPS comment for route.
        /// </summary>
        [XmlElement("cmt")]
        public String Comment { get; set; } = String.Empty;

        /// <summary>
        /// Text description of route for user. Not sent to GPS.
        /// </summary>
        [XmlElement("desc")]
        public String Description { get; set; } = String.Empty;

        /// <summary>
        /// Source of data.Included to give user some idea of reliability and accuracy of data.
        /// </summary>
        [XmlElement("src")]
        public String Source { get; set; } = String.Empty;

        /// <summary>
        /// Links to external information about the route. 
        /// </summary>
        [XmlElement("link")]
        public GPXLink Link { get; set; } = new GPXLink();

        /// <summary>
        /// GPS route number.
        /// </summary>
        [XmlElement("number")]
        public Int32 RouteNumber { get; set; } = 0;

        /// <summary>
        /// Type (classification) of route.
        /// </summary>
        [XmlElement("type")]
        public String Type { get; set; } = String.Empty;

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlElement("extensions")]
        public List<GPXExtension> Extensions { get; set; } = new List<GPXExtension>();

        /// <summary>
        /// A list of route points.
        /// </summary>
        [XmlElement("rtept")]
        public List<GPXWaypoint> RoutePoints { get; set; } = new List<GPXWaypoint>();

        /// <summary>
        /// Convert the list of points to a list of common coordinates
        /// </summary>
        /// <returns></returns>
        public List<GeoCoordinateExtended> ToCoords() => RoutePoints.Select(pt => pt.ToCoord()).ToList();
    }

    /// <summary>
    /// trk represents a track - an ordered list of points describing a path.
    /// </summary>
    [Serializable]
    public class GPXTrack : XmlBase
    {
        /// <summary>
        /// GPS name of track.
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// GPS comment for track.
        /// </summary>
        [XmlElement("cmt")]
        public String Comment { get; set; } = String.Empty;

        /// <summary>
        /// User description of track. 
        /// </summary>
        [XmlElement("desc")]
        public String Description { get; set; } = String.Empty;

        /// <summary>
        /// Source of data. Included to give user some idea of reliability and accuracy of data.
        /// </summary>
        [XmlElement("src")]
        public String Source { get; set; } = String.Empty;

        /// <summary>
        /// Links to external information about the route. 
        /// </summary>
        [XmlElement("link")]
        public GPXLink Link { get; set; }

        /// <summary>
        /// GPS track number.
        /// </summary>
        [XmlElement("number")]
        public Int32 TrackNumber { get; set; } = 0;

        /// <summary>
        /// Type (classification) of track.
        /// </summary>
        [XmlElement("type")]
        public String Type { get; set; } = String.Empty;

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlElement("extensions")]
        public List<GPXExtension> Extensions { get; set; } = new List<GPXExtension>();

        /// <summary>
        /// A Track Segment holds a list of Track Points which are logically connected in order. To represent a single GPS track where GPS reception was lost, or the GPS receiver was turned off, start a new Track Segment for each continuous span of track data.
        /// </summary>
        [XmlElement("trkseg")]
        public List<GPXTrackSegment> TrackSegments { get; set; } = new List<GPXTrackSegment>();

        /// <summary>
        /// Convert the list of points to a list of common coordinates
        /// </summary>
        /// <returns></returns>
        public List<GeoCoordinateExtended> ToCoords()
        {
            List<GeoCoordinateExtended> merged = new List<GeoCoordinateExtended>();
            TrackSegments.ForEach(seg => merged.AddRange(seg.ToCoords()));
            return merged;
        }
    }

    /// <summary>
    /// A Track Segment holds a list of Track Points which are logically connected in order. To represent a single GPS track where GPS reception was lost, or the GPS receiver was turned off, start a new Track Segment for each continuous span of track data.
    /// </summary>
    [Serializable]
    public class GPXTrackSegment : XmlBase
    {
        /// <summary>
        /// A Track Point holds the coordinates, elevation, timestamp, and metadata for a single point in a track.
        /// </summary>
        [XmlElement("trkpt")]
        public List<GPXWaypoint> TrackPoints { get; set; } = new List<GPXWaypoint>();

        /// <summary>
        /// You can add extend GPX by adding your own elements from another schema here.
        /// </summary>
        [XmlElement("extensions")]
        public List<GPXExtension> Extensions { get; set; } = new List<GPXExtension>();

        /// <summary>
        /// Convert the list of points to a list of common coordinates
        /// </summary>
        /// <returns></returns>
        public List<GeoCoordinateExtended> ToCoords() => TrackPoints.Select(pt => pt.ToCoord()).ToList();
    }

    /// <summary>
    /// You can add extend GPX by adding your own elements from another schema here.
    /// </summary>
    [Serializable]
    public class GPXExtension : XmlBase
    {
        // All Dynamic handled by Base class
    }

    /// <summary>
    /// Information about the copyright holder and any license governing use of this file. By linking to an appropriate license, you may place your data into the public domain or grant additional usage rights.
    /// </summary>
    [Serializable]
    public class GPXCopyright : XmlBase
    {
        /// <summary>
        /// Year of copyright.
        /// </summary>
        [XmlElement("year")]
        public Int32 Year { get; set; } = 0;

        /// <summary>
        /// Link to external file containing license text.
        /// </summary>
        [XmlElement("license")]
        public String License { get; set; } = String.Empty;

        /// <summary>
        /// Copyright holder (TopoSoft, Inc.) 
        /// </summary>
        [XmlElement("author")]
        public GPXPerson Author { get; set; } = new GPXPerson();
    }

    /// <summary>
    /// Two lat/lon pairs defining the extent of an element.
    /// </summary>
    [Serializable]
    public class GPXBounds : XmlBase
    {
        /// <summary>
        /// The minimum latitude.
        /// </summary>
        [XmlElement("minlat")]
        public Decimal MinLatitude { get; set; } = 0.0M;

        /// <summary>
        /// The minimum longitude.
        /// </summary>
        [XmlElement("minlon")]
        public Decimal MinLongitude { get; set; } = 0.0M;

        /// <summary>
        /// The maximum latitude.
        /// </summary>
        [XmlElement("maxlat")]
        public Decimal MaxLatitude { get; set; } = 0.0M;

        /// <summary>
        /// The maximum longitude.
        /// </summary>
        [XmlElement("maxlon")]
        public Decimal MaxLongitude { get; set; } = 0.0M;
    }

    /// <summary>
    /// A link to an external resource (Web page, digital photo, video clip, etc) with additional information.
    /// </summary>
    [Serializable]
    public class GPXLink : XmlBase
    {
        /// <summary>
        /// Text of hyperlink.
        /// </summary>
        [XmlElement("text")]
        public String Text { get; set; } = String.Empty;

        /// <summary>
        /// Mime type of content (image/jpeg)
        /// </summary>
        [XmlElement("type")]
        public String Type { get; set; } = String.Empty;

        /// <summary>
        /// URL of hyperlink.
        /// </summary>
        [XmlElement("href")]
        public String HRef { get; set; } = String.Empty;
    }

    /// <summary>
    /// An ordered sequence of points. (for polygons or polylines, e.g.)
    /// </summary>
    [Serializable]
    public class GPXSegment : XmlBase
    {
        /// <summary>
        /// Ordered list of geographic points.
        /// </summary>
        [XmlElement("pt")]
        public List<GPXPoint> Points { get; set; } = new List<GPXPoint>();
    }

    /// <summary>
    /// A geographic point with optional elevation and time. Available for use by other schemas.
    /// </summary>
    [Serializable]
    public class GPXPoint : XmlBase
    {
        /// <summary>
        /// The elevation (in meters) of the point.
        /// </summary>
        [XmlElement("ele")]
        public Decimal Elevation { get; set; } = 0.0M;

        /// <summary>
        /// The time that the point was recorded.
        /// </summary>
        [XmlIgnore]
        public DateTime CreatedDateTime = DateTime.MinValue;
        [XmlElement("time")]
        public String Time { get; set; }

        /// <summary>
        /// The latitude of the point. Decimal degrees, WGS84 datum.
        /// </summary>
        [XmlElement("lat")]
        public Decimal Latitude { get; set; } = 0.0M;

        /// <summary>
        /// The longitude of the point. Decimal degrees, WGS84 datum.
        /// </summary>
        [XmlElement("lon")]
        public Decimal Longitude { get; set; } = 0.0M;
    }

    /// <summary>
    /// A person or organization.
    /// </summary>
    [Serializable]
    public class GPXPerson : XmlBase
    {
        /// <summary>
        /// Name of person or organization.
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// Email address.
        /// </summary>
        [XmlElement("email")]
        public GPXEMail EMail { get; set; } = new GPXEMail();

        /// <summary>
        /// Link to Web site or other external information about person.
        /// </summary>
        [XmlElement("link")]
        public GPXLink Link { get; set; } = new GPXLink();
    }

    /// <summary>
    /// An email address. Broken into two parts (id and domain) to help prevent email harvesting.
    /// </summary>
    [Serializable]
    public class GPXEMail : XmlBase
    {
        /// <summary>
        /// id half of email address (billgates2004)
        /// </summary>
        [XmlElement("id")]
        public String Id { get; set; } = String.Empty;

        /// <summary>
        /// domain half of email address (hotmail.com)
        /// </summary>
        [XmlElement("domain")]
        public String Domain { get; set; } = String.Empty;
    }

    [Serializable]
    public class GPXFix : XmlBase
    {
    }

    [Serializable]
    public class GPXStation : XmlBase
    {
    }
}
