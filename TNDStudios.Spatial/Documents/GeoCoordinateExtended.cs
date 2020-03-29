using GeoCoordinatePortable;
using System;

namespace TNDStudios.Spatial.Documents
{
    /// <summary>
    /// Geocoordinate extended to add time of the coordinate so it
    /// can be used to calculate speed between two points
    /// </summary>
    public class GeoCoordinateExtended : GeoCoordinate
    {
        public DateTime Time { get; set; } = DateTime.MinValue;

        public GeoCoordinateExtended(Double latitude, Double longitude, Double altitude, DateTime time) : base(latitude, longitude, altitude)
        {
            Time = time;
        }

        public GeoCoordinateExtended(Double latitude, Double longitude, Double altitude) : base(latitude, longitude, altitude)
        {
        }

        public void CalculateSpeed(GeoCoordinateExtended previous)
        {
            Double distance = this.GetDistanceTo(previous);
            Double seconds = ((TimeSpan)(this.Time - previous.Time)).TotalSeconds;
            this.Speed = distance / seconds;
        }
    }
}
