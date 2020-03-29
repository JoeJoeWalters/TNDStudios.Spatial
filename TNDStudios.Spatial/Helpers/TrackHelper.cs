using GeoCoordinatePortable;
using Package.Documents;
using System;
using System.Collections.Generic;

namespace Package.Helpers
{
    public static class TrackHelper
    {
        public static List<GeoCoordinateExtended> CalculateSpeeds(this List<GeoCoordinateExtended> points)
        {
            // Loop the coords from start to finish missing the first 
            // to make sure we always have the end point one step ahead
            for (var coordId = 1; coordId < points.Count; coordId++)
            {
                if (coordId == 1) { points[0].Speed = 0; }
                else if (coordId > 1)
                {
                    // Calculate speed from last point to this one
                    points[coordId].CalculateSpeed(points[coordId - 1]);
                }
            }

            return points;
        }

        public static Double CalculateTotalDistance(this List<GeoCoordinateExtended> points)
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


        /// <summary>
        /// Calculates to the total time for the track in different ways
        /// depending on the calculation type, relies on the speeds having already been calculated by the appropriate method
        /// </summary>
        /// <param name="timeCalculationType"></param>
        /// <returns></returns>
        public static TimeSpan TotalTime(this List<GeoCoordinateExtended> points, TimeCalculationType timeCalculationType)
        {
            TimeSpan result = new TimeSpan();

            switch (timeCalculationType)
            {
                case TimeCalculationType.ActualTime:
                    result = points[points.Count - 1].Time - points[0].Time; // Difference in time between the first and last points in the track
                    break;

                case TimeCalculationType.MovingTime:

                    // Loop all points in the track
                    for (var coordId = 1; coordId < points.Count; coordId++)
                    {
                        if (points[coordId].Speed > 0)
                        {
                            result = result.Add(points[coordId].Time - points[coordId - 1].Time);
                        }
                    }

                    break;
            }

            return result;
        }
    }
}
