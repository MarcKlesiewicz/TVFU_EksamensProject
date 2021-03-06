using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories.Implementations;
using Persistence.Data;
using System.Data.SqlClient;
using DomainLayer.EventArgs;
using DomainLayer.Models;

namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class UC01_Repository_Tests
    {
        ProductRepo repo;
        List<ProductEventArgs> testList;

        [TestInitialize]
        public void Init()
        {
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                    ("begin " +
                    "DELETE FROM ProductDescription " +
                    "WHERE ProductNumber > 1899 AND ProductNumber < 2199 " +
                    "end " +
                    "begin " +
                    "DELETE FROM Product " +
                    "WHERE Number > 1899 AND Number < 2199 " +
                    "end "
                    , connection);
                command.ExecuteNonQuery();
            }
            repo = new ProductRepo();

        }

        [TestMethod]
        public void AddFourDifferentProducts()
        {
            //Arrange
            testList = new List<ProductEventArgs>()
            {
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test1",
                    UnitPrice = (float)0.0,
                    GuidingPrice = (float)0.5,
                    TotalStock = 1,
                    Blocked = true,
                    UnitPerPackage = 25,
                    QuantityDiscount = 0,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1900,
                    CountryOfOrigin = "Danmark",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Kubik"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test2",
                    UnitPrice = (float)0.5,
                    GuidingPrice = (float)3.5,
                    TotalStock = 1496984,
                    Blocked = false,
                    UnitPerPackage = 1,
                    QuantityDiscount = (float)6.5,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1901,
                    CountryOfOrigin = "Tyskland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test3",
                    UnitPrice = (float)0.7543,
                    GuidingPrice = (float)2.324,
                    TotalStock = 125134,
                    Blocked = false,
                    UnitPerPackage = 2,
                    QuantityDiscount = (float)4.5,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1902,
                    CountryOfOrigin = "Polen",
                    PurchasingManager = "Anders Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test4",
                    UnitPrice = (float)0.5527,
                    GuidingPrice = (float)2.6545,
                    TotalStock = 6874,
                    Blocked = false,
                    UnitPerPackage = 5,
                    QuantityDiscount = (float)0.8375,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1903,
                    CountryOfOrigin = "Hviderusland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                }
            };

            //Act
            repo.Add(testList[0]);
            repo.Add(testList[1]);
            repo.Add(testList[2]);
            repo.Add(testList[3]);

            //Assert
            List<Product> actualList = (repo.GetByProductNumber(1899, 2199) as List<Product>);
            string actualResult = String.Join("\n", actualList);
            string expectedResult = String.Join("\n", testList);

            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                    ("begin " +
                    "DELETE FROM ProductDescription " +
                    "WHERE ProductNumber > 1899 AND ProductNumber < 2199 " +
                    "end " +
                    "begin " +
                    "DELETE FROM Product " +
                    "WHERE Number > 1899 AND Number < 2199 " +
                    "end "
                    , connection);
                command.ExecuteNonQuery();
            }

            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void AddAProductWhoseProductNumberAlreadyExists()
        {
            //Arrange
            testList = new List<ProductEventArgs>()
            {
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test1",
                    UnitPrice = (float)0.0,
                    GuidingPrice = (float)0.5,
                    TotalStock = 1,
                    Blocked = true,
                    UnitPerPackage = 25,
                    QuantityDiscount = 0,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1900,
                    CountryOfOrigin = "Danmark",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Kubik"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test2",
                    UnitPrice = (float)0.5,
                    GuidingPrice = (float)3.5,
                    TotalStock = 1496984,
                    Blocked = false,
                    UnitPerPackage = 1,
                    QuantityDiscount = (float)6.5,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1901,
                    CountryOfOrigin = "Tyskland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test3",
                    UnitPrice = (float)0.7543,
                    GuidingPrice = (float)2.324,
                    TotalStock = 125134,
                    Blocked = false,
                    UnitPerPackage = 2,
                    QuantityDiscount = (float)4.5,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1902,
                    CountryOfOrigin = "Polen",
                    PurchasingManager = "Anders Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test4",
                    UnitPrice = (float)0.5527,
                    GuidingPrice = (float)2.6545,
                    TotalStock = 6874,
                    Blocked = false,
                    UnitPerPackage = 5,
                    QuantityDiscount = (float)0.8375,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1903,
                    CountryOfOrigin = "Hviderusland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test5",
                    UnitPrice = (float)0.5527,
                    GuidingPrice = (float)2.6545,
                    TotalStock = 6874,
                    Blocked = false,
                    UnitPerPackage = 5,
                    QuantityDiscount = (float)0.8375,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1903,
                    CountryOfOrigin = "Hviderusland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                }
            };

            //Act
            repo.Add(testList[0]);
            repo.Add(testList[1]);
            repo.Add(testList[2]);
            repo.Add(testList[3]);
            try
            {
                repo.Add(testList[4]);
                Assert.Fail();
            }
            //Assert
            catch (DuplicateWaitObjectException)
            {
                var actual = (repo.GetByProductNumber(1902, 1904) as List<Product>);
                Assert.AreEqual(1, actual.Count);
            }
            finally
            {
                using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand
                        ("begin " +
                        "DELETE FROM ProductDescription " +
                        "WHERE ProductNumber > 1899 AND ProductNumber < 2199 " +
                        "end " +
                        "begin " +
                        "DELETE FROM Product " +
                        "WHERE Number > 1899 AND Number < 2199 " +
                        "end "
                        , connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        [TestMethod]
        public void AddAProductWhoseDescriptionAlreadyExists()
        {
            //Arrange
            testList = new List<ProductEventArgs>()
            {
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test1",
                    UnitPrice = (float)0.0,
                    GuidingPrice = (float)0.5,
                    TotalStock = 1,
                    Blocked = true,
                    UnitPerPackage = 25,
                    QuantityDiscount = 0,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1900,
                    CountryOfOrigin = "Danmark",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Kubik"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test2",
                    UnitPrice = (float)0.5,
                    GuidingPrice = (float)3.5,
                    TotalStock = 1496984,
                    Blocked = false,
                    UnitPerPackage = 1,
                    QuantityDiscount = (float)6.5,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1901,
                    CountryOfOrigin = "Tyskland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test3",
                    UnitPrice = (float)0.7543,
                    GuidingPrice = (float)2.324,
                    TotalStock = 125134,
                    Blocked = false,
                    UnitPerPackage = 2,
                    QuantityDiscount = (float)4.5,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1902,
                    CountryOfOrigin = "Polen",
                    PurchasingManager = "Anders Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test4",
                    UnitPrice = (float)0.5527,
                    GuidingPrice = (float)2.6545,
                    TotalStock = 6874,
                    Blocked = false,
                    UnitPerPackage = 5,
                    QuantityDiscount = (float)0.8375,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1903,
                    CountryOfOrigin = "Hviderusland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                },
                new ProductEventArgs()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "test4",
                    UnitPrice = (float)0.5527,
                    GuidingPrice = (float)2.6545,
                    TotalStock = 6874,
                    Blocked = false,
                    UnitPerPackage = 5,
                    QuantityDiscount = (float)0.8375,
                    ConfirmedDeliveryDate = DateTime.Today,
                    ProductNumber = 1904,
                    CountryOfOrigin = "Hviderusland",
                    PurchasingManager = "Henrik Lange",
                    ProductCategory = "Vase"
                }
            };

            //Act
            repo.Add(testList[0]);
            repo.Add(testList[1]);
            repo.Add(testList[2]);
            repo.Add(testList[3]);
            try
            {
                repo.Add(testList[4]);
                Assert.Fail();
            }
            //Assert
            catch (DuplicateWaitObjectException)
            {
                using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand
                        ("BEGIN " +
                        "SELECT * " +
                        "FROM dbo.Product " +
                        "WHERE Number = 1904 " +
                        "END"
                        , connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Assert.IsFalse(reader.Read());
                    }
                }
            }
            finally
            {
                using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand
                        ("begin " +
                        "DELETE FROM ProductDescription " +
                        "WHERE ProductNumber > 1899 AND ProductNumber < 2199 " +
                        "end " +
                        "begin " +
                        "DELETE FROM Product " +
                        "WHERE Number > 1899 AND Number < 2199 " +
                        "end "
                        , connection);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}