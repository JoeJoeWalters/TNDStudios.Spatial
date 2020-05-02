using System;
using System.Collections.Generic;
using TNDStudios.Spatial.Documents;
using TNDStudios.Spatial.Helpers;
using TNDStudios.Spatial.Types;
using Xunit;

namespace TNDStudios.Spatial.Tests
{
    public class CleanTests : TestBase
    {
        private readonly GeoFile trackFile;

        public CleanTests()
        {
            trackFile = base.GetXMLData<GPXFile>("GPXFiles/HalfMarathon.gpx").ToGeoFile();
        }

        [Fact]
        public void Remove_Not_Moving_Time()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = trackFile.Routes[0].Points;

            // ACT
            TimeSpan totalTime = points.TotalTime(TimeCalculationType.ActualTime); // Work out the actual time of the track
            TimeSpan movingTime = points.TotalTime(TimeCalculationType.MovingTime); // Work out the moving time of the track
            TimeSpan difference = totalTime - movingTime; // Calculate the not moving time
            List<GeoCoordinateExtended> cleanedPoints = points.RemoveNotMoving(); // Call the process to remove the not moving time and heal
            TimeSpan cleanedTime = cleanedPoints.TotalTime(TimeCalculationType.ActualTime); // Calculate the new actual time of the track

            // ASSERT
            Assert.Equal(movingTime, cleanedTime); // The old moving time should be the new actual time
        }

    }
}
