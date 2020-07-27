using FluentAssertions;
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
        public void Track_Compare_ToGeoFile_Conversion()
        {
            // ARRANGE
            Int32 origionalCount = 0;
            Int32 transformedCount = 0;

            // ACT
            origionalCount = gpxTrackFile.Tracks[0].TrackSegments[0].TrackPoints.Count; // Count of origional
            transformedCount = gpxTrackFile.ToGeoFile().Routes[0].Points.Count; // Do conversion and count

            // ASSERT
            origionalCount.Should().Be(transformedCount);
        }

        [Fact]
        public void Track_Compare_FromGeoFile_Conversion()
        {
            // ARRANGE
            Int32 transformedCount = 0;
            GeoFile geoFile = gpxTrackFile.ToGeoFile();
            Int32 origionalCount = geoFile.Routes[0].Points.Count;
            GPXFile gpxFile = new GPXFile();

            // ACT
            Boolean success = gpxFile.FromGeoFile(geoFile);
            transformedCount = gpxFile.Routes[0].RoutePoints.Count; // Count of transformed track

            // ASSERT
            success.Should().BeTrue();
            origionalCount.Should().Be(transformedCount);
        }
    }
}
