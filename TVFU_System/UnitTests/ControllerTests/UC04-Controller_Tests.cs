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
    public class UC04_ShowProductList_ControllerTests
    {
        ProductListController controller;
        TextProductRepo repo;
        TextFileWriter writer;

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void ShowProductListUnsorted()
        {
            //Arrange
            writer = new TextFileWriter();
            writer.Flush();

            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += CreateProduct1;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct1;
            controller.NewProductRequested += CreateProduct2;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct2;
            controller.NewProductRequested += CreateProduct3;

            controller.CreateProduct(null);

            controller.CloseProductList();

            //Act
            controller.ShowProductListRequested += () => { };

            controller.ShowProductList();

            //Assert
            List<ProductEventArgs> expectedResult = new List<ProductEventArgs>()
                { CreateProduct2(null, null), CreateProduct3(null, null), CreateProduct1(null, null) };

            Assert.AreEqual(expectedResult[0].ToString(), controller.CurrentProductListVM.ViewModels[0].ToString());
            Assert.AreEqual(expectedResult[1].ToString(), controller.CurrentProductListVM.ViewModels[1].ToString());
            Assert.AreEqual(expectedResult[2].ToString(), controller.CurrentProductListVM.ViewModels[2].ToString());
        }

        [TestMethod]
        public void ShowProductListSorted()
        {
            //Arrange
            writer = new TextFileWriter();
            writer.Flush();

            repo = new TextProductRepo();
            controller = new ProductListController(repo);

            controller.NewProductRequested += CreateProduct3;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct3;
            controller.NewProductRequested += CreateProduct1;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct1;
            controller.NewProductRequested += CreateProduct6;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct6;
            controller.NewProductRequested += CreateProduct2;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct2;
            controller.NewProductRequested += CreateProduct5;

            controller.CreateProduct(null);

            controller.NewProductRequested -= CreateProduct5;
            controller.NewProductRequested += CreateProduct4;

            controller.CreateProduct(null);

            controller.CloseProductList();

            //Act
            controller.ShowProductListRequested += () => { };

            controller.ShowProductList();

            //Assert
            List<ProductEventArgs> expectedResult = new List<ProductEventArgs>()
                { CreateProduct2(null, null), CreateProduct3(null, null), CreateProduct4(null, null)
                , CreateProduct5(null, null), CreateProduct6(null, null), CreateProduct1(null, null) };

            Assert.AreEqual(expectedResult[0].ToString(), controller.CurrentProductListVM.ViewModels[0].ToString());
            Assert.AreEqual(expectedResult[1].ToString(), controller.CurrentProductListVM.ViewModels[1].ToString());
            Assert.AreEqual(expectedResult[2].ToString(), controller.CurrentProductListVM.ViewModels[2].ToString());
            Assert.AreEqual(expectedResult[3].ToString(), controller.CurrentProductListVM.ViewModels[3].ToString());
            Assert.AreEqual(expectedResult[4].ToString(), controller.CurrentProductListVM.ViewModels[4].ToString());
            Assert.AreEqual(expectedResult[5].ToString(), controller.CurrentProductListVM.ViewModels[5].ToString());
        }

        private ProductEventArgs CreateProduct1(object sender, ProductEventArgs args)
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

        private ProductEventArgs CreateProduct2(object sender, ProductEventArgs args)
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
                ProductCategory = "Ape"
            };
            return result;
        }

        private ProductEventArgs CreateProduct3(object sender, ProductEventArgs args)
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
                ProductCategory = "Conservatory"
            };
            return result;
        }

        private ProductEventArgs CreateProduct4(object sender, ProductEventArgs args)
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
                ProductCategory = "Dependency"
            };
            return result;
        }

        private ProductEventArgs CreateProduct5(object sender, ProductEventArgs args)
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
                ProductCategory = "Equilibrium"
            };
            return result;
        }
        private ProductEventArgs CreateProduct6(object sender, ProductEventArgs args)
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
                ProductCategory = "Fuba"
            };
            return result;
        }

    }
}