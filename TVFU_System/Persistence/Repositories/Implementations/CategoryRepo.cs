using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.IO;

namespace Persistence.Repositories.Implementations
{
    public class CategoryRepo : ICategoryRepo
    {
        public void Add(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Add(string category)
        {
            List<string> categories = (List<string>)GetAll();
            categories.Add(category);
            using (StreamWriter sw = new StreamWriter(new FileStream(@"Repositories\Implementations\Categories.ini", FileMode.Open, FileAccess.Write), Encoding.GetEncoding("iso-8859-1")))
            {
                foreach (var item in categories)
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
            List<string> categories = new List<string>();
            using (StreamReader sr = new StreamReader(@"Repositories\Implementations\Categories.ini", Encoding.GetEncoding("iso-8859-1")))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var firstLetter = line.Substring(0, 1);
                    var rest = line.Substring(1);
                    categories.Add(firstLetter.ToUpper() + rest);
                }
            }
            categories.Sort();
            return categories;
        }

        public IEnumerable<string> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(string category)
        {
            List<string> categories = (List<string>)GetAll();
            categories.Remove(category);
            using (StreamWriter sw = new StreamWriter(new FileStream(@"Repositories\Implementations\Categories.ini", FileMode.Open, FileAccess.Write), Encoding.GetEncoding("iso-8859-1")))
            {
                foreach (var item in categories)
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
