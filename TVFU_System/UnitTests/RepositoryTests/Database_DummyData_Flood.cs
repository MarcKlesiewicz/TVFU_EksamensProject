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
    ///Know what you're doing before you outcomment this code and run it!

    //[TestClass]
    //public class Database_DummyData_Flood
    //{
    //    ProductRepo repo;
    //    List<ProductEventArgs> testList;
    //    Random random;

    //    [TestInitialize]
    //    public void Init()
    //    {
    //        repo = new ProductRepo();
    //        random = new Random();
    //        testList = new List<ProductEventArgs>();
    //        //for (int i = 0; i < 1000; i++)
    //        //{
    //        //    if (i % 3 == 0)
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Papkasse {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Kasse"
    //        //        });
    //        //    }
    //        //    else if (i % 5 == 0)
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Skuffe {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Skuffe"
    //        //        });
    //        //    }
    //        //    else if (i % 7 == 0)
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Kubik montage {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Kubik"
    //        //        });
    //        //    }
    //        //    else if (i % 11 == 0)
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Reol gøgl {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Reol"
    //        //        });
    //        //    }
    //        //    else if (i % 13 == 0)
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Hylde {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Hylde"
    //        //        });
    //        //    }
    //        //    else if (i % 17 == 0)
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Sokkel {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Sokkel"
    //        //        });
    //        //    }
    //        //    else
    //        //    {
    //        //        testList.Add(new ProductEventArgs()
    //        //        {
    //        //            Id = Guid.NewGuid().ToString(),
    //        //            Description = $"Dubloklods (træ) {100 + i} mm",
    //        //            UnitPrice = (float)random.Next(1, 10000),
    //        //            GuidingPrice = (float)random.Next(1, 10000),
    //        //            TotalStock = random.Next(0, 100000),
    //        //            Blocked = (random.Next(0, 15) == 0) ? true : false,
    //        //            UnitPerPackage = random.Next(1, 1000),
    //        //            QuantityDiscount = random.Next(0, 1000),
    //        //            ConfirmedDeliveryDate = DateTime.Today,
    //        //            ProductNumber = 100 + i,
    //        //            CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //        //            PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //        //            ProductCategory = "Dublo"
    //        //        });
    //        //    }
    //        //}
    //        for (int i = 0; i < 4000; i++)
    //        {
    //            string dateTime = $"{random.Next(10, 31)}/{random.Next(10, 12)}/{random.Next(2000, 2021)}";
    //            if (i % 3 == 0)
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"ABC Grundreol Birk Grøn {100 + i} mm" : $"ABC Tilbygningsreol Kirsebær Blå {100 + i} mm",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "ABC",
    //                });
    //            }
    //            else if (i % 5 == 0)
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"KUBIK Color hel reolkasse Akacie Sort {100 + i} mm" : $"KUBIK halv reolkasse Fyr Grøn {100 + i} mm",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "KUBIK"
    //                });
    //            }
    //            else if (i % 7 == 0)
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"Lænestol i formspændt Bøg Rød {100 + i} mm" : $"Fodskammel i formspændt i formspændt Eg Gul {100 + i} mm",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "Møbler"
    //                });
    //            }
    //            else if (i % 11 == 0)
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"Sticks bordplade Birk Rød {100 + i}x{90 + i} mm" : $"Sticks bordplade Akacie Hvid {100 + i}x{90 + i} mm",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "Køkken"
    //                });
    //            }
    //            else if (i % 13 == 0)
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"Musvitkasse Kirsebær Gul {100 + i} mm hul" : $"Mejsekasse Fyr Hvid {100 + i} mm hul",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "Have"
    //                });
    //            }
    //            else if (i % 17 == 0)
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"Le Corbusier stol Bøg Blå {100 + i} mm" : $"Le Corbusier stol Eg Gul {100 + i} mm",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "Møbler"
    //                });
    //            }
    //            else
    //            {
    //                testList.Add(new ProductEventArgs()
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    Description = (random.Next(0, 1) == 0) ? $"Vitrineskab i Kirsebær Hvid {100 + i} mm" : $"Vitrineskab i Birk Sort {100 + i} mm",
    //                    UnitPrice = (float)random.Next(1, 10000),
    //                    GuidingPrice = (float)random.Next(1, 10000),
    //                    TotalStock = random.Next(0, 100000),
    //                    Blocked = (random.Next(0, 15) == 0) ? true : false,
    //                    UnitPerPackage = random.Next(1, 1000),
    //                    QuantityDiscount = random.Next(0, 1000),
    //                    ConfirmedDeliveryDate = DateTime.Parse(dateTime),
    //                    ProductNumber = 100 + i,
    //                    CountryOfOrigin = (random.Next(0, 8) == 0) ? "Tyskland" : "Danmark",
    //                    PurchasingManager = (random.Next(0, 1) == 0) ? "Henrik Lange" : "Anders Lange",
    //                    ProductCategory = "Møbler"
    //                });
    //            }
    //        }
    //    }

    //    [TestMethod]
    //    public void Flood()
    //    {
    //        for (int i = 0; i < testList.Count; i++)
    //        {
    //            repo.Add(testList[i]);
    //        }
    //        Assert.IsTrue(true);
    //    }
    //}
}
