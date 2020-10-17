using ConferenceTrackManagement.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConferenceTrackManagement.Models
{
    public class Schedule
    {
        private DateTime _nextAvailableTime;
        protected SortedDictionary<DateTime, IEvent> schedules = null;

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        /// <summary>
        /// Starting a Event of a Day, needs 
        /// start time and end time of the Event for that day.
        /// </summary>
        public Schedule(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            _nextAvailableTime = startTime;
            schedules = new SortedDictionary<DateTime, IEvent>();
        }

        public void AddFixEvent(FixedEvent breakTime)
        {
            ValidateScheduleStartTime(breakTime.StartTime);

            schedules.Add(breakTime.StartTime, breakTime);
        }

        public void AddFixEvent(DateTime startTime, string title, int durationInMinutes)
        {
            ValidateScheduleStartTime(startTime);
            schedules.Add(startTime, new FixedEvent(startTime, title, durationInMinutes));
        }

        public void AddTalkEvent(string title, int durationInMinutes)
        {
            // this is where the schedule manager algorithm will take place
            // currently we are using a simple logic to prepare schedule
            if (this.IsScheduleFull())
                throw new ScheduleFullException("Schedule is full for the day");

            DateTime availableTimeSlot = NextAvailableTimeSlot(durationInMinutes);

            if (availableTimeSlot >= this.EndTime)
                throw new EventOutOfScheduleRangeException();

            schedules.Add(availableTimeSlot, new TalkEvent(availableTimeSlot, title, durationInMinutes));

            _nextAvailableTime = availableTimeSlot.AddMinutes(durationInMinutes);
        }

        public DateTime NextAvailableTimeSlot(int durationInMinutes)
        {
            if (!schedules.Values.Any(s => s.StartTime >= _nextAvailableTime && s.StartTime < _nextAvailableTime.AddMinutes(durationInMinutes)))
            {
                return _nextAvailableTime;
            }

            var sch = schedules.Values.Last(s => s.StartTime >= _nextAvailableTime && s.StartTime < _nextAvailableTime.AddMinutes(durationInMinutes));

            return sch.StartTime.AddMinutes(sch.DurationInMinutes);
        }

        public bool IsScheduleFull()
        {
            return _nextAvailableTime == EndTime;
        }

        public override string ToString()
        {
            StringBuilder schedules = new StringBuilder();

            foreach (var schedule in this.schedules.Values)
            {
                schedules.AppendLine(schedule.ToString());
            }

            return schedules.ToString();
        }

        private void ValidateScheduleStartTime(DateTime startTime)
        {
            if (startTime < StartTime)
                throw new InvalidOperationException($"Start time cannot be before event starts.");
            if (startTime > EndTime)
                throw new InvalidOperationException($"Start time cannot be after event ends.");
        }
    }
}
