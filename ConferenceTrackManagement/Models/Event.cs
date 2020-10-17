using System;

namespace ConferenceTrackManagement.Models
{
    public abstract class Event
    {
        public Event(DateTime startTime, string title, int durationInMinutes)
        {
            StartTime = startTime;
            Title = title;
            DurationInMinutes = durationInMinutes;
        }

        public DateTime StartTime { get; }
        public string Title { get; }
        public int DurationInMinutes { get; }
        public override string ToString()
        {
            return $"{StartTime.ToString("t")} {Title} {DurationInMinutes}min";
        }
    }
}
