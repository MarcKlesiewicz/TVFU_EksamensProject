using System;
using DomainLayer.EventArgs;
using Application.Controllers;
using Application.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.IO;
using UnitTests.Dummy;

namespace UnitTests
{
    [TestClass]
    public class UC01_Controller_Tests
    {
        ProductListController controller;
        TextProductRepo repo;

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void CreateProductCancel()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);
            controller.NewProductRequested += NullProductEventArgs;

            //Act
            controller.CreateProduct(null);

            //Assert
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                Assert.AreEqual(null, reader.ReadLine());
            }
        }

        [TestMethod]
        public void CreateProductConfirmed()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);
            controller.NewProductRequested += NotNullProductEventArgs;

            //Act
            controller.CreateProduct(null);

            //Assert
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                Assert.IsNotNull(reader.ReadLine());
            }
        }

        private ProductEventArgs NullProductEventArgs(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = null;
            return result;
        }

        private ProductEventArgs NotNullProductEventArgs(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = new ProductEventArgs() { Description = "descriptionTest", UnitPrice = (float)1.1
            , GuidingPrice = (float)1.5, TotalStock = 5, Blocked = false, UnitPerPackage = 1, QuantityDiscount = (float)0
            , ConfirmedDeliveryDate = DateTime.Now, ProductNumber = 100000, CountryOfOrigin = "Danmark", PurchasingManager = "Anders"
            , ProductCategory = "productCategoryTest" };
            return result;
        }
    }
}
