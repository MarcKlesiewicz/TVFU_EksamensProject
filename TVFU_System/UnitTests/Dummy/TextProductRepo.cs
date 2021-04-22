using DomainLayer.Models;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using DomainLayer.EventArgs;

namespace UnitTests.Dummy
{
    public class TextProductRepo : IProductRepo
    {
        public void Add(EventArgs args)
        {
            ProductEventArgs productArgs = (args as ProductEventArgs);
            string entry = productArgs.Id + "," + productArgs.Description + "," + productArgs.UnitPrice + "," + productArgs.GuidingPrice
                 + "," + productArgs.TotalStock + "," + productArgs.Blocked + "," + productArgs.UnitPerPackage + ","
                 + productArgs.QuantityDiscount + "," + productArgs.ConfirmedDeliveryDate.ToString() + ","
                 + productArgs.ProductNumber + "," + productArgs.CountryOfOrigin + "," + productArgs.PurchasingManager;
            TextFileWriter writer = new TextFileWriter();
            writer.Save(entry);
        }

        public IEnumerable<Product> GetAll()
        {
            return null;
        }

        public IEnumerable<Product> GetBy(EventArgs args)
        {
            return null;
        }

        public void Remove(EventArgs args)
        {

        }

        public void Update(EventArgs args)
        {

        }
    }
}
