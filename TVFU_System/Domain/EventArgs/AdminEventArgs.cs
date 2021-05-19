using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EventArgs
{
    public class AdminEventArgs : System.EventArgs
    {
        public IEnumerable<string> Filters { get; set; }
    }
}
