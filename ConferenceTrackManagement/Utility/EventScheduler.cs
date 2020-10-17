
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using ConferenceTrackManagement.Models;
using ConferenceTrackManagement.CustomExceptions;

namespace ConferenceTrackManagement.Utility
{

    public class EventScheduler
    {
        const int Year = 2020;
        const int Month = 10;
        const int Day = 17;

        public List<Schedule> PrepareSchedule(List<Talk> talks)
        {
            List<Schedule> schedules = new List<Schedule>();
            Schedule schedule = new Schedule(new DateTime(Year, Month, Day, 9, 0, 0), new DateTime(Year, Month, Day, 18, 0, 0));
            schedule.AddFixEvent(new DateTime(Year, Month, Day, 12, 0, 0), "Lunch", 60);
            schedule.AddFixEvent(new DateTime(Year, Month, Day, 17, 0, 0), "Networking Event", 60);

            foreach (var talk in talks)
            {
                try
                {
                    schedule.AddTalkEvent(talk.Title, talk.Duration);
                }
                catch (Exception ex)
                {
                    if (ex is ScheduleFullException || ex is EventOutOfScheduleRangeException)
                    {                       
                        schedules.Add(schedule);
                        schedule = new Schedule(new DateTime(Year, Month, Day, 9, 0, 0), new DateTime(Year, Month, Day, 18, 0, 0));
                        schedule.AddFixEvent(new DateTime(Year, Month, Day, 12, 0, 0), "Lunch", 60);
                        schedule.AddFixEvent(new DateTime(Year, Month, Day, 17, 0, 0), "Networking Event", 60);
                        schedule.AddTalkEvent(talk.Title, talk.Duration);
                        continue;
                    }

                    throw;
                }
            }

            schedules.Add(schedule);
            return schedules;
        }

       
    }

}
