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
            TimeSpan totalTime = points.CalculateSpeeds().TotalTime(TimeCalculationType.ActualTime);
            TimeSpan movingTime = points.CalculateSpeeds().TotalTime(TimeCalculationType.MovingTime);
            TimeSpan difference = totalTime - movingTime;
            List<GeoCoordinateExtended> cleanedPoints = points.RemoveNotMoving();
            TimeSpan cleanedTime = cleanedPoints.CalculateSpeeds().TotalTime(TimeCalculationType.ActualTime);

            // ASSERT
            Assert.Equal(movingTime, cleanedTime);
        }

    }
}
