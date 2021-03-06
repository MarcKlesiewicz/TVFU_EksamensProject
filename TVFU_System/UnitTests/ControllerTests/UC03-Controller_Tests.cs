using System;
using DomainLayer.EventArgs;
using Application.Controllers;
using Application.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.IO;
using UnitTests.Dummy;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UC03_Revider_Lagerliste
    {
        ProductListController controller;
        TextProductRepo repo;
        TextFileWriter writer;

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void CancelDeleteProduct()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            writer = new TextFileWriter();
            writer.Flush();

            controller.NewProductRequested += NotNullProductEventArgs;

            var createdProduct1 = new ProductViewModel(controller.CreateProduct());

            controller.ProductDeleteRequested += () => false;

            //Act
            controller.DeleteProduct(createdProduct1);

            //Assert
            Assert.AreEqual(1, controller.CurrentProductListVM.ViewModels.Where(s => s.Id == createdProduct1.Id).Count());

            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                Assert.IsNotNull(reader.ReadLine());

            }


        }

        [TestMethod]
        public void DeleteProductConfirmed()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            writer = new TextFileWriter();
            writer.Flush();

            controller.NewProductRequested += NotNullProductEventArgs;

            var createdProduct1 = new ProductViewModel(controller.CreateProduct());

            controller.ProductDeleteRequested += () => true;

            //Act
            controller.DeleteProduct(createdProduct1);

            //Assert
            Assert.AreEqual(0, controller.CurrentProductListVM.ViewModels.Where(s => s.Id == createdProduct1.Id).Count());


            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {

                try
                {
                    repo.Get(createdProduct1.Id);
                    Assert.Fail();
                }
                catch (KeyNotFoundException)
                {

                }
            }

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
    }
}