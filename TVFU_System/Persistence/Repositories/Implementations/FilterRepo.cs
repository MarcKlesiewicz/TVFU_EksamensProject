using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.IO;

namespace Persistence.Repositories.Implementations
{
    public class FilterRepo : IFilterRepo
    {
        public void Add(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Add(string filter)
        {
            List<string> filters = (List<string>)GetAll();
            filters.Add(filter);
            using (StreamWriter sw = new StreamWriter(@"Repositories\Implementations\Filters.ini"))
            {
                foreach (var item in filters)
                {
                    sw.WriteLine(item.ToLower());
                }
            }
        }

        public string Get(string guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAll()
        {
            List<string> filters = new List<string>();
            using (StreamReader sr = new StreamReader(@"Repositories\Implementations\Filters.ini"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var firstLetter = line.Substring(0, 1);
                    var rest = line.Substring(1);
                    filters.Add(firstLetter.ToUpper() + rest);
                }
            }
            filters.Sort();
            return filters;
        }

        public IEnumerable<string> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(string filter)
        {
            List<string> filters = (List<string>)GetAll();
            filters.Remove(filter);
            using (StreamWriter sw = new StreamWriter(@"Repositories\Implementations\Filters.ini"))
            {
                foreach (var item in filters)
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
