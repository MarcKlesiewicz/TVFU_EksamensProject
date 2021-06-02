using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EventArgs
{
    public class AdminEventArgs : System.EventArgs
    {
        public IEnumerable<string> Colours { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Materials { get; set; }

        public IEnumerable<string> OtherFilters { get; set; }
    }
}
