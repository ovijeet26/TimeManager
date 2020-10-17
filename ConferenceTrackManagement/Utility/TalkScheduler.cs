using ConferenceTrackManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement.Utility
{
    class TalkScheduler
    {

        public Dictionary<int, Track> ScheduleTalks(List<Talk> events)
        {
            Dictionary<int, Track> tracks = new Dictionary<int, Track>();

            int totalEventTime = TotalTalksDuration(events);

            int trackCount = TotalTracksCount(totalEventTime);

            List<Talk> talks = AscendingListOfTalks(events);
            
            //Iterating over number of Tracks.
            for (int track = 1; track <= trackCount; track++)
            {
                tracks[track] = new Track();
                //9 AM to 12 PM
                int morningSessionTimeRemaining = 180;
                //1 PM to 5 PM
                int eveningSessionTimeRemaining = 240;
                //Iterating over the list of Talks.
                for (int i=0;i<talks.Count;i++)
                {
                    if (morningSessionTimeRemaining - talks[i].Duration >= 0)
                    {
                        tracks[track].PreLunchTalks.Add(talks[i]);
                        morningSessionTimeRemaining -= talks[i].Duration;
                        talks.Remove(talks[i]);
                        i--;
                        continue;
                    }
                    else if(eveningSessionTimeRemaining - talks[i].Duration >= 0)
                    {
                        tracks[track].PostLunchTalks.Add(talks[i]);
                        eveningSessionTimeRemaining -= talks[i].Duration;
                        talks.Remove(talks[i]);
                        i--;
                        continue;
                    }
                    //Cannot fit anymore talks in this Track.
                    else
                    {
                        break;
                    }
                }

            }
            return tracks;
        }
        /// <summary>
        /// Total duration of the talks.
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        private int TotalTalksDuration(List<Talk> events)
        {
            int totalEventTime = 0;
            foreach (var e in events)
            {
                totalEventTime += e.Duration;
            }
            return totalEventTime;
        }
        /// <summary>
        /// Total number of tracks the Talks need to be divided in.
        /// </summary>
        /// <param name="totalTalksDuration"></param>
        /// <returns></returns>
        private int TotalTracksCount(int totalTalksDuration)
        {
            int maxOneTrackCapacity = (3 + 4) * 60;
            float fraction = ((float)totalTalksDuration / (float)maxOneTrackCapacity);
            int trackCount = (int)(Math.Ceiling(fraction));
            return trackCount;
        }
        /// <summary>
        /// Return the Talk List in descending order of duration.
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        private List<Talk> AscendingListOfTalks(List<Talk> events)
        {
            return events.OrderBy(o => o.Duration).ToList();
        }
    }
}
