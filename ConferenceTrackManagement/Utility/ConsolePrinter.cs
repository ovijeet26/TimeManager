using ConferenceTrackManagement.Models;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement.Utility
{
    public class ConsolePrinter
    {
        /// <summary>
        /// Print any string array.
        /// </summary>
        /// <param name="arr"></param>
        public void PrintArray(string[] arr)
        {
            LineBreak();
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            LineBreak();
        }
        /// <summary>
        /// Print the Tracks HashMap into the console in the desired format.
        /// </summary>
        /// <param name="tracks"></param>
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
        /// <summary>
        /// Print a line break.
        /// </summary>
        private void LineBreak()
        {
            Console.WriteLine("................");
        }
    }
}
