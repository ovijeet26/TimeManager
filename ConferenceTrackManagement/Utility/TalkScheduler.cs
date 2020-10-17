using ConferenceTrackManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement.Utility
{
    class TalkScheduler
    {
        public Dictionary<int, Track> ScheduleTalks(Dictionary<int, List<string>> events)
        {
            Dictionary<int, Track> tracks = new Dictionary<int, Track>();

            int totalEventTime = 0;
            foreach (var e in events)
            {
                totalEventTime += e.Key * e.Value.Count;
            }

            int maxOneTrackCapacity = (3 + 4) * 60;
            float fraction = ((float)totalEventTime / (float)maxOneTrackCapacity);
            int trackCount = (int)(Math.Ceiling(fraction));

            List<Talk> talks = DescendingListOfTalks(events);

            for (int track = 1; track <= trackCount; track++)
            {
                tracks[track] = new Track();
                int morningSessionTimeRemaining = 180;
                int eveningSessionTimeRemaining = 240;
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
                    else
                    {
                        break;
                    }
                }

            }
            return tracks;
        }

        private List<Talk> DescendingListOfTalks(Dictionary<int, List<string>> events)
        {
            List<Talk> talks = new List<Talk>();
            foreach(var e in events)
            {
                foreach(var talk in e.Value)
                {
                    Talk t = new Talk(e.Key,talk);
                    talks.Add(t);
                }
            }
            return talks.OrderByDescending(o => o.Duration).ToList();
        }
    }
}
