using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainLayer.EventArgs
{
    public class FilterEventArgs : System.EventArgs
    {
        public string SearchWord { get; set; }

        public string SearchCategory { get; set; }

        public string FilterMaterial { get; set; }

        public string FilterCategory { get; set; }

        public string FilterColour { get; set; }

        public string FilterOther { get; set; }

    }
}
