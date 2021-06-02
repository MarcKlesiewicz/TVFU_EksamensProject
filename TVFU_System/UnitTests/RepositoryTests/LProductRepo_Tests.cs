using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.Data.SqlClient;
using DomainLayer.EventArgs;
using DomainLayer.Models;
using System.Collections;

namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class LPRoductRepo_Tests
    {
        LProductRepo repo;
        ProductRepo GetAllImplementation;
        FilterEventArgs args;

        [TestInitialize]
        public void Init()
        {
            GetAllImplementation = new ProductRepo();
            args = new FilterEventArgs() { FilterCategory = "Køkken", FilterColour = "Rød", FilterTreeSort = "Birk", SearchCategory = "Description", SearchWord = "122" };
        }

        //[TestMethod]
        //public void LinqTest1()
        //{
        //    repo = new LProductRepo(GetAllImplementation, LProductRepo.firstFilterMethod);

        //    for (int i = 0; i < 100; i++)
        //    {
        //        repo.FilterAndSearchProductList(args);
        //    }
        //}

        //[TestMethod]
        //public void LinqTest2()
        //{
        //    repo = new LProductRepo(GetAllImplementation, LProductRepo.secondFilterMethod);

        //    for (int i = 0; i < 100; i++)
        //    {
        //        repo.FilterAndSearchProductList(args);
        //    }
        //}

        //[TestMethod]
        //public void LinqRepo()
        //{
        //    repo = new LProductRepo(GetAllImplementation, LProductRepo.firstFilterMethod);

        //    for (int i = 0; i < 200; i++)
        //    {
        //        repo.FilterAndSearchProductList(args);
        //    }
        //}

        //[TestMethod]
        //public void DatabaseRepo()
        //{
        //    var repo = new ProductRepo();

        //    for (int i = 0; i < 200; i++)
        //    {
        //        repo.FilterAndSearchProductList(args);
        //    }
        //}
    }
}
