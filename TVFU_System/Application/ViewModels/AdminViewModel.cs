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
        public ObservableCollection<string> Colours { get; set; }

        public ObservableCollection<string> Categories { get; set; }

        public ObservableCollection<string> Materials { get; set; }

        public ObservableCollection<string> OtherFilters { get; set; }

        public AdminViewModel()
        {
            Colours = new ObservableCollection<string>();
            Categories = new ObservableCollection<string>();
            Materials = new ObservableCollection<string>();
            OtherFilters = new ObservableCollection<string>();
        }
    }
}
