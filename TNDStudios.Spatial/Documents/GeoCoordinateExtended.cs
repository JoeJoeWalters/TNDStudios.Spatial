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

        /// <summary>
        /// If a coordinate cannot represent the longitude and latitude e.g. in TCX files where the signal is paused no position is given,
        /// so used as a trigger to infill coordinates from previous entries on conversion.
        /// </summary>
        public Boolean BadCoordinate { get; set; } = false;

        /// <summary>
        /// Default constructor for Newtonsoft to perform serialisation etc.
        /// </summary>
        public GeoCoordinateExtended()
        {
        }

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
