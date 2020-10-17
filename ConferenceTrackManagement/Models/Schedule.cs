
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ConferenceTrackManagement.CustomExceptions;

namespace ConferenceTrackManagement.Models
{
    public class Schedule
    {
        private DateTime _nextAvailableTime;
        protected SortedDictionary<DateTime, Event> schedules = null;
        public IEnumerable<Event> events => schedules.Values.AsEnumerable<Event>();

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// Set the start time and end time for a track for a day.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public Schedule(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            _nextAvailableTime = startTime;
            schedules = new SortedDictionary<DateTime, Event>();
        }
        /// <summary>
        /// Add a fixed event, that had a specified time and duration. e.g. Lunch 12 pm to 1 pm.
        /// </summary>
        /// <param name="breakTime"></param>
        public void AddFixedEvent(FixedEvent breakTime)
        {
            ValidateScheduleStartTime(breakTime.StartTime);
            schedules.Add(breakTime.StartTime, breakTime);
        }
        /// <summary>
        /// Add a fixed event, that had a specified time and duration. e.g. Lunch 12 pm to 1 pm.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="title"></param>
        /// <param name="durationInMinutes"></param>
        public void AddFixedEvent(DateTime startTime, string title, int durationInMinutes)
        {
            ValidateScheduleStartTime(startTime);
            schedules.Add(startTime, new FixedEvent(startTime, title, durationInMinutes));
        }
        /// <summary>
        /// Add a talk event that has been extracted from the raw user input.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="durationInMinutes"></param>
        public void AddTalkEvent(string title, int durationInMinutes)
        {
            // this is where the schedule manager algorithm will take place
            // currently we are using a simple logic to prepare schedule
            if (this.IsScheduleFull())
                throw new ScheduleFullException("Schedule is full for the day");

            DateTime availableTimeSlot = NextAvailableTimeSlot(durationInMinutes);

            //If the available time slot is beyond the end time, we raise an exception.
            if (availableTimeSlot >= this.EndTime)
                throw new EventOutOfScheduleRangeException();

            Event talkEvent = null;
            //Adding special case to handle the lightning event formatting.
            if (durationInMinutes <= 5)
            {
                talkEvent = new LightningTalkEvent(availableTimeSlot, title, durationInMinutes);
            }
            else
            {
                talkEvent = new TalkEvent(availableTimeSlot, title, durationInMinutes);
            }

            schedules.Add(availableTimeSlot, talkEvent);

            _nextAvailableTime = availableTimeSlot.AddMinutes(durationInMinutes);

            while (schedules.ContainsKey(_nextAvailableTime))
            {
                _nextAvailableTime = _nextAvailableTime.AddMinutes(schedules[_nextAvailableTime].DurationInMinutes);
            }
        }
        /// <summary>
        /// Returns the next valid available time slot.
        /// </summary>
        /// <param name="durationInMinutes"></param>
        /// <returns></returns>
        public DateTime NextAvailableTimeSlot(int durationInMinutes)
        {
            if (!schedules.Values.Any(s => s.StartTime >= _nextAvailableTime && s.StartTime < _nextAvailableTime.AddMinutes(durationInMinutes)))
            {
                return _nextAvailableTime;
            }

            var sch = schedules.Values.Last(s => s.StartTime >= _nextAvailableTime && s.StartTime < _nextAvailableTime.AddMinutes(durationInMinutes));

            return sch.StartTime.AddMinutes(sch.DurationInMinutes);
        }
        /// <summary>
        /// Check if the last talk ends exactly when the last Networking event starts.
        /// </summary>
        /// <returns></returns>
        public bool IsScheduleFull()
        {
            return _nextAvailableTime == EndTime;
        }
        /// <summary>
        /// Override the ToString() method to compose the string according to the requirement.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder schedules = new StringBuilder();

            foreach (var schedule in this.schedules.Values)
            {
                schedules.AppendLine(schedule.ToString());
            }

            return schedules.ToString();
        }
        /// <summary>
        /// Validate whether the time of the event obeys the constraints of the Track schedule.
        /// </summary>
        /// <param name="startTime"></param>
        private void ValidateScheduleStartTime(DateTime startTime)
        {
            if (startTime < StartTime)
                throw new InvalidOperationException($"Start time cannot be before event starts.");
            if (startTime > EndTime)
                throw new InvalidOperationException($"Start time cannot be after event ends.");
        }
    }
}
