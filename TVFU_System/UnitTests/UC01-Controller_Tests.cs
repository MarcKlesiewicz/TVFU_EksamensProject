using System;
using DomainLayer.EventArgs;
using Application.Controllers;
using Application.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UC01_Controller_Tests
    {
        ProductListController controller;
        ProductRepo repo;

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void CreateProductCancel()
        {
            repo = new ProductRepo();
            controller = new ProductListController(repo);
        }

        [TestMethod]
        public void CreateProductConfirmed()
        {
            repo = new ProductRepo();
            controller = new ProductListController(repo);
        }

        private ProductEventArgs NullProductEventArgs()
        {

        }
    }
}
