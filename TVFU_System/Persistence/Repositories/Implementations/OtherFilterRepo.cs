using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.IO;

namespace Persistence.Repositories.Implementations
{
    public class OtherFilterRepo : IOtherFilterRepo
    {
        public void Add(string otherFilter)
        {
            List<string> otherFilters = (List<string>)GetAll();
            otherFilters.Add(otherFilter);
            using (StreamWriter sw = new StreamWriter(@"Repositories\Implementations\OtherFilters.ini"))
            {
                foreach (var item in otherFilters)
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
            List<string> otherFilters = new List<string>();
            using (StreamReader sr = new StreamReader(@"Repositories\Implementations\OtherFilters.ini", Encoding.GetEncoding("iso-8859-1")))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var firstLetter = line.Substring(0, 1);
                    var rest = line.Substring(1);
                    otherFilters.Add(firstLetter.ToUpper() + rest);
                }
            }
            otherFilters.Sort();
            return otherFilters;
        }

        public IEnumerable<string> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(string otherFílter)
        {
            List<string> otherFilters = (List<string>)GetAll();
            otherFilters.Remove(otherFílter);
            using (StreamWriter sw = new StreamWriter(@"Repositories\Implementations\OtherFilters.ini"))
            {
                foreach (var item in otherFilters)
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
