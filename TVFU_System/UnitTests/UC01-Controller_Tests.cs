using System;
using DomainLayer.EventArgs;
using Application.Controllers;
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
        TextFileWriter dataWriter;

        [TestInitialize]
        public void Init()
        {
            dataWriter = new TextFileWriter();
            repo = new ProductRepo(dataWriter);
        }

        [TestMethod]
        public void CanCreateProductTestWithNull()
        {
            //Arrange
            controller = new ProductListController(repo);

            //Act
            controller.NewProductRequested += NewProductRequestedHandlerNull;
            controller.CreateProduct(null);

            //Assert
            using (StreamReader reader = new StreamReader(@"Data\TextFile.ini"))
            {
                Assert.AreEqual(true, reader.EndOfStream);
            }
        }

        [TestMethod]
        public void CanCreateProductTestWithoutNull()
        {
            //Arrange
            controller = new ProductListController(repo);

            //Act
            controller.NewProductRequested += NewProductRequestedHandlerNotNull;
            controller.CreateProduct(null);

            //Assert
            using (StreamReader reader = new StreamReader(@"Data\TextFile.ini"))
            {
                Assert.AreEqual("Mette Frederiksen", reader.ReadLine());
            }
        }

        private ProductEventArgs NewProductRequestedHandlerNull(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = null;
            return result;
        }

        private ProductEventArgs NewProductRequestedHandlerNotNull(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = new ProductEventArgs() { Name = "Mette Frederiksen" };
            return result;
        }
    }
}
