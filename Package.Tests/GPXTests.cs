using TNDStudios.Spatial.Documents;
using TNDStudios.Spatial.Helpers;
using TNDStudios.Spatial.Types;
using System;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace TNDStudios.Spatial.Tests
{
    public class GPXTests : TestBase
    {
        private readonly GPXFile gpxTrackFile;
        private readonly GPXFile gpxFile;

        public GPXTests()
        {
            gpxTrackFile = base.GetXMLData<GPXFile>("GPXFiles/GPXRouteOnly.gpx");
            gpxFile = base.GetXMLData<GPXFile>("GPXFiles/HalfMarathon.gpx");
        }

        [Fact]
        public void GPX_Track_Segment_Calculate_Distance()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = gpxFile.Tracks[0].TrackSegments[0].ToCoords();

            // ACT
            Double distance = Math.Round(points.CalculateTotalDistance() / 1000, 2);

            // ASSERT
            Assert.True(distance == 21.37D);
        }

        [Fact]
        public void GPX_Track_Calculate_Distance()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = gpxFile.Tracks[0].ToCoords();

            // ACT
            Double distance = Math.Round(points.CalculateTotalDistance() / 1000, 2);

            // ASSERT
            Assert.True(distance == 21.37D);
        }

        [Fact]
        public void GPX_Track_Actual_Time()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = gpxFile.Tracks[0].ToCoords();

            // ACT]
            TimeSpan calculatedSpan = points.CalculateSpeeds().TotalTime(TimeCalculationType.ActualTime);

            // ASSERT
            Assert.Equal(133.0, Math.Floor(calculatedSpan.TotalMinutes));
        }

        [Fact]
        public void GPX_Track_Moving_Time()
        {
            // ARRANGE
            List<GeoCoordinateExtended> points = gpxFile.Tracks[0].ToCoords();

            // ACT]
            TimeSpan calculatedSpan = points.CalculateSpeeds().TotalTime(TimeCalculationType.MovingTime);

            // ASSERT
            Assert.Equal(124.0, Math.Floor(calculatedSpan.TotalMinutes));
        }

        [Fact]
        public void GPX_Track_Array_Mapping_Success()
        {
            // ARRANGE
    
            // ACT
            Int32 trackCount = (Int32)gpxTrackFile?.Tracks?.Count;
            Int32 segmentCount = (Int32)gpxTrackFile?.Tracks?[0].TrackSegments?.Count;
            Int32 trackpointCount = (Int32)gpxTrackFile?.Tracks?[0].TrackSegments?[0].TrackPoints.Count;

            // ASSERT
            Assert.Equal(1, trackCount);
            Assert.Equal(1, segmentCount);
            Assert.True(trackpointCount > 1);
        }

        [Fact]
        public void GPX_Track_Array_To_Coords()
        {
            // ARRANGE

            // ACT
            Int32 trackpointCount = (Int32)gpxTrackFile?.Tracks?[0].TrackSegments?[0].TrackPoints.Count;
            Int32 coordCount = (Int32)gpxTrackFile?.Tracks?[0].TrackSegments?[0].ToCoords().Count;

            // ASSERT
            Assert.Equal(trackpointCount, coordCount);
        }

        [Fact]
        public void GPX_Track_Compare_Positive()
        {
            // ARRANGE
            List<GeoCoordinateExtended> compare1 = gpxFile.Tracks[0].ToCoords();
            List<GeoCoordinateExtended> compare2 = gpxFile.Tracks[0].ToCoords();

            // ACT
            Double score = compare1.Compare(compare2, ActivityType.Running);

            // ASSERT
            Assert.Equal(100.0D, score); // Should be a perfect match
        }

        [Fact]
        public void GPX_Track_Compare_Negative()
        {
            // ARRANGE
            List<GeoCoordinateExtended> compare1 = gpxFile.Tracks[0].ToCoords();
            List<GeoCoordinateExtended> compare2 = gpxTrackFile.Tracks[0].ToCoords();

            // ACT
            Double score = compare1.Compare(compare2, ActivityType.Running);

            // ASSERT
            Assert.Equal(0.0D, score); // Should be a total mismatch
        }

        [Fact]
        public void GPX_Track_Compare_Partial()
        {
            // ARRANGE
            List<GeoCoordinateExtended> compare1 = gpxFile.Tracks[0].ToCoords();
            List<GeoCoordinateExtended> compare2 = gpxTrackFile.Tracks[0].ToCoords();

            // ACT
            Double score = compare1.Compare(compare2, ActivityType.Running);

            // ASSERT
            Assert.Equal(0.0D, score); // Should be a partial match
        }

        [Fact]
        public void GPX_Round_Coordinates_By_Meters()
        {
            // ARRANGE
            GeoCoordinateExtended source = gpxFile.Tracks[0].ToCoords()[2];
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
