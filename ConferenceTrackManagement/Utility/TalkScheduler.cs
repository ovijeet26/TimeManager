
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ConferenceTrackManagement.Utility
{

    public class Talk
    {
        public Talk(string title, int duration)
        {
            Title = title;
            Duration = duration;
        }

        public string Title { get; private set; }
        public int Duration { get; private set; }
    }

    public class EventScheduler
    {
        const int Year = 2020;
        const int Month = 10;
        const int Day = 17;

        List<Schedule> schedules = new List<Schedule>();

        public void PrepareSchedule()
        {
            List<Talk> talks = new List<Talk>();

            talks.Add(new Talk("Writing Fast Tests Against Enterprise Rails", 60));
            talks.Add(new Talk("Overdoing it in Python", 45));
            talks.Add(new Talk("Lua for the Masses", 30));
            talks.Add(new Talk("Ruby Errors from Mismatched Gem Versions", 45));
            talks.Add(new Talk("Common Ruby Errors", 45));
            talks.Add(new Talk("Rails for Python Developers", 5));
            talks.Add(new Talk("Communicating Over Distance", 60));
            talks.Add(new Talk("Accounting-Driven Development", 45));
            talks.Add(new Talk("Woah", 30));
            talks.Add(new Talk("Sit Down and Write", 30));
            talks.Add(new Talk("Pair Programming vs Noise", 45));
            talks.Add(new Talk("Rails Magic", 60));
            talks.Add(new Talk("Ruby on Rails: Why We Should Move On", 60));
            talks.Add(new Talk("Clojure Ate Scala (on my project)", 45));
            talks.Add(new Talk("Programming in the Boondocks of Seattle", 30));
            talks.Add(new Talk("Ruby vs. Clojure for Back-End Development", 30));
            talks.Add(new Talk("Ruby on Rails Legacy App Maintenance", 60));
            talks.Add(new Talk("A World Without HackerNews", 30));
            talks.Add(new Talk("User Interface CSS in Rails Apps", 30));

            Schedule schedule = new Schedule(new DateTime(Year, Month, Day, 9, 0, 0), new DateTime(Year, Month, Day, 18, 0, 0));
            schedule.AddFixEvent(new DateTime(Year, Month, Day, 12, 0, 0), "Lunch", 60);
            schedule.AddFixEvent(new DateTime(Year, Month, Day, 17, 0, 0), "Networking Event", 60);

            foreach (var talk in talks)
            {
                try
                {
                    schedule.AddTalkEvent(talk.Title, talk.Duration);
                }
                catch (Exception ex)
                {
                    if (ex is ScheduleFullException || ex is EventOutOfScheduleRangeException)
                    {
                        // 
                        schedules.Add(schedule);
                        schedule = new Schedule(new DateTime(Year, Month, Day, 9, 0, 0), new DateTime(Year, Month, Day, 18, 0, 0));
                        schedule.AddFixEvent(new DateTime(Year, Month, Day, 12, 0, 0), "Lunch", 60);
                        schedule.AddFixEvent(new DateTime(Year, Month, Day, 17, 0, 0), "Networking Event", 60);
                        schedule.AddTalkEvent(talk.Title, talk.Duration);
                        continue;
                    }

                    throw;
                }
            }

            schedules.Add(schedule);
        }

        public void PrintSchedule()
        {
            int trackCounter = 1;

            foreach (var schedule in schedules)
            {
                Console.WriteLine($"Track {trackCounter}");
                Console.WriteLine(schedule.ToString());
                trackCounter++;
            }
        }
    }

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

        public void AddFixEvent(FixEvent breakTime)
        {
            ValidateScheduleStartTime(breakTime.StartTime);

            schedules.Add(breakTime.StartTime, breakTime);
        }

        public void AddFixEvent(DateTime startTime, string title, int durationInMinutes)
        {
            ValidateScheduleStartTime(startTime);
            schedules.Add(startTime, new FixEvent(startTime, title, durationInMinutes));
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

    public class ScheduleFullException : Exception
    {
        public ScheduleFullException()
        {

        }

        public ScheduleFullException(string message) : base(message)
        {

        }
    }

    public class EventOutOfScheduleRangeException : Exception
    {

    }

    public interface IEvent
    {
        DateTime StartTime { get; }
        string Title { get; }
        int DurationInMinutes { get; }
        string ToString();
    }

    public class TalkEvent : IEvent
    {
        private DateTime _startTime;
        private string _title;
        private int _durationInMinutes;

        public TalkEvent(DateTime startTime, string title, int durationInMinutes)
        {
            StartTime = startTime;
            Title = title;
            DurationInMinutes = durationInMinutes;
        }

        /// <summary>
        /// Note: This will have only time when Talk will start.
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            private set { _startTime = value; }
        }

        public string Title
        {
            get { return _title; }
            private set { _title = value; }
        }

        public int DurationInMinutes
        {
            get { return _durationInMinutes; }
            private set { _durationInMinutes = value; }
        }

        public override string ToString()
        {
            return $"{StartTime.ToString("t")} {Title} {DurationInMinutes}min";
        }
    }

    public class FixEvent : IEvent
    {
        private DateTime _startTime;
        private string _title;
        private int _durationInMinutes;

        public FixEvent(DateTime startTime, string title, int durationInMinutes)
        {
            StartTime = startTime;
            Title = title;
            DurationInMinutes = durationInMinutes;
        }

        /// <summary>
        /// Note: This will have only time when Talk will start.
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            private set { _startTime = value; }
        }

        public string Title
        {
            get { return _title; }
            private set { _title = value; }
        }

        public int DurationInMinutes
        {
            get { return _durationInMinutes; }
            private set { _durationInMinutes = value; }
        }

        public override string ToString()
        {
            return $"{StartTime.ToString("t")} {Title}";
        }
    }
}
