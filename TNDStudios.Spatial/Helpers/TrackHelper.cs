using GeoCoordinatePortable;
using System;
using System.Collections.Generic;

namespace Package.Helpers
{
    public class TrackHelper
    {

        public static Double CalculateTotalDistance(List<GeoCoordinate> points)
        {
            Double distance = 0D;

            // Loop the coords from start to finish missing the first 
            // to make sure we always have the end point one step ahead
            for (var coordId = 1; coordId < points.Count; coordId++)
            {
                distance += points[coordId].GetDistanceTo(points[coordId - 1]);
            }

            return distance;
        }
    }
}
