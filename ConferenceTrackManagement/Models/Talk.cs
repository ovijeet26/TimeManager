namespace ConferenceTrackManagement.Models
{
    /// <summary>
    /// Talk class containing two properties for Duration and Title.
    /// </summary>
    public class Talk
    {
        public Talk(string title,int duration)
        {
            Duration = duration;
            Title = title;
        }
        public int Duration { get;  }
        public string Title { get;  }
    }
}
