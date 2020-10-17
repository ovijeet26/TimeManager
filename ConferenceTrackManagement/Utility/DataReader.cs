namespace ConferenceTrackManagement.Utility
{
    public class DataReader
    {
        public string[] ReadInputFromFile()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }
    }
}
