using TNDStudios.Spatial.Documents;
using TNDStudios.Spatial.Helpers;
using TNDStudios.Spatial.Types;
using System;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace TNDStudios.Spatial.Tests
{
    public class TCXTests : TestBase
    {
        private readonly TCXFile tcxTrackFile;

        public TCXTests()
        {
            tcxTrackFile = base.GetXMLData<TCXFile>("TCXFiles/HalfMarathon.tcx");
        }

        [Fact]
        public void Track_Compare_Conversion()
        {
            // ARRANGE
            Int32 origionalCount = 0;
            Int32 transformedCount = 0;

            // ACT
            tcxTrackFile.Activities.Activity[0].Laps.ForEach(lap => origionalCount += lap.Track.TrackPoints.Count); // Count of origional
            transformedCount = tcxTrackFile.ToGeoFile().Routes[0].Points.Count; // Do conversion and count

            // ASSERT
            Assert.Equal(origionalCount, transformedCount);
        }
    }
}
