using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class AdminViewModel
    {
        public ObservableCollection<string> Filters { get; set; }

        public AdminViewModel()
        {
            Filters = new ObservableCollection<string>();
        }
    }
}
