using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Interfaces;
using System.IO;

namespace Persistence.Repositories.Implementations
{
    public class MaterialRepo : IMaterialRepo
    {
        public void Add(string material)
        {
            List<string> materials = (List<string>)GetAll();
            materials.Add(material);
            using (StreamWriter sw = new StreamWriter(@"Repositories\Implementations\Materials.ini"))
            {
                foreach (var item in materials)
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
            List<string> materials = new List<string>();
            using (StreamReader sr = new StreamReader(@"Repositories\Implementations\Materials.ini"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var firstLetter = line.Substring(0, 1);
                    var rest = line.Substring(1);
                    materials.Add(firstLetter.ToUpper() + rest);
                }
            }
            materials.Sort();
            return materials;
        }

        public IEnumerable<string> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(string material)
        {
            List<string> materials = (List<string>)GetAll();
            materials.Remove(material);
            using (StreamWriter sw = new StreamWriter(@"Repositories\Implementations\Colours.ini"))
            {
                foreach (var item in materials)
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
