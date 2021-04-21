using System;
using DomainLayer.EventArgs;
using Application.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.IO;
using Persistence.Repositories.Interfaces;

namespace UnitTests
{
    /// <summary>
    /// Summary description for UC02_Revider_Produktinformation
    /// </summary>
    [TestClass]
    public class UC02_Revider_Produktinformation
    {
        public UC02_Revider_Produktinformation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        ProductListController controller;
        IProductRepo repo;

        [TestInitialize]
        public void init()
        {

        }
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NewProductChange()
        {
            //Arrange
            controller = new ProductListController(repo);

            //Act
            controller.NewProductChange += NewProductRequestedHandlerNotNull;
            controller.ChangeProduct(null);

            //Assert
            using (StreamReader reader = new StreamReader(@"Data\TextFile.ini"))
            {
                Assert.AreEqual(true, reader.EndOfStream);
            }
        }

        private ProductEventArgs NewProductRequestedHandlerNull(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = null;
            return result;
        }

        private ProductEventArgs NewProductRequestedHandlerNotNull(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = new ProductEventArgs() { Name = "Anders Fogh" };
            return result;
        }
    }
}
