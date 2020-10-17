using System.Collections.Generic;

namespace ConferenceTrackManagement.Models
{
    public class Track
    {
        public Track()
        {
            PreLunchTalks = new List<Talk>();
            PostLunchTalks = new List<Talk>();
        }
        public List<Talk> PreLunchTalks { get; set; }
        public List<Talk> PostLunchTalks { get; set; }
    }
}
