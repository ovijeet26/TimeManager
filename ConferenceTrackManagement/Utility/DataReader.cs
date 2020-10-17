namespace ConferenceTrackManagement.Utility
{
    /// <summary>
    /// Class to read input data
    /// Todo: Implement different ways to read data.
    /// </summary>
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
