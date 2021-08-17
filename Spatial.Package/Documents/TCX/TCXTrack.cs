using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TNDStudios.Spatial.Common;

namespace TNDStudios.Spatial.Documents
{
    public class TCXTrack : XmlBase
    {
        [XmlElement("Trackpoint")]
        public List<TCXTrackPoint> TrackPoints { get; set; }

        public List<GeoCoordinateExtended> ToCoords()
            => TrackPoints
                .Select(trkpt => trkpt.ToCoord())
                .ToList().InfillPositions();
    }
}
