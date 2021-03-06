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
        TextFileWriter writer;

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

            writer = new TextFileWriter();

            writer.Flush();

            controller.NewProductRequested += NotNullProductEventArgs;

            //Act
            var expected = controller.CreateProduct();

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
