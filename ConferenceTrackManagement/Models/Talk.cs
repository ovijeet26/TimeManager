namespace ConferenceTrackManagement.Models
{
    public class Talk
    {
        public Talk(int duration, string title)
        {
            Duration = duration;
            Title = title;
        }
        public int Duration { get;  }
        public string Title { get;  }
    }
}
