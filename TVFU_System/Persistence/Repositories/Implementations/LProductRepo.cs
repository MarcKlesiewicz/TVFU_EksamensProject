using DomainLayer.EventArgs;
using DomainLayer.Models;
using Persistence.Data;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementations
{
    public class LProductRepo : IProductRepo
    {
        private static List<Product> _products = new List<Product>();

        private IGetAll<Product> _getAllService;

        private Func<EventArgs, IEnumerable> FilterMethod;

        public LProductRepo(IGetAll<Product> service, Func<EventArgs, IEnumerable> filterMethod)
        {
            _getAllService = service;
            FilterMethod = filterMethod;
        }

        public void Add(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public IEnumerable FilterAndSearchProductList(EventArgs args)
        {
            var filter = (args as FilterEventArgs); //This should be implemented as something else. Preferably a list of strings so we don't couple ourselves to a constant set of filters.
            GetAll();

            return FilterMethod(args);
        }

        public static IEnumerable firstFilterMethod(EventArgs args)
        {
            var filter = (args as FilterEventArgs);

            var productListService = new ProductListService();

            var result = _products;

            if (!String.IsNullOrWhiteSpace(filter.FilterCategory))
            {
                var temp = result;
                result = productListService.FilterCategory(temp, filter.FilterCategory);
            }
            if (!String.IsNullOrWhiteSpace(filter.FilterTreeSort) || !String.IsNullOrWhiteSpace(filter.FilterColour))
            {
                var temp = result;
                result = productListService.FilterOther(temp, new List<string>() { filter.FilterTreeSort, filter.FilterColour });
            }
            if (!String.IsNullOrWhiteSpace(filter.SearchWord))
            {
                var temp = result;
                result = productListService.Search(temp, filter.SearchCategory, filter.SearchWord);
            }

            return result;
        }

        public static IEnumerable secondFilterMethod(EventArgs args)
        {
            var filter = (args as FilterEventArgs);

            switch (filter.SearchCategory.ToLower())
            {
                case "description":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.Description.Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "unitprice":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.UnitPrice.ToString().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "guidingprice":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.GuidingPrice.ToString().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "totalstock":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.TotalStock.ToString().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "blocked":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.Blocked.ToString().ToLower().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "unitperpackage":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.UnitPerPackage.ToString().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "quantitydiscount":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.QuantityDiscount.ToString().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "confirmeddeliverydate":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.ConfirmedDeliveryDate.ToString("dd/MM/yyyy").Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "productnumber":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.ProductNumber.ToString().Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "countryoforigin":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.CountryOfOrigin.Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                case "purchasingmanager":
                    return _products.Where(s => s.ProductCategory.Contains(filter.FilterCategory) && s.PurchasingManager.Contains(filter.SearchWord) && s.Description.Contains(filter.FilterTreeSort) && s.Description.Contains(filter.FilterColour));
                default:
                    throw new Exception("No such category was found, or it was an unsearchable category");
            }
        }

        public Product Get(string guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            _products.Clear();
            _products = _getAllService.GetAll().ToList();
            return _products;
        }

        public IEnumerable<Product> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetByProductCategories()
        {
            GetAll();
            return new ProductListService().SortByProductCategoryThenByProductNumber(_products);
        }

        public IEnumerable GetByProductNumber(int min, int max)
        {
            throw new NotImplementedException();
        }

        public void Remove(string guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable SearchProductList(string searchCategory, string searchWord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable SortAfter(string sortCategory, string order)
        {
            throw new NotImplementedException();
        }

        public void Update(EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
