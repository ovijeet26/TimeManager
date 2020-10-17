using System;

namespace ConferenceTrackManagement.Models
{

    public class FixedEvent : Event
    {
        public FixedEvent(DateTime startTime, string title, int durationInMinutes) : base(startTime, title, durationInMinutes)
        {

        }

        public override string ToString()
        {
            return $"{StartTime.ToString("t")} {Title}";
        }
    }
}
