using ConferenceTrackManagement.Models;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement.Utility
{
    class TalkParser
    {
        /// <summary>
        /// Extract talks and duration into a List of Talk objects.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<Talk> ExtractEvents(string[] arr)
        {
            List<Talk> events = new List<Talk>(); 
            for (int i = 0; i < arr.Length; i++)
            {
                string input = arr[i].Replace(" lightning ", " 5 ");
                int startIndex = 0;
                int linesExtractedFromCurrentIteration = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    if (char.IsDigit(input[j]))
                    {
                        string talkTitle = input.Substring(startIndex, (j - startIndex)).Trim();
                        int duration = 0;
                        //Three digit minutes starting from 100 mins.
                        if (char.IsDigit(input[j + 1]) && char.IsDigit(input[j + 2]))
                        {
                            string durationKey = input[j] + input[j + 1] + input[j + 2].ToString();
                            duration = input[j] + input[j + 1] + input[j + 2];
                            j += 2;

                        }
                        //Two digit minutes starting from 10 mins.
                        else if (char.IsDigit(input[j + 1]))
                        {
                            string durationKey = input[j] + input[j + 1].ToString();
                            duration = int.Parse(durationKey);
                            j++;
                        }
                        //One digit minute starting from 1 min.
                        else
                        {
                            duration = int.Parse(input[j].ToString());
                        }
                        //Adding to the HashMap/Dictionary.
                        AddToEvents(events, duration, talkTitle);

                        linesExtractedFromCurrentIteration++;
                        //Detecting the next space after one line has been extracted.
                        int spaceIndex = input.IndexOf(" ", j);
                        //If there is no other talkTitle present in the current line, then break.
                        if (spaceIndex == -1)
                            break;
                        //Setting the next start index to the start of the next talk.
                        startIndex = spaceIndex + 1;
                    }
                    //If the current line has ended, then we have some data that needs to be extracted, which we prepend to the next line.
                    if (j == input.Length - 1)
                    {
                        EnsureDataIntegrity(arr, i, startIndex);
                    }
                }

            }
            return events;
        }
        /// <summary>
        /// Ensure there is no data loss by adding lines that have not been completely parsed to the next line.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="iteratingIndex"></param>
        /// <param name="delimiterPos"></param>
        private void EnsureDataIntegrity(string[] arr, int iteratingIndex, int delimiterPos)
        {
            arr[iteratingIndex] = arr[iteratingIndex].Trim();
            string remainingPart = arr[iteratingIndex].Substring(delimiterPos).Trim();
            arr[iteratingIndex + 1] = remainingPart + " " + arr[iteratingIndex + 1];
        }
        /// <summary>
        /// Add talks and duration to the Talk List.
        /// </summary>
        /// <param name="events"></param>
        /// <param name="duration"></param>
        /// <param name="eventValue"></param>
        private void AddToEvents (List<Talk> events, int duration, string eventValue)
        {
            events.Add(new Talk(duration, eventValue));
        }
    }
}
