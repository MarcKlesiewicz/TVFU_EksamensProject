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

        public Product Get(string guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

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
    }
}