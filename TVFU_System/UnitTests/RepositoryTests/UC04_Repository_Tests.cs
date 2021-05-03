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
using Application.ViewModels;
using UnitTests.Dummy;

namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class UC04_Repository_Tests
    {
        ProductRepo repo;
        TextProductRepo textRepo;
        List<Product> testList;

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
            textRepo = new TextProductRepo();
        }

        [TestMethod]
        public void GetProductListByCategoriesWith1000EntriesAmountOfTime()
        {
            repo.GetByProductCategories();
        }

        [TestMethod]
        public void GetProductListByCategoriesTwiceWith1000EntriesAmountOfTime()
        {
            repo.GetByProductCategories();

            repo.GetByProductCategories();
        }

        [TestMethod]
        public void ShowProductListSorted()
        {
            //Arrange
            string productsCategory = "";
            string sortedProductsCategory = "";

            //Act
            testList = repo.GetByProductCategories() as List<Product>;

            for (int i = 0; i < testList.Count; i++)
            {
                productsCategory += testList[i].ProductCategory + "\n";
            }

            Sort(ref testList);
            
            for (int i = 0; i < testList.Count; i++)
            {
                sortedProductsCategory += testList[i].ProductCategory + "\n";
            }

            //Assert
            Assert.AreEqual(sortedProductsCategory, productsCategory);
        }

        private void Sort(ref List<Product> products)
        {
            for (int i = products.Count - 1; i > 1; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (products[j].CompareTo(products[j + 1]) > 0)
                    {
                        var temp = products[j];
                        products[j] = products[j + 1];
                        products[j + 1] = temp;
                    }
                }
            }
        }
    }
}
