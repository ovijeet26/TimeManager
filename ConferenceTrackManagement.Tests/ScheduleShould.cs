
using Xunit;
using System;
using System.Linq;
using ConferenceTrackManagement.Models;
using ConferenceTrackManagement.CustomExceptions;

namespace ConferenceTrackManagement.Tests
{
    public class ScheduleShould
    {
        [Fact]
        public void CreateScheduleWithStartDateAndEndDate()
        {
            //Arrange
            DateTime expectedStartTime = new DateTime(2020, 10, 17, 09, 00, 00);
            DateTime expectedEndTime = new DateTime(2020, 10, 17, 18, 00, 00);
            //Act
            Schedule schedule = GetSchedule();
            //Assert
            Assert.Equal(expectedStartTime, schedule.StartTime);
            Assert.Equal(expectedEndTime, schedule.EndTime);

        }

        [Fact]
        public void AddFixedEvent1()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            FixedEvent lunch = new FixedEvent(new DateTime(2020, 10, 17, 12, 00, 00), "Lunch", 60);
            //Act
            schedule.AddFixedEvent(lunch);
            //Assert
            Assert.Single(schedule.events);
            Assert.Equal("lunch", schedule.events.First().Title, true);
            Assert.Equal(60, schedule.events.First().DurationInMinutes);
            Assert.Equal("12:00 PM", schedule.events.First().StartTime.ToString("t"), true);
        }

        [Fact]
        public void AddFixedEvent2()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            //Act
            schedule.AddFixedEvent(new DateTime(2020, 10, 17, 12, 00, 00), "Lunch", 60);
            //Assert
            Assert.Single(schedule.events);
            Assert.Equal("lunch", schedule.events.First().Title, true);
            Assert.Equal(60, schedule.events.First().DurationInMinutes);
            Assert.Equal("12:00 PM", schedule.events.First().StartTime.ToString("t"), true);
        }


        [Fact]
        public void AddFixedEventWithInvalidStartDate1()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            //Act
            Action act = () => schedule.AddFixedEvent(new DateTime(2020, 10, 17, 07, 00, 00), "Lunch", 60);
            //Assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal($"Start time cannot be before event starts.", exception.Message);
        }

        [Fact]
        public void AddFixedEventWithInvalidStartDate2()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            //Act
            Action act = () => schedule.AddFixedEvent(new DateTime(2020, 10, 17, 23, 00, 00), "Lunch", 60);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            //Assert
            Assert.Equal($"Start time cannot be after event ends.", exception.Message);
        }

        [Fact]
        public void AddTalkEvent()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            //Act
            schedule.AddFixedEvent(new DateTime(2020, 10, 17, 12, 00, 00), "Lunch", 60);
            schedule.AddTalkEvent("Test case demo", 90);
            //Assert
            Assert.Equal(2, schedule.events.Count());
            Assert.Equal("Test case demo", schedule.events.First().Title, true);
            Assert.Equal(90, schedule.events.First().DurationInMinutes);
        }

        [Fact]
        public void AddTalkEvent_Invalid()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            //Act
            schedule.AddFixedEvent(new DateTime(2020, 10, 17, 12, 00, 00), "Lunch", 60);
            schedule.AddFixedEvent(new DateTime(2020, 10, 17, 17, 00, 00), "Networking Event", 60);
            schedule.AddTalkEvent("First half", 180);
            schedule.AddTalkEvent("Second half", 200);
            Action act = () => schedule.AddTalkEvent("Test case demo", 60);
            //Assert
            EventOutOfScheduleRangeException exception = Assert.Throws<EventOutOfScheduleRangeException>(act);
        }

        [Fact]
        public void AddTalkEvent_ScheduleFull()
        {
            //Arrange
            Schedule schedule = GetSchedule();
            //Act
            schedule.AddFixedEvent(new DateTime(2020, 10, 17, 12, 00, 00), "Lunch", 60);
            schedule.AddFixedEvent(new DateTime(2020, 10, 17, 17, 00, 00), "Networking Event", 60);
            schedule.AddTalkEvent("First half", 180);
            schedule.AddTalkEvent("Second half", 240);
            Action act = () => schedule.AddTalkEvent("Test case demo", 30);
            //Assert
            ScheduleFullException exception = Assert.Throws<ScheduleFullException>(act);
        }

        private Schedule GetSchedule()
        {
            //Arrange
            //Act
            //Assert
            DateTime startTime = new DateTime(2020, 10, 17, 09, 00, 00);
            DateTime endTime = new DateTime(2020, 10, 17, 18, 00, 00);
            return new Schedule(startTime, endTime);
        }
    }
}
