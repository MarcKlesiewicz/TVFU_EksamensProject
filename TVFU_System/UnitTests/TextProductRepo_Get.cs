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
    public class TextProductRepo_Get
    {
        ProductListController controller;
        TextProductRepo repo;

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void GetWithOneEntry()
        {
            //Arrange
            repo = new TextProductRepo();
            controller = new ProductListController(repo);
            controller.NewProductRequested += NotNullProductEventArgs;

            //Act
            var expected = controller.CreateProduct(null);

            //Assert
            using (StreamReader reader = new StreamReader(@"Dummy\TextFile.ini"))
            {
                string[] line = reader.ReadLine().Split(';');
                string result = "";
                for (int i = 1; i < line.Length; i++)
                {
                    result += line[i];
                    if (i < line.Length - 1)
                    {
                        result += ";";
                    }
                }
                Assert.AreEqual(expected.ToString(), result);
            }
        }

        private ProductEventArgs NotNullProductEventArgs(object sender, ProductEventArgs args)
        {
            ProductEventArgs result = new ProductEventArgs() { Description = "descriptionTest", UnitPrice = (float)1.1
            , GuidingPrice = (float)1.5, TotalStock = 5, Blocked = false, UnitPerPackage = 1, QuantityDiscount = (float)0
            , ConfirmedDeliveryDate = DateTime.Now, ProductNumber = 100000, CountryOfOrigin = "Danmark", PurchasingManager = "Anders" };
            return result;
        }
    }
}
