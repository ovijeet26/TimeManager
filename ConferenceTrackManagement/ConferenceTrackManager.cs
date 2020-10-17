using ConferenceTrackManagement.Utility;
using System;
using System.Collections.Generic;
using ConferenceTrackManagement.Models;

namespace ConferenceTrackManagement
{
    class ConferenceTrackManager
    {
        static void Main(string[] args)
        {
            //Read the input data.
            DataReader reader = new DataReader();
            string[] lines = reader.ReadInputFromFile();
            
            //Optional : Print the raw input data.
            ConsolePrinter printer = new ConsolePrinter();
            printer.PrintArray(lines);

            //Parse the raw input data and extract talk details.
            TalkParser parser = new TalkParser();
            List<Talk> events = parser.ExtractEvents(lines);

            //Schedule the talks according to the given constraints.
            TalkScheduler scheduler = new TalkScheduler();
            Dictionary<int, Track> tracks = scheduler.ScheduleTalks(events);

            //Print the final output schedule in the console.
            printer.PrintTracks(tracks);
            Console.ReadKey();
        }
        
    }
}
