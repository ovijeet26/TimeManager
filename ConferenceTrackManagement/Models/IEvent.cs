using System;

namespace ConferenceTrackManagement.Models
{
    public interface IEvent
    {
        DateTime StartTime { get; }
        string Title { get; }
        int DurationInMinutes { get; }
        string ToString();
    }
}
