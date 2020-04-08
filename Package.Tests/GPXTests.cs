using System;
using TNDStudios.Spatial.Documents;
using Xunit;

namespace TNDStudios.Spatial.Tests
{
    public class GPXTests : TestBase
    {
        private readonly GPXFile gpxTrackFile;

        public GPXTests()
        {
            gpxTrackFile = base.GetXMLData<GPXFile>("GPXFiles/GPXRouteOnly.gpx");
        }

        [Fact]
        public void Track_Compare_Conversion()
        {
            // ARRANGE
            Int32 origionalCount = 0;
            Int32 transformedCount = 0;

            // ACT
            origionalCount = gpxTrackFile.Tracks[0].TrackSegments[0].TrackPoints.Count; // Count of origional
            transformedCount = gpxTrackFile.ToGeoFile().Routes[0].Points.Count; // Do conversion and count

            // ASSERT
            Assert.Equal(origionalCount, transformedCount);
        }
    }
}
