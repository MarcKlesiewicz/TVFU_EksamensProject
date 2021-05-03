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
        TextFileWriter writer;

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

            writer = new TextFileWriter();
            writer.Flush();

            controller.NewProductRequested += NullProductEventArgs;

            //Act
            controller.CreateProduct();

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

            writer = new TextFileWriter();
            writer.Flush();

            controller.NewProductRequested += NotNullProductEventArgs;

            //Act
            controller.CreateProduct();

            //Assert
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                Assert.IsNotNull(reader.ReadLine());
            }
        }

        private ProductEventArgs NullProductEventArgs()
        {
            ProductEventArgs result = null;
            return result;
        }

        private ProductEventArgs NotNullProductEventArgs()
        {
            ProductEventArgs result = new ProductEventArgs() { Description = "descriptionTest", UnitPrice = (float)1.1
            , GuidingPrice = (float)1.5, TotalStock = 5, Blocked = false, UnitPerPackage = 1, QuantityDiscount = (float)0
            , ConfirmedDeliveryDate = DateTime.Now, ProductNumber = 100000, CountryOfOrigin = "Danmark", PurchasingManager = "Anders"
            , ProductCategory = "productCategoryTest" };
            return result;
        }
    }
}
