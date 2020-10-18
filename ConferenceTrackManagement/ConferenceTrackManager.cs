using ConferenceTrackManagement.Models;
using ConferenceTrackManagement.Utility;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    class ConferenceTrackManager
    {
        static void Main(string[] args)
        {
            //Read the input data.
            DataReader reader = new DataReader();
            string[] lines = reader.ReadInputFromFile();

            ConsolePrinter printer = new ConsolePrinter();
            //Optional : Print the raw input data.
            //printer.PrintArray(lines);

            //Parse the raw input data and extract talk details.
            TalkParser parser = new TalkParser();
            List<Talk> talks = parser.ExtractEvents(lines);

            //Schedule the talks according to the given constraints.
            EventScheduler scheduler = new EventScheduler();
            List<Schedule> schedule = scheduler.PrepareSchedule(talks);

            //Print the schedule into the console.
            printer.PrintSchedule(schedule);
          
            Console.ReadKey();
        }

    }
}
