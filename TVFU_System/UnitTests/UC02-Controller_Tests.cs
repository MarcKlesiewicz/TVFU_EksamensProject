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
            var createdProduct = new ProductViewModel(controller.CreateProduct(null));

            controller.ProductUpdateRequested += NullProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct);

            //Assert
            Assert.AreEqual(NotNullProductEventArgs(null, null).ToString(), repo.Get(createdProduct.Id).ToString());
        }

        [TestMethod]
        public void ChangeProductConfirmedWithOneEntryInRepository()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += NotNullProductEventArgs;
            var createdProduct = new ProductViewModel(controller.CreateProduct(null));

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct);

            //Assert
            Assert.AreEqual(UpdatedProductEventArgs(null, null).ToString(), repo.Get(createdProduct.Id).ToString());
        }

        [TestMethod]
        public void ChangeProductConfirmedWithManyEntriesInRepository()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += NotNullProductEventArgs;

            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            var createdProduct = new ProductViewModel(controller.CreateProduct(null));
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct);

            //Assert
            Assert.AreEqual(UpdatedProductEventArgs(null, null).ToString(), repo.Get(createdProduct.Id).ToString());
        }

        [TestMethod]
        public void ChangeManyProductsConfirmedWithManyEntriesInRepository()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += NotNullProductEventArgs;

            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            var createdProduct1 = new ProductViewModel(controller.CreateProduct(null));
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            var createdProduct2 = new ProductViewModel(controller.CreateProduct(null));
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            controller.CreateProduct(null);
            var createdProduct3 = new ProductViewModel(controller.CreateProduct(null));
            controller.CreateProduct(null);
            controller.CreateProduct(null);

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct1);
            controller.ChangeProduct(createdProduct2);
            controller.ChangeProduct(createdProduct3);

            //Assert
            Assert.AreEqual(UpdatedProductEventArgs(null, null).ToString(), repo.Get(createdProduct1.Id).ToString());
            Assert.AreEqual(UpdatedProductEventArgs(null, null).ToString(), repo.Get(createdProduct2.Id).ToString());
            Assert.AreEqual(UpdatedProductEventArgs(null, null).ToString(), repo.Get(createdProduct3.Id).ToString());
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
                PurchasingManager = "Anders",
                ProductCategory = "productCategoryTest"
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
                PurchasingManager = "Simon",
                ProductCategory = "changedProductCategoryTest"
            };
            return result;
        }
    }
}
