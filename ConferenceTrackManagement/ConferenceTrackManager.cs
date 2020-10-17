using ConferenceTrackManagement.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace ConferenceTrackManagement
{
    class ConferenceTrackManager
    {
        static void Main(string[] args)
        {
            EventScheduler scheduler = new EventScheduler();
            scheduler.PrepareSchedule();
            
            scheduler.PrintSchedule();

            string[] lines = System.IO.File.ReadAllLines(@"input.txt");
            Print(lines);
            Console.WriteLine("................");
            Console.WriteLine("................");
            TalkParser parser = new TalkParser();
            var events = parser.ExtractEvents(lines);
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
