using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UC03_Revider_Lagerliste
    {
        ProductListController controller;
        TextProductRepo Repo;

        [TestInitialize]
        public void init
        {
        }

        [TestMethod]
        public void DeleteProduct()
        {
            Repo = new TextProductRepo();
            controller = new ProductListController(Repo);
            controller.NewProductRequested += NotNullProductEventArgs;

            controller.CreateProduct();
        }
    }
}
