using System;

namespace ConferenceTrackManagement.CustomExceptions
{
    /// <summary>
    /// Custom exception to indicate that the last talk of the day ended just before the last event. 
    /// i.e. there is no more capacity to include any more talks to the current track.
    /// </summary>
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
