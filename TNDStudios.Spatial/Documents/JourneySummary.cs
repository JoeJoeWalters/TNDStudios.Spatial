using GeoCoordinatePortable;
using Newtonsoft.Json;
using Package.Common;
using System;
using System.Collections.Generic;

namespace Package.Documents
{
    [JsonObject]
    public class JourneySummary
    {
        [JsonProperty(PropertyName = "points")]
        public List<JourneyPoint> Points { get; set; } = new List<JourneyPoint>();
    }

    [JsonObject]
    public class JourneyPoint
    {
        /// <summary>
        /// Distance from previous point calculated precalculated
        /// </summary>
        [JsonProperty(PropertyName = "distanceFromPreviousPoint")]
        public Double DistanceFromPreviousPoint { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public Double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public Double Longitude { get; set; }

        [JsonProperty(PropertyName = "elevation")]
        public Double Elevation { get; set; }

        public JourneyPoint(GeoCoordinate current) => ConstructorBase(current);
        public JourneyPoint(GeoCoordinate current, GeoCoordinate previous)
        {
            ConstructorBase(current);
            DistanceFromPreviousPoint = current.GetDistanceTo(previous);
        }

        /// <summary>
        /// Shared constructor basics
        /// </summary>
        /// <param name="current">The Geocoordinate to populate this point</param>
        private void ConstructorBase(GeoCoordinate current)
        {
            Latitude = current.Latitude;
            Longitude = current.Longitude;
            Elevation = current.Altitude;
        }
    }
}
