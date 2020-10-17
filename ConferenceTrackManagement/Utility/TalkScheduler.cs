using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement.Utility
{
    class TalkScheduler
    {
        public Dictionary<int,List<string>> ScheduleTalks(Dictionary<int, List<string>> events)
        {
            Dictionary<int, List<string>> talks = new Dictionary<int, List<string>>();

            int totalEventTime = 0;
            foreach(var e in events)
            {
                totalEventTime += e.Key * e.Value.Count;
            }

            int maxOneTrackCapacity = (3 + 4) * 60;
            float fraction = ((float)totalEventTime / (float)maxOneTrackCapacity);
            int trackCount = (int)(Math.Ceiling(fraction));
            return talks;
        }
    }
}
