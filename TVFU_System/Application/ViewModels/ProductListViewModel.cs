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

    public enum FilterCategory { Ingen, KUBIK, ABC, Møbler, Køkken, Have }
    public static class LinqExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _linqResult)
        {
            return new ObservableCollection<T>(_linqResult);
        }
    }

    public class ProductListViewModel : INotifyPropertyChanged
    {
        private SearchCategory _searchCategory;
        public SearchCategory SearchCategory { get { return _searchCategory; } set { _searchCategory = value; OnPropertyChanged("SearchCategory"); } }

        private string _searchWord = String.Empty;
        public string SearchWord { get { return _searchWord; } set { _searchWord = value; OnPropertyChanged("SearchWord"); } }

        private FilterCategory _filterCategory;
        public FilterCategory FilterCategory { get { return _filterCategory; } set { _filterCategory = value; OnPropertyChanged("FilterCategory"); } }

        private string _filterTreeSort = String.Empty;
        public string FilterTreeSort { get { return _filterTreeSort; } set { _filterTreeSort = value; OnPropertyChanged("FilterTreeSort"); } }

        private string _filterColor = String.Empty;
        public string FilterColor { get { return _filterColor; } set { _filterColor = value; OnPropertyChanged("FilterColor"); } }

        public string LastSortedCategory { get; set; }

        public ObservableCollection<ProductViewModel> ViewModels { get; set; }

        public ObservableCollection<string> Categories { get; set; }

        public ObservableCollection<string> Colours { get; set; }

        public ObservableCollection<string> Materials { get; set; }

        public ObservableCollection<string> OtherFilters { get; set; }

        private readonly List<Column> _columns = new List<Column>()
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

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductListViewModel()
        {
            ViewModels = new ObservableCollection<ProductViewModel>();
            Categories = new ObservableCollection<string>();
            Colours = new ObservableCollection<string>();
            Materials = new ObservableCollection<string>();
            OtherFilters = new ObservableCollection<string>();
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

            if (!ColumnsReset())
            {
                LastSortedCategory = category;
            }
            else
            {
                LastSortedCategory = String.Empty;
            }

            return new CategorySorter().Sort(ViewModels.ToList(), category.ToLower(), column.Order);
        }

        public bool ColumnsReset()
        {
            for (int i = 0; i < _columns.Count; i++)
            {
                if (_columns[i].Order != ColumnOrder.Null)
                {
                    return false;
                }
            }
            return true;
        }

        public void LastCategoryPreviousColumnOrder()
        {
            var column = _columns.First(s => s.Name.ToLower() == LastSortedCategory.ToLower());

            for (int i = 0; i < 2; i++)
            {
                column.NextOrder();
            }
        }

        private void ResetColumns()
        {
            LastSortedCategory = String.Empty;

            for (int i = 0; i < _columns.Count; i++)
            {
                _columns[i].Order = ColumnOrder.Null;
            }
        }

        public void Reset()
        {
            ResetColumns();
            _searchCategory = SearchCategory.Beskrivelse;
            OnPropertyChanged("SearchCategory");
            _filterCategory = FilterCategory.Ingen;
            OnPropertyChanged("FilterCategory");
            _searchWord = String.Empty;
            OnPropertyChanged("SearchWord");
            _filterTreeSort = String.Empty;
            OnPropertyChanged("FilterTreeSort");
            _filterColor = String.Empty;
            OnPropertyChanged("FilterColor");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}