
using System;
using System.Collections.Generic;
using ConferenceTrackManagement.Models;
using ConferenceTrackManagement.CustomExceptions;

namespace ConferenceTrackManagement.Utility
{

    public class EventScheduler
    {
        //Setting the date for today
        const int Year = 2020;
        const int Month = 10;
        const int Day = 18;
        /// <summary>
        /// Create the schedule for the events, divided into different tracks based on time.
        /// </summary>
        /// <param name="talks"></param>
        /// <returns></returns>
        public List<Schedule> PrepareSchedule(List<Talk> talks)
        {
            List<Schedule> schedules = new List<Schedule>();
            Schedule schedule = InitializeSchedule();

            //Iterating over all the talks thats have been parsed out of the raw input.
            foreach (var talk in talks)
            {
                try
                {
                    //Add the talk event to the schedule.
                    schedule.AddTalkEvent(talk.Title, talk.Duration);
                }
                catch (Exception ex)
                {
                    //If any of our custom exceptions are encountered, we create a new track as the existing one has 'overflowed'.
                    if (ex is ScheduleFullException || ex is EventOutOfScheduleRangeException)
                    {
                        schedules.Add(schedule);
                        schedule = InitializeSchedule();
                        schedule.AddTalkEvent(talk.Title, talk.Duration);
                        continue;
                    }
                    throw;
                }
            }
            schedules.Add(schedule);
            return schedules;
        }
        /// <summary>
        /// Initialize a Schedule object based on given constraints.
        /// </summary>
        /// <returns></returns>
        private Schedule InitializeSchedule()
        {
            //  a new schedule object based on the given scenarios, starting time 9 am and end time 6pm (considering Networking event to be 60 mins long).
            Schedule schedule = new Schedule(new DateTime(Year, Month, Day, 9, 0, 0), new DateTime(Year, Month, Day, 18, 0, 0));
            //Adding fixed events in the schedule. For the given scenario, they are Lunch (60 mins) and Networking Event (60 mins).
            schedule.AddFixedEvent(new DateTime(Year, Month, Day, 12, 0, 0), "Lunch", 60);
            schedule.AddFixedEvent(new DateTime(Year, Month, Day, 17, 0, 0), "Networking Event", 60);
            return schedule;
        }
    }
}
