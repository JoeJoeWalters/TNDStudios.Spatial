using System;
using TNDStudios.Spatial.Documents;
using Xunit;

namespace Package.Tests
{
    public class GPXTests : TestBase
    {
        [Fact]
        public void LoadGPX()
        {
            // ARRANGE
            GPXFile gpxFile = base.GetXMLData<GPXFile>("GPXFiles/GPXRouteOnly.gpx");

            // ACT
            var pointCount = 1;

            // ASSERT
        }
    }
}
