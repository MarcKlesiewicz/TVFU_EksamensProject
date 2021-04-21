using System.IO;

namespace Persistence.Data
{
    public class TextFileWriter : IData
    {
        public void Save(string input)
        {
            using (StreamWriter writer = new StreamWriter(@"Data\TextFile.ini"))
            {
                writer.WriteLine(input);
            }
        }
        public void Update(string input)
        {
            using (StreamWriter writer = new StreamWriter(@"Data\TextFile.ini"))
            {
                writer.WriteLine(input);
            }
        }
    }
}
