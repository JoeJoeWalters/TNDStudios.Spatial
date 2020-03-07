using GeoCoordinatePortable;
using System;

namespace Package.Documents
{
    /// <summary>
    /// Geocoordinate extended to add time of the coordinate so it
    /// can be used to calculate speed between two points
    /// </summary>
    public class GeoCoordinateExtended : GeoCoordinate
    {
        public DateTime time { get; set; } = DateTime.MinValue;

        public GeoCoordinateExtended(Double latitude, Double longitude, Double altitude, DateTime time) : base(latitude, longitude, altitude)
        {
            this.time = time;
        }

        public GeoCoordinateExtended(Double latitude, Double longitude, Double altitude) : base(latitude, longitude, altitude)
        {

        }
    }
}
