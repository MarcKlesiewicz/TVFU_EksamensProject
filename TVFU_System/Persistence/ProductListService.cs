using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ProductListService
    {
        public List<Product> SortByProductCategoryThenByProductNumber(List<Product> list)
        {
            return list.OrderBy(s => s.ProductCategory).ThenBy(s => s.ProductNumber).ToList();
        }

        /// <summary>
        /// Right now, the searchInput searches in the entirity of the Products. Meaning that it searches for anything 
        /// within the ToString() method of the Product Class.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="searchCategory">Unused</param>
        /// <param name="searchInput"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<Product> FilterCategory(List<Product> list, string filterCategory)
        {
            return list.FindAll(s => s.ProductCategory.Contains(filterCategory));
        }

        public List<Product> FilterOther(List<Product> list, List<string> filters)
        {
            List<Product> result = list;
            foreach (var item in filters)
            {
                var temp = result;
                result = temp.FindAll(s => s.Description.Contains(item));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="searchCategory"></param>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        public List<Product> Search(List<Product> list, string searchCategory, string searchInput)
        {
            switch (searchCategory.ToLower())
            {
                case "description":
                    return list.FindAll(s => s.Description.Contains(searchInput));
                case "unitprice":
                    return list.FindAll(s => s.UnitPrice.ToString().Contains(searchInput));
                case "guidingprice":
                    return list.FindAll(s => s.GuidingPrice.ToString().Contains(searchInput));
                case "totalstock":
                    return list.FindAll(s => s.TotalStock.ToString().Contains(searchInput));
                case "blocked":
                    return list.FindAll(s => s.Blocked.ToString().ToLower().Contains(searchInput));
                case "unitperpackage":
                    return list.FindAll(s => s.UnitPerPackage.ToString().Contains(searchInput));
                case "quantitydiscount":
                    return list.FindAll(s => s.QuantityDiscount.ToString().Contains(searchInput));
                case "confirmeddeliverydate":
                    return list.FindAll(s => s.ConfirmedDeliveryDate.ToString("dd/MM/yyyy").Contains(searchInput));
                case "productnumber":
                    return list.FindAll(s => s.ProductNumber.ToString().Contains(searchInput));
                case "countryoforigin":
                    return list.FindAll(s => s.CountryOfOrigin.Contains(searchInput));
                case "purchasingmanager":
                    return list.FindAll(s => s.PurchasingManager.Contains(searchInput));
                default:
                    throw new Exception("No such category was found, or it was an unsearchable category");
            }
        }

        private void method()
        {
            using (StreamReader reader = new StreamReader(@"Data\config.ini"))
            {
                reader.ReadLine();
                string[] extractedFilters = reader.ReadLine().Split(':')[1].Split(',');
                if (extractedFilters.Length == 0 || String.IsNullOrWhiteSpace(extractedFilters[0]))
                {
                    throw new Exception("No filters found in config file.");
                }
                foreach (var item in extractedFilters)
                {
                    if ("".ToLower() == item)
                    {

                    }
                }
            }
        }
    }
}
