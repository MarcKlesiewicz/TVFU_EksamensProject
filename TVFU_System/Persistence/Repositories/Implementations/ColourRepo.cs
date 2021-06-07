using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.IO;

namespace Persistence.Repositories.Implementations
{
    public class ColourRepo : IColourRepo
    {
        public void Add(string colour)
        {
            List<string> colours = (List<string>)GetAll();
            colours.Add(colour);
            using (StreamWriter sw = new StreamWriter(new FileStream(@"Repositories\Implementations\Colours.ini", FileMode.Open, FileAccess.Write), Encoding.GetEncoding("iso-8859-1")))
            {
                foreach (var item in colours)
                {
                    sw.WriteLine(item.ToLower());
                }
            }

        }

        public void Add(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public string Get(string guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAll()
        {
            List<string> colours = new List<string>();
            using (StreamReader sr = new StreamReader(@"Repositories\Implementations\Colours.ini", Encoding.GetEncoding("iso-8859-1")))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var firstLetter = line.Substring(0, 1);
                    var rest = line.Substring(1);
                    colours.Add(firstLetter.ToUpper() + rest);
                }
            }
            colours.Sort();
            return colours;
        }

        public IEnumerable<string> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(string colour)
        {
            List<string> colours = (List<string>)GetAll();
            colours.Remove(colour);
            using (StreamWriter sw = new StreamWriter(new FileStream(@"Repositories\Implementations\Colours.ini", FileMode.Open, FileAccess.Write), Encoding.GetEncoding("iso-8859-1")))
            {
                foreach (var item in colours)
                {
                    sw.WriteLine(item);
                }
            }
        }

        public void Update(EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
