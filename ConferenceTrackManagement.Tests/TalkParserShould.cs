using Xunit;
using System.Linq;
using ConferenceTrackManagement.Models;
using ConferenceTrackManagement.Utility;
using System.Collections.Generic;

namespace ConferenceTrackManagement.Tests
{
    public class TalkParserShould
    {
        [Theory]
        [InlineData(new string[] { "Ovijeet adding some testcases to this", "sample input 20min" }, 0, 0)]
        public void EnsureDataIntegrity1(string[] arr, int iteratingIndex, int delimiterPos)
        {
            //Arrange
            TalkParser parser = new TalkParser();
            //Act
            parser.EnsureDataIntegrity(arr, iteratingIndex, delimiterPos);
            //Assert
            Assert.Equal("Ovijeet adding some testcases to this sample input 20min", arr[1]);
        }
        [Theory]
        [InlineData(new string[] { "Programming in the Boondocks of Seattle 30min Ruby vs. Clojure for Back-End",
        "Development 30min Ruby on Rails Legacy App Maintenance 60min" }, 0, 46)]
        public void EnsureDataIntegrity2(string[] arr, int iteratingIndex, int delimiterPos)
        {
            //Arrange
            TalkParser parser = new TalkParser();
            //Act
            parser.EnsureDataIntegrity(arr, iteratingIndex, delimiterPos);
            //Assert
            Assert.Equal("Ruby vs. Clojure for Back-End Development 30min Ruby on Rails Legacy App Maintenance 60min", arr[1]);
        }

        [Fact]
        public void ExtractEvents()
        {
            //Arrange
            string[] inputArr = { "Introduction lightning","Ovijeet adding some testcases to this", "sample input 20min","Programming in the Boondocks of Seattle 30min Ruby vs. Clojure for Back-End", "Development 30min Ruby on Rails Legacy App Maintenance 60min" };
            TalkParser parser = new TalkParser();
            //Act 
            List<Talk> finalList = parser.ExtractEvents(inputArr);
            //Assert
            Assert.Equal(5, finalList.Count);
            Assert.NotNull(finalList.Where(x => x.Title == "Introduction" && x.Duration == 5));
            Assert.NotNull(finalList.Where(x => x.Title == "Ovijeet adding some testcases to this sample input" && x.Duration == 20)); 
            Assert.NotNull(finalList.Where(x => x.Title == "Programming in the Boondocks of Seattle" && x.Duration == 30));
            Assert.NotNull(finalList.Where(x => x.Title == "Ruby vs. Clojure for Back-End Development" && x.Duration == 30));
            Assert.NotNull(finalList.Where(x => x.Title == "Ruby on Rails Legacy App Maintenance" && x.Duration == 60));
        }
    }
}
