using GeoCoordinatePortable;
using Package.Common;
using Package.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using TNDStudios.Spatial.Documents;
using Xunit;

namespace Package.Tests
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
        public void GPX_Track_Calculate_Distance()
        {
            // ARRANGE
            List<GeoCoordinate> points = gpxFile.Tracks[0].TrackSegments[0].ToCoords();

            // ACT
            Double distance = Math.Round(TrackHelper.CalculateTotalDistance(points) / 1000, 2);

            // ASSERT
            Assert.True(distance == 21.37D);
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
    }
}
