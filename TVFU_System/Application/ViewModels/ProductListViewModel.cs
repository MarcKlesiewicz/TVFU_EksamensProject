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
        private SearchCategory _searchCategory;
        public SearchCategory SearchCategory { get { return _searchCategory; } set { _searchCategory = value; OnPropertyChanged("SearchCategory"); } }

        private string _searchWord = String.Empty;
        public string SearchWord { get { return _searchWord; } set { _searchWord = value; OnPropertyChanged("SearchWord"); } }

        public string LastSortedCategory { get; set; }

        public ObservableCollection<ProductViewModel> ViewModels { get; set; }

        private string _currentCategory;
        public string CurrentCategory { get { return _currentCategory; } set { _currentCategory = value; OnPropertyChanged("CurrentCategory"); } }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories.Clear();
                _categories.Add("Ingen");
                foreach (var item in value)
                {
                    _categories.Add(item);
                }
            }
        }

        private string _currentColour;
        public string CurrentColour { get { return _currentColour; } set { _currentColour = value; OnPropertyChanged("CurrentColour"); } }

        private ObservableCollection<string> _colours;
        public ObservableCollection<string> Colours
        {
            get { return _colours; }
            set
            {
                _colours.Clear();
                _colours.Add("Ingen");
                foreach (var item in value)
                {
                    _colours.Add(item);
                }
            }
        }

        private string _currentMaterial;
        public string CurrentMaterial { get { return _currentMaterial; } set { _currentMaterial = value; OnPropertyChanged("CurrentMaterial"); } }

        private ObservableCollection<string> _materials;
        public ObservableCollection<string> Materials
        {
            get { return _materials; }
            set
            {
                _materials.Clear();
                _materials.Add("Ingen");
                foreach (var item in value)
                {
                    _materials.Add(item);
                }
            }
        }

        private string _currentOtherFilter;
        public string CurrentOtherFilter { get { return _currentOtherFilter; } set { _currentOtherFilter = value; OnPropertyChanged("CurrentMaterial"); } }

        private ObservableCollection<string> _otherFilters;
        public ObservableCollection<string> OtherFilters
        {
            get { return _otherFilters; }
            set
            {
                _otherFilters.Clear();
                _otherFilters.Add("Ingen");
                foreach (var item in value)
                {
                    _otherFilters.Add(item);
                }
            }
        }

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
            _categories = new ObservableCollection<string>();
            _colours = new ObservableCollection<string>();
            _materials = new ObservableCollection<string>();
            _otherFilters = new ObservableCollection<string>();
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
            _searchWord = String.Empty;
            OnPropertyChanged("SearchWord");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}