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

        // private Func<EventArgs, IEnumerable> FilterMethod;

        public LProductRepo(IGetAll<Product> service)
        {
            _getAllService = service;
        }

        public void Add(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public IEnumerable FilterAndSearchProductList(EventArgs args)
        {
            var filter = (args as FilterEventArgs); //This should be implemented as something else. Preferably a list of strings so we don't couple ourselves to a constant set of filters.
            GetAll();

            return firstFilterMethod(args);
        }

        public static IEnumerable firstFilterMethod(EventArgs args)
        {
            var filter = (args as FilterEventArgs);

            if (filter.FilterCategory == null || filter.FilterMaterial == null || filter.FilterColour == null || filter.FilterOther == null)
            {
                return _products;
            }

            var productListService = new ProductListService();

            var result = _products;

            if (anyActiveFilters(filter))
            {
                var filters = new List<String>();
                if (filter.FilterCategory != "Ingen")
                {
                    filters.Add(filter.FilterCategory);
                }
                if (filter.FilterColour != "Ingen")
                {
                    filters.Add(filter.FilterColour);
                }
                if (filter.FilterMaterial != "Ingen")
                {
                    filters.Add(filter.FilterMaterial);
                }
                if (filter.FilterOther != "Ingen")
                {
                    filters.Add(filter.FilterOther);
                }
                var temp = result;
                result = productListService.FilterCategory(temp, filters);
            }
            if (!String.IsNullOrWhiteSpace(filter.SearchWord))
            {
                var temp = result;
                result = productListService.Search(temp, filter.SearchCategory, filter.SearchWord);
            }

            return result;
        }

        private static bool anyActiveFilters(FilterEventArgs args)
        {
            if (args.FilterCategory == "Ingen" || args.FilterColour == "Ingen" || args.FilterMaterial == "Ingen" || args.FilterOther == "Ingen")
            {
                return true;
            }
            return false;
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
