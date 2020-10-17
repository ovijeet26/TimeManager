
using System;

namespace ConferenceTrackManagement.Models
{
    public class TalkEvent : Event
    {
        public TalkEvent(DateTime startTime, string title, int durationInMinutes) : base(startTime, title, durationInMinutes)
        {

        }

        public override string ToString()
        {
            return $"{StartTime.ToString("t")} {Title} {DurationInMinutes}min";
        }
    }
}
