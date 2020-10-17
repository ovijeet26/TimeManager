using System;

namespace ConferenceTrackManagement.CustomExceptions
{
    public class ScheduleFullException : Exception
    {
        public ScheduleFullException()
        {

        }

        public ScheduleFullException(string message) : base(message)
        {

        }
    }
}
