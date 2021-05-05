using DomainLayer.Models;
using Persistence.Repositories.Interfaces;
using DomainLayer.EventArgs;
using System.Data.SqlClient;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Persistence.Repositories.Implementations
{
    public class ProductRepo : IProductRepo
    {
        /// <summary>
        /// Saves the given parameter in an SQL Server database
        /// </summary>
        /// <param name="args">Expected to be of type ProductEventArgs, with no properties being null</param>
        public void Add(EventArgs args)
        {
            var productArgs = (args as ProductEventArgs);
            string commandText = "EXEC AddProduct @ID, @Description, @UnitPrice, @GuidingPrice, @TotalStock, @Blocked, @UnitPerPackage" +
                ", @QuantityDiscount, @ConfirmedDeliveryDate, @ProductNumber, @CountryOfOrigin, @PurchasingManager, @ProductCategory";
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = productArgs.Id;
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = productArgs.Description;
                command.Parameters.Add("@UnitPrice", System.Data.SqlDbType.Float).Value = productArgs.UnitPrice;
                command.Parameters.Add("@GuidingPrice", System.Data.SqlDbType.Float).Value = productArgs.GuidingPrice;
                command.Parameters.Add("@TotalStock", System.Data.SqlDbType.Int).Value = productArgs.TotalStock;
                command.Parameters.Add("@Blocked", System.Data.SqlDbType.Bit).Value = productArgs.Blocked;
                command.Parameters.Add("@UnitPerPackage", System.Data.SqlDbType.Int).Value = productArgs.UnitPerPackage;
                command.Parameters.Add("@QuantityDiscount", System.Data.SqlDbType.Float).Value = productArgs.QuantityDiscount;
                command.Parameters.Add("@ConfirmedDeliveryDate", System.Data.SqlDbType.SmallDateTime).Value = productArgs.ConfirmedDeliveryDate;
                command.Parameters.Add("@ProductNumber", System.Data.SqlDbType.Int).Value = productArgs.ProductNumber;
                command.Parameters.Add("@CountryOfOrigin", System.Data.SqlDbType.VarChar).Value = productArgs.CountryOfOrigin;
                command.Parameters.Add("@PurchasingManager", System.Data.SqlDbType.VarChar).Value = productArgs.PurchasingManager;
                command.Parameters.Add("@ProductCategory", System.Data.SqlDbType.VarChar).Value = productArgs.ProductCategory;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("ProductNumber_UC"))
                    {
                        throw new DuplicateWaitObjectException("The Product Number already exists within the database");
                    } 
                    else if (ex.Message.Contains("Description_UC"))
                    {
                        string cleanUpCommandText = "BEGIN " +
                            "DELETE FROM dbo.Product " +
                            "WHERE Number = @ProductNumber " +
                            "END";
                        using (SqlConnection cleanUpConnection = new SqlConnection(new Connector().ConnectionString))
                        {
                            cleanUpConnection.Open();
                            SqlCommand cleanUpCommand = new SqlCommand(cleanUpCommandText, cleanUpConnection);

                            cleanUpCommand.Parameters.Add("@ProductNumber", System.Data.SqlDbType.Int).Value = productArgs.ProductNumber;

                            cleanUpCommand.ExecuteNonQuery();
                        }
                        throw new DuplicateWaitObjectException("The Product Description already exists within the database");
                    } 
                    else
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Product Get(string guid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all entries in the SQL Server database sorted by ProductCategory.
        /// Instantiates objects of type 'Product' for each entry in the database
        /// </summary>
        /// <returns>A List of type 'Product'</returns>
        public IEnumerable GetByProductCategories()
        {
            string commandText = "EXEC GetByProductCategories";
            var products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product()
                        {
                            Id = (string)reader["ID"],
                            Description = (string)reader["Description"],
                            UnitPrice = (float)reader["UnitPrice"],
                            GuidingPrice = (float)reader["GuidingPrice"],
                            TotalStock = (int)reader["TotalStock"],
                            Blocked = (bool)reader["Blocked"],
                            UnitPerPackage = (int)reader["UnitPerPackage"],
                            QuantityDiscount = (float)reader["QuantityDiscount"],
                            ConfirmedDeliveryDate = (DateTime)reader["ConfirmedDeliveryDate"],
                            ProductNumber = (int)reader["ProductNumber"],
                            CountryOfOrigin = (string)reader["CountryOfOrigin"],
                            PurchasingManager = (string)reader["Name"],
                            ProductCategory = (string)reader["ProductCategory"]
                        });
                    }
                }
            }
            return products;
        }

        /// <summary>
        /// Gets all entries in the SQL Server database, that has a ProductNumber in between two given integers.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IEnumerable GetByProductNumber(int min, int max)
        {
            string commandText = "BEGIN " +
                "SET NOCOUNT ON " +
                "SELECT dbo.ProductDescription.ID,Description,UnitPrice,GuidingPrice" +
                ",TotalStock,Blocked,UnitPerPackage,QuantityDiscount,ConfirmedDeliveryDate" +
                ",ProductNumber,CountryOfOrigin,ProductCategory,dbo.PurchasingManager.Name " +
                "FROM dbo.ProductDescription " +
                "INNER JOIN dbo.PurchasingManager ON dbo.ProductDescription.PurchasingManagerID = dbo.PurchasingManager.ID " +
                "WHERE dbo.ProductDescription.ProductNumber > @ProductNumberMin AND dbo.ProductDescription.ProductNumber < @ProductNumberMax " +
                "ORDER BY ProductCategory ASC, ProductNumber ASC " +
                "END";
            var products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@ProductNumberMin", System.Data.SqlDbType.Int).Value = min;
                command.Parameters.Add("@ProductNumberMax", System.Data.SqlDbType.Int).Value = max;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product()
                        {
                            Id = (string)reader["ID"],
                            Description = (string)reader["Description"],
                            UnitPrice = (float)reader["UnitPrice"],
                            GuidingPrice = (float)reader["GuidingPrice"],
                            TotalStock = (int)reader["TotalStock"],
                            Blocked = (bool)reader["Blocked"],
                            UnitPerPackage = (int)reader["UnitPerPackage"],
                            QuantityDiscount = (float)reader["QuantityDiscount"],
                            ConfirmedDeliveryDate = (DateTime)reader["ConfirmedDeliveryDate"],
                            ProductNumber = (int)reader["ProductNumber"],
                            CountryOfOrigin = (string)reader["CountryOfOrigin"],
                            PurchasingManager = (string)reader["Name"],
                            ProductCategory = (string)reader["ProductCategory"]
                        });
                    }
                }
            }
            return products;
        }

        /// <summary>
        /// Removes a single entry in the SQL Server Database whose ID matches exactly with the given paramater.
        /// </summary>
        /// <param name="guid">A Guid represented by a string object</param>
        public void Remove(string guid)
        {
            string commandText = "EXEC DeleteProduct @ID";
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("ID", System.Data.SqlDbType.VarChar).Value = guid;

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates a single entry in the SQL Server Database - whose ID matches exactly with the parameter's 'ID' property - 
        /// with the information in the parameter's properties (except the 'ID' property)
        /// </summary>
        /// <param name="args">Expected to be of type ProductEventArgs, with no properties being null</param>
        public void Update(EventArgs args)
        {
            var productArgs = (args as ProductEventArgs);
            string commandText = "EXEC UpdateProduct @ID, @Description, @UnitPrice, @GuidingPrice, @TotalStock, @Blocked, @UnitPerPackage" +
                ", @QuantityDiscount, @ConfirmedDeliveryDate, @ProductNumber, @CountryOfOrigin, @PurchasingManager, @ProductCategory";
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = productArgs.Id;
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = productArgs.Description;
                command.Parameters.Add("@UnitPrice", System.Data.SqlDbType.Float).Value = productArgs.UnitPrice;
                command.Parameters.Add("@GuidingPrice", System.Data.SqlDbType.Float).Value = productArgs.GuidingPrice;
                command.Parameters.Add("@TotalStock", System.Data.SqlDbType.Int).Value = productArgs.TotalStock;
                command.Parameters.Add("@Blocked", System.Data.SqlDbType.Bit).Value = productArgs.Blocked;
                command.Parameters.Add("@UnitPerPackage", System.Data.SqlDbType.Int).Value = productArgs.UnitPerPackage;
                command.Parameters.Add("@QuantityDiscount", System.Data.SqlDbType.Float).Value = productArgs.QuantityDiscount;
                command.Parameters.Add("@ConfirmedDeliveryDate", System.Data.SqlDbType.SmallDateTime).Value = productArgs.ConfirmedDeliveryDate;
                command.Parameters.Add("@ProductNumber", System.Data.SqlDbType.Int).Value = productArgs.ProductNumber;
                command.Parameters.Add("@CountryOfOrigin", System.Data.SqlDbType.VarChar).Value = productArgs.CountryOfOrigin;
                command.Parameters.Add("@PurchasingManager", System.Data.SqlDbType.VarChar).Value = productArgs.PurchasingManager;
                command.Parameters.Add("@ProductCategory", System.Data.SqlDbType.VarChar).Value = productArgs.ProductCategory;

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// A method which creates a temperary list of products in the database, using the SearchInAColumn stored procedure.
        /// Then filtrering the temperary list by FilterCategory, FilterColor, FilterTreeSort.
        /// Which is returned and set to the CurrentProductListVM. 
        /// </summary>
        /// <param name="args">Expected to be of type FilterEventArgs, with no properties being null</param>
        public IEnumerable FilterAndSearchProductList(EventArgs args)
        {
            var filterEventArgs = (args as FilterEventArgs);

            string commandText = "EXEC FilterAndSearch @SearchFor, @searchIn, @FilterCategory, @FilterColor, @FilterTreeSort";
            var searchedProductList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@searchFor", System.Data.SqlDbType.VarChar).Value = filterEventArgs.SearchWord;
                command.Parameters.Add("@searchIn", System.Data.SqlDbType.VarChar).Value = filterEventArgs.SearchCategory;
                command.Parameters.Add("@FilterCategory", System.Data.SqlDbType.VarChar).Value = filterEventArgs.FilterCategory;
                command.Parameters.Add("@FilterColor", System.Data.SqlDbType.VarChar).Value = filterEventArgs.FilterColour;
                command.Parameters.Add("@FilterTreeSort", System.Data.SqlDbType.VarChar).Value = filterEventArgs.FilterTreeSort;


                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        searchedProductList.Add(new Product()
                        {
                            Id = (string)reader["ID"],
                            Description = (string)reader["Description"],
                            UnitPrice = (float)reader["UnitPrice"],
                            GuidingPrice = (float)reader["GuidingPrice"],
                            TotalStock = (int)reader["TotalStock"],
                            Blocked = (bool)reader["Blocked"],
                            UnitPerPackage = (int)reader["UnitPerPackage"],
                            QuantityDiscount = (float)reader["QuantityDiscount"],
                            ConfirmedDeliveryDate = (DateTime)reader["ConfirmedDeliveryDate"],
                            ProductNumber = (int)reader["ProductNumber"],
                            CountryOfOrigin = (string)reader["CountryOfOrigin"],
                            PurchasingManager = (string)reader["PurchasingManagerName"],
                            ProductCategory = (string)reader["ProductCategory"]
                        });
                    }
                }
            }
            return searchedProductList;

        }

        public IEnumerable SortAfter(string sortCategory, string order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable SearchProductList(string searchCategory, string searchWord)
        {
            throw new NotImplementedException();
        }
    }
}
