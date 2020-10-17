using ConferenceTrackManagement.Utility;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    class ConferenceTrackManager
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string[] lines = System.IO.File.ReadAllLines(@"input.txt");
            Print(lines);
            Console.WriteLine("................");
            Console.WriteLine("................");
            TalkParser parser = new TalkParser();
            var events = parser.ExtractEvents(lines);
            TalkScheduler scheduler = new TalkScheduler();
            scheduler.ScheduleTalks(events);
            Console.ReadKey();
        }
        static void Print(string[] arr)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
