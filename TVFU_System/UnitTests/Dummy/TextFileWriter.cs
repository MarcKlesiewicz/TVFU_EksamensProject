using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTests.Dummy
{
    public class TextFileWriter
    {
        public void Save(string input)
        {
            string result = "";
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(';');
                    result += String.Join(";", line) + "\n";
                }
            }

            using (StreamWriter writer = new StreamWriter(@"Dummy\TextFile.ini"))
            {
                writer.WriteLine(result + input);
            }
        }
        public void Update(string input, string Id)
        {
            string result = "";
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(';');
                    if (line[0] == Id)
                    {
                        result += input + "\n";
                    } else
                    {
                        result += String.Join(";", line) + "\n";
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(@"Dummy\TextFile.ini"))
            {
                writer.Write(result);
            }
        }

        public string Get(string id)
        {
            string result = "";
            bool objectFound = false;
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(';');
                    if (line[0] == id)
                    {
                        result = String.Join(";", line);
                        objectFound = true;
                        break;
                    }
                }
            }
            if (!objectFound)
            {
                throw new Exception();
            }
            return result;
        }

        public List<String> GetAll()
        {
            List<String> listResult = new List<string>();
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(';');
                    listResult.Add(String.Join(";", line));
                }
            }
            return listResult;
        }

        public void Remove(string id)
        {
            string result = "";
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(';');
                    if (line[0] != id)
                    {
                        result += String.Join(";", line) + "\n";
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(@"Dummy\TextFile.ini"))
            {
                writer.Write(result);
            }
        }
    }
}
