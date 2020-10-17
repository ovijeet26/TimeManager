using System.Collections.Generic;

namespace ConferenceTrackManagement.Models
{
    /// <summary>
    /// Track class ccontaining two lists for talks before and after lunch.
    /// </summary>
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
