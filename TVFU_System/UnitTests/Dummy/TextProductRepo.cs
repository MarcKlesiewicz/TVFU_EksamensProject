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
            string entry = $"{productArgs.Id};{productArgs.ToString()}";
            TextFileWriter writer = new TextFileWriter();
            writer.Save(entry);
        }

        public Product Get(string guid)
        {
            Product product = new Product();
            TextFileWriter writer = new TextFileWriter();
            string[] productTextInitiator = writer.Get(guid).Split(';');

            product.Id = productTextInitiator[0];
            product.Description = productTextInitiator[1];
            product.UnitPrice = float.Parse(productTextInitiator[2]);
            product.GuidingPrice = float.Parse(productTextInitiator[3]);
            product.TotalStock = int.Parse(productTextInitiator[4]);
            product.Blocked = bool.Parse(productTextInitiator[5]);
            product.UnitPerPackage = int.Parse(productTextInitiator[6]);
            product.QuantityDiscount = float.Parse(productTextInitiator[7]);
            product.ConfirmedDeliveryDate = DateTime.Parse(productTextInitiator[8]);
            product.ProductNumber = int.Parse(productTextInitiator[9]);
            product.CountryOfOrigin = productTextInitiator[10];
            product.PurchasingManager = productTextInitiator[11];

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            List<String> productsInStringFormat = new List<string>();
            List<Product> result = new List<Product>();
            for (int i = 0; i < productsInStringFormat.Count; i++)
            {
                string[] productTextInitiator = productsInStringFormat[i].Split(';');

                Product product = new Product();

                product.Id = productTextInitiator[0];
                product.Description = productTextInitiator[1];
                product.UnitPrice = float.Parse(productTextInitiator[2]);
                product.GuidingPrice = float.Parse(productTextInitiator[3]);
                product.TotalStock = int.Parse(productTextInitiator[4]);
                product.Blocked = bool.Parse(productTextInitiator[5]);
                product.UnitPerPackage = int.Parse(productTextInitiator[6]);
                product.QuantityDiscount = float.Parse(productTextInitiator[7]);
                product.ConfirmedDeliveryDate = DateTime.Parse(productTextInitiator[8]);
                product.ProductNumber = int.Parse(productTextInitiator[9]);
                product.CountryOfOrigin = productTextInitiator[10];
                product.PurchasingManager = productTextInitiator[11];

                result.Add(product);
            }
            return result;
        }

        public IEnumerable<Product> GetBy(EventArgs args)
        {
            return null;
        }

        public void Remove(string guid)
        {
            TextFileWriter writer = new TextFileWriter();
            writer.Remove(guid);
        }

        public void Update(EventArgs args)
        {
            ProductEventArgs productArgs = (args as ProductEventArgs);
            string entry = $"{productArgs.Id};{productArgs.ToString()}";
            TextFileWriter writer = new TextFileWriter();
            writer.Update(entry, productArgs.Id);
        }
    }
}