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

    public class ProductListViewModel
    {
        public SearchCategory SearchCategory { get; set; }

        public string SearchWord { get; set; } = "";

        public FilterCategory FilterCategory { get; set; }

        public string FilterTreeSort { get; set; } = "";

        public string FilterColor { get; set; } = "";

        public string LastSortedCategory { get; set; }

        public ObservableCollection<ProductViewModel> ViewModels { get; set; }

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
    }
}