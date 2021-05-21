using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface ICategoryRepo : IRepo<string>
    {
        void Add(string category);
    }
}
