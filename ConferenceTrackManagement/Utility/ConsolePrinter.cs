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
        /// Print the Schedule into the console in the desired format.
        /// </summary>
        /// <param name="schedules"></param>
        public void PrintSchedule(List<Schedule> schedules)
        {
            int trackCounter = 1;

            foreach (var schedule in schedules)
            {
                Console.WriteLine($"Track {trackCounter}");
                Console.WriteLine();
                Console.WriteLine(schedule.ToString());
                trackCounter++;
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
