using Xunit;
using System.Linq;
using ConferenceTrackManagement.Models;
using ConferenceTrackManagement.Utility;
using System.Collections.Generic;

namespace ConferenceTrackManagement.Tests
{
    public class EventSchedulerShould
    {
        [Fact]
        public void PrepareShedule()
        {
            //Arrange
            EventScheduler scheduler = new EventScheduler();
            List<Talk> talks = new List<Talk>()
            {
                new Talk("Introduction",5),
                new Talk("Ovijeet adding some testcases to this sample input", 20),
                new Talk("Programming in the Boondocks of Seattle", 30),
                new Talk("Ruby vs. Clojure for Back-End Development",30),
                new Talk("Ruby on Rails Legacy App Maintenance", 60)
            };

            //Act
            List<Schedule> schedules = scheduler.PrepareSchedule(talks);
            var events = schedules[0].events.ToList();
            //Assert
            Assert.Single(schedules);
            Assert.Equal(7,events.Count);
        }
    }
}
