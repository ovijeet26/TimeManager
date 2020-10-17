namespace ConferenceTrackManagement.Utility
{
    public class DataReader
    {
        /// <summary>
        /// Read raw input from file.
        /// </summary>
        /// <returns></returns>
        public string[] ReadInputFromFile()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }
    }
}
