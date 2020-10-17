using System;


namespace ConferenceTrackManagement.CustomExceptions
{
    /// <summary>
    /// Custom exception to indicate that the next available event is beyond the end time. 
    /// </summary>
    public class EventOutOfScheduleRangeException : Exception
    {

    }
}
