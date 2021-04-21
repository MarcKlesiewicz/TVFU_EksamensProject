using DomainLayer.Models;
using Persistence.Repositories.Interfaces;
using DomainLayer.EventArgs;
using Persistence.Data;
using System;
using System.Collections.Generic;

namespace Persistence.Repositories.Implementations
{
    public class ProductRepo : IProductRepo
    {
        public IData DataWriter { get; private set; }
        public ProductRepo(IData datawriter)
        {
            DataWriter = datawriter;
        }

        public void Add(EventArgs args)
        {
            DataWriter.Save((args as ProductEventArgs).Name);
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Update(EventArgs args)
        {
            DataWriter.Save((args as ProductEventArgs).Name);
        }
    }
}
