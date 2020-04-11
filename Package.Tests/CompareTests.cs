using System;
using TNDStudios.Spatial.Documents;
using TNDStudios.Spatial.Helpers;
using TNDStudios.Spatial.Types;
using Xunit;

namespace TNDStudios.Spatial.Tests
{
    public class CompareTests : TestBase
    {
        // Same data in different formats
        private readonly TCXFile tcxTrackFile;
        private readonly GPXFile gpxTrackFile;

        public CompareTests()
        {
            tcxTrackFile = base.GetXMLData<TCXFile>("TCXFiles/HalfMarathon.tcx");
            gpxTrackFile = base.GetXMLData<GPXFile>("GPXFiles/HalfMarathon.gpx");
        }

        [Fact]
        public void File_Format_Compare_Distance()
        {
            // ARRANGE
            GeoFile tcxConversion = tcxTrackFile.ToGeoFile();
            GeoFile gpxConversion = gpxTrackFile.ToGeoFile();

            // ACT
            Double tcxDistance = Math.Round(tcxConversion.Routes[0].Points.CalculateTotalDistance(), 0);
            Double gpxDIstance = Math.Round(gpxConversion.Routes[0].Points.CalculateTotalDistance(), 0);

            // ASSERT
            Assert.Equal(tcxDistance, gpxDIstance);
        }

        [Fact]
        public void File_Format_Compare_Actual_Time()
        {
            // ARRANGE
            GeoFile tcxConversion = tcxTrackFile.ToGeoFile();
            GeoFile gpxConversion = gpxTrackFile.ToGeoFile();
            TimeSpan tcxSpeed;
            TimeSpan gpxSpeed;

            // ACT
            tcxSpeed = tcxConversion.Routes[0].Points.CalculateSpeeds().TotalTime(TimeCalculationType.ActualTime);
            gpxSpeed = gpxConversion.Routes[0].Points.CalculateSpeeds().TotalTime(TimeCalculationType.ActualTime);

            // ASSERT
            Assert.Equal(Math.Floor(tcxSpeed.TotalMinutes), Math.Floor(gpxSpeed.TotalMinutes));
        }

        [Fact]
        public void File_Format_Compare_Moving_Time()
        {
            // ARRANGE
            GeoFile tcxConversion = tcxTrackFile.ToGeoFile();
            GeoFile gpxConversion = gpxTrackFile.ToGeoFile();
            TimeSpan tcxSpeed;
            TimeSpan gpxSpeed;

            // ACT
            tcxSpeed = tcxConversion.Routes[0].Points.CalculateSpeeds().TotalTime(TimeCalculationType.MovingTime);
            gpxSpeed = gpxConversion.Routes[0].Points.CalculateSpeeds().TotalTime(TimeCalculationType.MovingTime);

            // ASSERT
            Assert.Equal(Math.Floor(tcxSpeed.TotalMinutes), Math.Floor(gpxSpeed.TotalMinutes));
        }
    }
}
