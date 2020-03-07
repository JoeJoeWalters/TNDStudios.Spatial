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
    }
}
