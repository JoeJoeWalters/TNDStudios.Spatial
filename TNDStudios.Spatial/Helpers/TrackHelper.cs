using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TNDStudios.Spatial.Documents;
using TNDStudios.Spatial.Types;

namespace TNDStudios.Spatial.Helpers
{
    public static class TrackHelper
    {
        public static Double EarthRadius = 40010040D; // What is the earth's radius in meters
        public static Double LatitudeDistance = EarthRadius / 360.0D; // What is 1 degree of latitude

        public static List<GeoCoordinateExtended> InfillPositions(this List<GeoCoordinateExtended> points)
        {
            GeoCoordinateExtended lastValidPosition = null;
            points.ForEach(pt => 
            {
                // Not a bad coordinate?
                if (!pt.BadCoordinate) 
                {
                    lastValidPosition = pt; // Assign this as the last known good position 
                }
                else if (pt.BadCoordinate && lastValidPosition != null) 
                { 
                    // Infill the position from the last known good
                    pt.Latitude = lastValidPosition.Latitude; 
                    pt.Longitude = lastValidPosition.Longitude;
                    pt.Altitude = lastValidPosition.Altitude;
                    pt.BadCoordinate = false;
                    lastValidPosition = pt; // Reassign this as the last known good
                }
            });

            return points;
        }

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

                    // Loop all points in the track and only count those that had speed (movement) between the two points
                    for (var coordId = 1; coordId < points.Count; coordId++)
                    {
                        // Any movement recorded?
                        if (points[coordId].Speed > 0)
                        {
                            result = result.Add(points[coordId].Time - points[coordId - 1].Time); // Add the timespan between the points to the total
                        }
                    }

                    break;
            }

            return result;
        }

        /// <summary>
        /// Clone an existing set of points so they can be modified breaking the reference to the origional list
        /// </summary>
        /// <param name="points">The set of points to clone</param>
        /// <returns>The new list of points cloned from the source</returns>
        public static List<GeoCoordinateExtended> Clone(this List<GeoCoordinateExtended> points)
            => JsonConvert.DeserializeObject<List<GeoCoordinateExtended>>(JsonConvert.SerializeObject(points)); // Serialise and then deserialise the object to break the references to new objects

        public static GeoCoordinateExtended Clone(this GeoCoordinateExtended coord)
            => JsonConvert.DeserializeObject<GeoCoordinateExtended>(JsonConvert.SerializeObject(coord));

        /// <summary>
        /// Take a set of points and modify them to be rounded to the nearest X meters
        /// </summary>
        /// <param name="points">The set of points to round</param>
        /// <param name="roundingMeters">The meter preceision to round to</param>
        /// <returns></returns>
        public static List<GeoCoordinateExtended> Round(this List<GeoCoordinateExtended> points, Double meters)
            => points.Clone().Select(coord => coord.Round(meters)).ToList();

        public static GeoCoordinateExtended Round(this GeoCoordinateExtended coord, Double meters)
        {
            // Coordinate offsets in radians
            Double latitudeMeters = coord.Latitude * LatitudeDistance;
            Double longitudeMeters = coord.Longitude * (EarthRadius * Math.Cos(coord.Latitude) / 360.0D);

            Double roundedLatitude = meters * Math.Round(latitudeMeters / meters, 0);
            Double roundedLongitude = meters * Math.Round(longitudeMeters / meters, 0);

            coord.Latitude = roundedLatitude / LatitudeDistance;
            coord.Longitude = roundedLongitude / (EarthRadius * Math.Cos(coord.Latitude) / 360.0D);

            return coord;
        }

        /// <summary>
        /// Compare one set of points to another to give a similarity score based on how common they are
        /// </summary>
        /// <param name="points">The set of points to compare</param>
        /// <param name="compareTo">The set of points to compare the list of points to</param>
        /// <param name="activityType">What type of activity is it (mainly to reduce or increase the comparison fuzziness)</param>
        /// <returns></returns>
        public static Double Compare(this List<GeoCoordinateExtended> points, List<GeoCoordinateExtended> compareTo, ActivityType activityType)
        {
            Double score = 0.0D;

            // Score based on if the 
            List<GeoCoordinateExtended> matches = points.Delta(compareTo, activityType, CompareType.Matches);

            score = (1.0 / points.Count) * matches.Count;
            score = (score < 0) ? 0 : score;

            return score;
        }

        /// <summary>
        /// Work out the delta between two sets of points rounded to a given activity type
        /// </summary>
        /// <param name="points">The set of points to compare</param>
        /// <param name="compareTo">The set of points to compare the list of points to</param>
        /// <param name="activityType">What type of activity is it (mainly to reduce or increase the comparison fuzziness)</param>
        /// <returns></returns>
        public static List<GeoCoordinateExtended> Delta(this List<GeoCoordinateExtended> points, List<GeoCoordinateExtended> compareTo, ActivityType activityType, CompareType compareType)
        {
            List<GeoCoordinateExtended> sourceRounded = points.Round(5D);
            List<GeoCoordinateExtended> compareRounded = compareTo.Round(5D);

            switch (compareType)
            {
                case CompareType.Matches:
                    return sourceRounded.Where(source => compareRounded.Any(compare => source == compare)).ToList();
                default:
                    return sourceRounded.Where(source => !compareRounded.Any(compare => source == compare)).ToList();
            }
        }
    }
}
