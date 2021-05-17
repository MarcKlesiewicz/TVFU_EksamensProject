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
        private List<Product> _products = new List<Product>();

        private IGetAll<Product> _getAllService;

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
