using Spatial.Common;
using Spatial.Helpers;
using System;

namespace Spatial.Documents
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

        /// <summary>
        /// Calculate the speed from a previous point
        /// </summary>
        /// <param name="previous"></param>
        public void CalculateSpeed(GeoCoordinateExtended previous)
        {
            Double distance = this.GetDistanceTo(previous);
            Double seconds = ((TimeSpan)(this.Time - previous.Time)).TotalSeconds;
            this.Speed = distance / seconds;
        }

        /// <summary>
        /// Round this coordinate to the nearest grid coordinate of X meters
        /// </summary>
        /// <param name="meters"></param>
        public GeoCoordinateExtended Round(Double meters)
        {
            // Coordinate offsets in radians
            Double latitudeMeters = this.Latitude * TrackHelper.LatitudeDistance;
            Double longitudeMeters = this.Longitude * (TrackHelper.EarthRadius * Math.Cos(this.Latitude) / 360.0D);

            Double roundedLatitude = meters * Math.Round(latitudeMeters / meters, 0);
            Double roundedLongitude = meters * Math.Round(longitudeMeters / meters, 0);

            this.Latitude = roundedLatitude / TrackHelper.LatitudeDistance;
            this.Longitude = roundedLongitude / (TrackHelper.EarthRadius * Math.Cos(this.Latitude) / 360.0D);

            return this;
        }
    }
}
