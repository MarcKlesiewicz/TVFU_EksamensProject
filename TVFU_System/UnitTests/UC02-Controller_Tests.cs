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
    public class UC02_Controller_Tests
    {
        ProductListController controller;
        TextProductRepo repo;

        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod]
        public void ChangeProductCancel()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += NotNullProductEventArgs;
            controller.CreateProduct(null);

            controller.ProductUpdateRequested += NullProductEventArgs;

            //Act
            controller.ChangeProduct(controller.CurrentProductVM);

            //Assert
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                Assert.AreEqual(NotNullProductEventArgs(null, null).ToString(), reader.ReadLine());
            }
        }

        [TestMethod]
        public void ChangeProductConfirmed()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += NotNullProductEventArgs;
            controller.CreateProduct(null);

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            controller.ChangeProduct(controller.CurrentProductVM);

            //Assert
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                string[] line = reader.ReadLine().Split(',');

                Assert.AreEqual(UpdatedProductEventArgs(null, null).ToString(), line.);
            }
        }

        private ProductEventArgs NullProductEventArgs(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = null;
            return result;
        }

        private ProductEventArgs NotNullProductEventArgs(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = new ProductEventArgs()
            {
                Description = "descriptionTest",
                UnitPrice = (float)1.1,
                GuidingPrice = (float)1.5,
                TotalStock = 5,
                Blocked = false,
                UnitPerPackage = 1,
                QuantityDiscount = (float)0,
                ConfirmedDeliveryDate = DateTime.Now,
                ProductNumber = 100000,
                CountryOfOrigin = "Danmark",
                PurchasingManager = "Anders"
            };
            return result;
        }

        private ProductEventArgs UpdatedProductEventArgs(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = new ProductEventArgs()
            {
                Description = "changedDescriptionTest",
                UnitPrice = (float)1.1,
                GuidingPrice = (float)1.5,
                TotalStock = 5,
                Blocked = false,
                UnitPerPackage = 1,
                QuantityDiscount = (float)0,
                ConfirmedDeliveryDate = DateTime.Now,
                ProductNumber = 100000,
                CountryOfOrigin = "Tyskland",
                PurchasingManager = "Simon"
            };
            return result;
        }
    }
}
