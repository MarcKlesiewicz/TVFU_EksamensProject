using System;
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
        public void Update(string input, Guid Id)
        {
            using (StreamReader sr = new StreamReader(@"Data\TextFile.ini"))
            {
                while (sr.EndOfStream == false)
                {
                    string[] line = sr.ReadLine().Split(':');
                    if (line[0] == Id.ToString())
                    {
                        goto there;
                    }
                }
                return;
            }
            there:

            using (StreamWriter writer = new StreamWriter(@"Data\TextFile.ini"))
            {
                writer.WriteLine(Id.ToString() + ":" + input);
            }
        }
    }
}
