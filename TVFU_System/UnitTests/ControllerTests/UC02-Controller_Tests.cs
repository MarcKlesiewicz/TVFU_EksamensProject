using System;
using DomainLayer.EventArgs;
using Application.Controllers;
using Application.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.IO;
using UnitTests.Dummy;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UC02_Controller_Tests
    {
        ProductListController controller;
        TextProductRepo repo;
        TextFileWriter writer;

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

            writer = new TextFileWriter();
            writer.Flush();

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

            writer = new TextFileWriter();
            writer.Flush();

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

            writer = new TextFileWriter();
            writer.Flush();

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

            writer = new TextFileWriter();
            writer.Flush();

            controller.NewProductRequested += NotNullProductEventArgs;

            List<ProductViewModel> productsToBeUpdated = new List<ProductViewModel>();

            for (int i = 1; i < 100; i++)
            {
                if (i % 4 == 0)
                    productsToBeUpdated.Add(new ProductViewModel(controller.CreateProduct(null)));
                else
                    controller.CreateProduct(null);
            }

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            for (int i = 0; i < productsToBeUpdated.Count; i++)
                controller.ChangeProduct(productsToBeUpdated[i]);

            //Assert
            List<string> expectedUpdatedText = new List<string>();
            for (int i = 0; i < productsToBeUpdated.Count; i++)
                expectedUpdatedText.Add(UpdatedProductEventArgs(null, null).ToString());
            string expected = String.Join("\n", expectedUpdatedText);
            List<string> actualUpdatedText = new List<string>();
            for (int i = 0; i < productsToBeUpdated.Count; i++)
                actualUpdatedText.Add(repo.Get(productsToBeUpdated[i].Id).ToString());
            string actual = String.Join("\n", actualUpdatedText);
            Assert.AreEqual(expected, actual);
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
