using System;
using System.Collections.Generic;
using TNDStudios.Spatial.Documents;
using TNDStudios.Spatial.Helpers;
using TNDStudios.Spatial.Types;
using Xunit;

namespace TNDStudios.Spatial.Tests
{
    public class GeographyTests : TestBase
    {
        private readonly GeoFile geoTrackFile;
        private readonly GeoFile geoFile;
        private readonly GeoFile geoCompare1;
        private readonly GeoFile geoCompare2;

        public GeographyTests()
        {
            geoTrackFile = base.GetXMLData<GPXFile>("GPXFiles/GPXRouteOnly.gpx").ToGeoFile();
            geoFile = base.GetXMLData<GPXFile>("GPXFiles/HalfMarathon.gpx").ToGeoFile();
            geoCompare1 = base.GetXMLData<GPXFile>("GPXFiles/Compare1.gpx").ToGeoFile();
            geoCompare2 = base.GetXMLData<GPXFile>("GPXFiles/Compare2.gpx").ToGeoFile();
        }

        [Fact]
        public void Calculate_Distance()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = geoFile.Routes[0].Points;

            // ACT
            Double distance = Math.Round(points.CalculateTotalDistance() / 1000, 2);

            // ASSERT
            Assert.True(distance == 21.37D);
        }

        [Fact]
        public void Calculate_Actual_Time()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = geoFile.Routes[0].Points;

            // ACT]
            TimeSpan calculatedSpan = points.TotalTime(TimeCalculationType.ActualTime);

            // ASSERT
            Assert.Equal(133.0, Math.Floor(calculatedSpan.TotalMinutes));
        }

        [Fact]
        public void Calculate_Moving_Time()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = geoFile.Routes[0].Points;

            // ACT]
            TimeSpan calculatedSpan = points.TotalTime(TimeCalculationType.MovingTime);

            // ASSERT
            Assert.Equal(124.0, Math.Floor(calculatedSpan.TotalMinutes));
        }

        [Fact]
        public void Track_Array_Mapping_Success()
        {
            // ARRANGE
    
            // ACT
            Int32 trackCount = (Int32)geoTrackFile?.Routes?.Count;
            Int32 trackpointCount = (Int32)geoTrackFile?.Routes[0].Points.Count;

            // ASSERT
            Assert.Equal(1, trackCount);
            Assert.True(trackpointCount > 1);
        }

        [Fact]
        public void Track_Array_To_Coords()
        {
            // ARRANGE

            // ACT
            Int32 coordCount = (Int32)geoTrackFile.Routes[0].Points.Count;

            // ASSERT
            Assert.Equal(523, coordCount);
        }

        [Fact]
        public void Track_Compare_Positive()
        {
            // ARRANGE
            List<GeoCoordinateExtended> compare1 = geoCompare1.Routes[0].Points;
            List<GeoCoordinateExtended> compare2 = geoCompare1.Routes[0].Points;

            // ACT
            Double score = compare1.Compare(compare2, ActivityType.Running);

            // ASSERT
            Assert.Equal(1.0D, score); // Should be a perfect match
        }

        [Fact]
        public void Track_Compare_Negative()
        {
            // ARRANGE
            List<GeoCoordinateExtended> compare1 = geoCompare1.Routes[0].Points;
            List<GeoCoordinateExtended> compare2 = geoTrackFile.Routes[0].Points;

            // ACT
            Double score = compare1.Compare(compare2, ActivityType.Running);

            // ASSERT
            Assert.Equal(0.0D, score); // Should be a total mismatch
        }

        [Fact]
        public void Track_Compare_Near()
        {
            // ARRANGE
            List<GeoCoordinateExtended> compare1 = geoCompare1.Routes[0].Points;
            List<GeoCoordinateExtended> compare2 = geoCompare2.Routes[0].Points;

            // ACT
            Double score = compare1.Compare(compare2, ActivityType.Running);

            // ASSERT
            Assert.True(score > 0.75); // Should be a partial match
        }

        [Fact]
        public void Round_Coordinates_By_Meters()
        {
            // ARRANGE
            GeoCoordinateExtended source = geoFile.Routes[0].Points[0];
            GeoCoordinateExtended compareTo = source.Clone();
            Double roundingMeters = 2D;

            // ACT
            compareTo.Round(roundingMeters); // Round second coordinate to 2 meter grid point
            Double distance = compareTo.GetDistanceTo(source); // Calculate the distance in meters 
            Double hypotenuse = Math.Sqrt(Math.Pow(roundingMeters, 2) + Math.Pow(roundingMeters, 2));

            // ASSERT
            Assert.True(distance < hypotenuse); // Should be smaller than the hypotenuse
        }
    }
}
