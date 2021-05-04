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
            var createdProduct = new ProductViewModel(controller.CreateProduct());

            controller.ProductUpdateRequested += NullProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct);

            //Assert
            Assert.AreEqual(NotNullProductEventArgs().ToString(), repo.Get(createdProduct.Id).ToString());
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
            var createdProduct = new ProductViewModel(controller.CreateProduct());

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct);

            //Assert
            Assert.AreEqual(UpdatedProductEventArgs().ToString(), repo.Get(createdProduct.Id).ToString());
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

            ProductViewModel createdProduct = null;

            for (int i = 0; i < 57; i++)
            {
                if (i == 23)
                {
                    createdProduct = new ProductViewModel(controller.CreateProduct());
                } else
                {
                    controller.CreateProduct();
                }
            }

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            controller.ChangeProduct(createdProduct);

            //Assert
            Assert.AreEqual(UpdatedProductEventArgs().ToString(), repo.Get(createdProduct.Id).ToString());
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
                    productsToBeUpdated.Add(new ProductViewModel(controller.CreateProduct()));
                else
                    controller.CreateProduct();
            }

            controller.ProductUpdateRequested += UpdatedProductEventArgs;

            //Act
            for (int i = 0; i < productsToBeUpdated.Count; i++)
                controller.ChangeProduct(productsToBeUpdated[i]);

            //Assert
            List<string> expectedUpdatedText = new List<string>();
            for (int i = 0; i < productsToBeUpdated.Count; i++)
                expectedUpdatedText.Add(UpdatedProductEventArgs().ToString());
            string expected = String.Join("\n", expectedUpdatedText);
            List<string> actualUpdatedText = new List<string>();
            for (int i = 0; i < productsToBeUpdated.Count; i++)
                actualUpdatedText.Add(repo.Get(productsToBeUpdated[i].Id).ToString());
            string actual = String.Join("\n", actualUpdatedText);
            Assert.AreEqual(expected, actual);
        }

        private ProductEventArgs NullProductEventArgs()
        {
            ProductEventArgs result = null;
            return result;
        }

        private ProductEventArgs NotNullProductEventArgs()
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

        private ProductEventArgs UpdatedProductEventArgs()
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
