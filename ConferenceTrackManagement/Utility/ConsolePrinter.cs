using ConferenceTrackManagement.Models;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement.Utility
{
    public class ConsolePrinter
    {
        public void PrintArray(string[] arr)
        {
            LineBreak();
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            LineBreak();

        }
        public void PrintTracks(Dictionary<int,Track> tracks)
        {
            foreach(var track in tracks)
            {
                Console.WriteLine("Track "+track.Key+":");
                Console.WriteLine();
                DateTime timer = DateTime.Today.AddHours(9);
                foreach (var talk in track.Value.PreLunchTalks)
                {
                    Console.WriteLine(timer.ToString("HH:mmtt") +" "+ talk.Title + " " + talk.Duration + "min");
                    timer=timer.AddMinutes(talk.Duration);
                }

                Console.WriteLine(timer.ToString("HH:mmtt") + " Lunch");
                timer = timer.AddMinutes(60);

                foreach (var talk in track.Value.PostLunchTalks)
                {
                    Console.WriteLine(timer.ToString("HH:mmtt") + " " + talk.Title + " " + talk.Duration + "min");
                    timer = timer.AddMinutes(talk.Duration);
                }
                Console.WriteLine(timer.ToString("HH:mmtt") + " Networking Event");
                Console.WriteLine();
            }
        }
        private void LineBreak()
        {
            Console.WriteLine("................");
        }
    }
}
