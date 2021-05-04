using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;


namespace Application.ViewModels
{
    public enum SearchCategory { Beskrivelse, Nummer, Enhedspris, Vejledende_pris, Total_lager, Spærret, Antal_pr_kolli, Mængderabat, Indkøbskode, Bekræftet_modtagelsesdato, Oprindelsesland }
    public class ProductListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SearchCategory _searchCategory;

        public SearchCategory SearchCategory { get { return _searchCategory; } set { _searchCategory = value; } }

        private string _searchWord;

        public string SearchWord { get { return _searchWord; } set { _searchWord = value; OnPropertyChanged("SearchWord"); } }


        public ObservableCollection<ProductViewModel> ViewModels { get; set; }

        public ProductListViewModel()
        {
            ViewModels = new ObservableCollection<ProductViewModel>();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}