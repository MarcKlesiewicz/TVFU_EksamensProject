using DomainLayer.Models;
using Persistence.Repositories.Interfaces;
using DomainLayer.EventArgs;
using System.Data.SqlClient;
using Persistence.Data;
using System;
using System.Collections.Generic;

namespace Persistence.Repositories.Implementations
{
    public class ProductRepo : IProductRepo
    {

        public void Add(EventArgs args)
        {
            var productArgs = (args as ProductEventArgs);
            string commandText = "EXEC AddProduct @ID, @Description, @UnitPrice, @GuidingPrice, @TotalStock, @Blocked, @UnitPerPackage" +
                ", @QuantityDiscount, @ConfirmedDeliveryDate, @ProductNumber, @CountyOfOrigin, @PurchasingManager";
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = productArgs.Id.ToString();
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = productArgs.Description;
                command.Parameters.Add("@UnitPrice", System.Data.SqlDbType.Float).Value = productArgs.UnitPrice;
                command.Parameters.Add("@GuidingPrice", System.Data.SqlDbType.Float).Value = productArgs.GuidingPrice;
                command.Parameters.Add("@TotalStock", System.Data.SqlDbType.Int).Value = productArgs.TotalStock;
                command.Parameters.Add("@Blocked", System.Data.SqlDbType.Bit).Value = productArgs.Blocked;
                command.Parameters.Add("@UnitPerPackage", System.Data.SqlDbType.Int).Value = productArgs.UnitPerPackage;
                command.Parameters.Add("@QuantityDiscount", System.Data.SqlDbType.Float).Value = productArgs.QuantityDiscount;
                command.Parameters.Add("@ConfirmedDeliveryDate", System.Data.SqlDbType.DateTime).Value = productArgs.ConfirmedDeliveryDate;
                command.Parameters.Add("@ProductNumber", System.Data.SqlDbType.Int).Value = productArgs.ProductNumber;
                command.Parameters.Add("@CountyOfOrigin", System.Data.SqlDbType.VarChar).Value = productArgs.CountryOfOrigin;
                command.Parameters.Add("@PurchasingManager", System.Data.SqlDbType.VarChar).Value = productArgs.PurchasingManager;

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Update(EventArgs args)
        {
            var productArgs = (args as ProductEventArgs);
            string commandText = "EXEC UpdateProduct @ID, @Description, @UnitPrice, @GuidingPrice, @TotalStock, @Blocked, @UnitPerPackage" +
                ", @QuantityDiscount, @ConfirmedDeliveryDate, @ProductNumber, @CountyOfOrigin, @PurchasingManager";
            using (SqlConnection connection = new SqlConnection(new Connector().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = productArgs.Id.ToString();
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = productArgs.Description;
                command.Parameters.Add("@UnitPrice", System.Data.SqlDbType.Float).Value = productArgs.UnitPrice;
                command.Parameters.Add("@GuidingPrice", System.Data.SqlDbType.Float).Value = productArgs.GuidingPrice;
                command.Parameters.Add("@TotalStock", System.Data.SqlDbType.Int).Value = productArgs.TotalStock;
                command.Parameters.Add("@Blocked", System.Data.SqlDbType.Bit).Value = productArgs.Blocked;
                command.Parameters.Add("@UnitPerPackage", System.Data.SqlDbType.Int).Value = productArgs.UnitPerPackage;
                command.Parameters.Add("@QuantityDiscount", System.Data.SqlDbType.Float).Value = productArgs.QuantityDiscount;
                command.Parameters.Add("@ConfirmedDeliveryDate", System.Data.SqlDbType.DateTime).Value = productArgs.ConfirmedDeliveryDate;
                command.Parameters.Add("@ProductNumber", System.Data.SqlDbType.Int).Value = productArgs.ProductNumber;
                command.Parameters.Add("@CountyOfOrigin", System.Data.SqlDbType.VarChar).Value = productArgs.CountryOfOrigin;
                command.Parameters.Add("@PurchasingManager", System.Data.SqlDbType.VarChar).Value = productArgs.PurchasingManager;

                command.ExecuteNonQuery();
            }
        }
    }
}
