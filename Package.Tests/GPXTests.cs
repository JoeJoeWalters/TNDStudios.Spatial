using System;
using TNDStudios.Spatial.Documents;
using Xunit;

namespace Package.Tests
{
    public class GPXTests : TestBase
    {
        private readonly GPXFile gpxFile;

        public GPXTests()
        {
            gpxFile = base.GetXMLData<GPXFile>("GPXFiles/GPXRouteOnly.gpx");
        }

        [Fact]
        public void GPX_Track_Array_Mapping_Success()
        {
            // ARRANGE
    
            // ACT
            Int32 trackCount = (Int32)gpxFile?.Tracks?.Count;
            Int32 segmentCount = (Int32)gpxFile?.Tracks?[0].TrackSegments?.Count;
            Int32 trackpointCount = (Int32)gpxFile?.Tracks?[0].TrackSegments?[0].TrackPoints.Count;

            // ASSERT
            Assert.Equal(1, trackCount);
            Assert.Equal(1, segmentCount);
            Assert.True(trackpointCount > 1);
        }
    }
}
