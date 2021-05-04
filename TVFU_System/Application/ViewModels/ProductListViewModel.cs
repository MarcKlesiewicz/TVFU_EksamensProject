using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Linq;
using System.Data;
using System.Collections;

namespace Application.ViewModels
{
    public enum SearchCategory { Beskrivelse, Nummer, Enhedspris, Vejledende_pris, Total_lager, Spærret, Antal_pr_kolli, Mængderabat, Indkøbskode, Bekræftet_modtagelsesdato, Oprindelsesland }

    public static class LinqExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _linqResult)
        {
            return new ObservableCollection<T>(_linqResult);
        }
    }

    public class ProductListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SearchCategory _searchCategory;

        public SearchCategory SearchCategory { get { return _searchCategory; } set { _searchCategory = value; } }

        private string _searchWord;

        public string SearchWord { get { return _searchWord; } set { _searchWord = value; OnPropertyChanged("SearchWord"); } }

        public ObservableCollection<ProductViewModel> ViewModels { get; set; }

        private List<Column> _columns = new List<Column>() 
        {
            new Column("ProductNumber"),
            new Column("Description"),
            new Column("UnitPrice"),
            new Column("GuidingPrice"),
            new Column("TotalStock"),
            new Column("Blocked"),
            new Column("UnitPerPackage"),
            new Column("QuantityDiscount"),
            new Column("PurchasingManager"),
            new Column("ConfirmedDeliveryDate"),
            new Column("CountryOfOrigin")
        };

        public ProductListViewModel()
        {
            ViewModels = new ObservableCollection<ProductViewModel>();
        }

        public IEnumerable<ProductViewModel> SortAfter(string category)
        {
            var list = _columns.FindAll(s => s.Name.ToLower() != category.ToLower());

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Order = ColumnOrder.Null;
            }

            if (!_columns.Exists(s => s.Name.ToLower() == category.ToLower()))
            {
                throw new ArgumentNullException("Could not find the given category");
            }

            var column = _columns.First(s => s.Name.ToLower() == category.ToLower());

            column.NextOrder();

            return new CategorySorter().Sort(ViewModels.ToList(), category.ToLower(), column.Order);
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