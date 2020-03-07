using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Common
{
    public class Coord
    {
        public static Double RadiansOverDegrees = (Math.PI / 180.0);
        public static Double EarthMiles = 3956.0D;

        public Double Latitude { get; set; } = 0D;
        public Double LatitudeAsRadians { get { return Latitude * RadiansOverDegrees; } }
        public Double Longitude { get; set; } = 0D;
        public Double LongitudeAsRadians { get { return Longitude * RadiansOverDegrees; } }

        // Calculate the distance to this point from another
        public Double Distance(Coord from)
        {
            var diffLongitude = LongitudeAsRadians - from.LongitudeAsRadians;
            var diffLatitude = LatitudeAsRadians - from.LatitudeAsRadians;

            // Intermediate result before mapping to distance to the Earth size
            // in the appropriate units
            var interResult = Math.Pow(Math.Sin(diffLatitude / 2.0), 2.0) +
                          Math.Cos(from.LatitudeAsRadians) * Math.Cos(LatitudeAsRadians) *
                          Math.Pow(Math.Sin(diffLongitude / 2.0), 2.0);

            // Map the result to the Earth shape / Distance in miles
            var mappedResult = EarthMiles * 2.0 *
                          Math.Atan2(Math.Sqrt(interResult), Math.Sqrt(1.0 - interResult));

            return mappedResult;
        }

    }
}
