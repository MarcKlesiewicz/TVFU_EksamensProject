using System;
using System.IO;

namespace UnitTests.Dummy
{
    public class TextFileWriter
    {
        public void Save(string input)
        {
            using (StreamWriter writer = new StreamWriter(@"Dummy\TextFile.ini"))
            {
                writer.WriteLine(input);
            }
        }
        public void Update(string input, string Id)
        {
            using (StreamReader sr = new StreamReader(@"Dummy\TextFile.ini"))
            {
                while (sr.EndOfStream == false)
                {
                    string[] line = sr.ReadLine().Split(',');
                    if (line[0] == Id.ToString())
                    {
                        goto there;
                    }
                }
                return;
            }
            there:

            using (StreamWriter writer = new StreamWriter(@"Dummy\TextFile.ini"))
            {
                writer.WriteLine(Id + "," + input);
            }
        }
    }
}
