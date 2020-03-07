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
            List<Coord> points = gpxFile.Tracks[0].TrackSegments[0].TrackPoints.Select(pt => pt.ToCoord()).ToList();

            // ACT
            Double distance = TrackHelper.CalculateDistance(points);

            // ASSERT
            Assert.True(distance > 13.25D && distance < 13.27D);
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
    }
}
