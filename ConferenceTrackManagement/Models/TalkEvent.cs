using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement.Models
{
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
}
