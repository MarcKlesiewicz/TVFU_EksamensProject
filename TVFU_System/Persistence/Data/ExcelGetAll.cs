using DomainLayer.Models;
using ExcelDataReader;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class ExcelGetAll : IGetAll<Product>
    {
        private string _filePath;

        public ExcelGetAll()
        {
            using (StreamReader reader = new StreamReader(@"Data\config.ini"))
            {
                _filePath = reader.ReadLine().Split(';')[1];
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            
            using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    while (reader.Read())
                    {
                        products.Add(new Product()
                        {
                            ProductNumber = reader[0].ToString(),
                            Description = reader[1].ToString(),
                            UnitPrice = double.Parse(reader[2].ToString()),
                            GuidingPrice = double.Parse(reader[3].ToString()),
                            TotalStock = double.Parse(reader[4].ToString()),
                            Blocked = reader[5].ToString(),
                            UnitPerPackage = double.Parse(reader[6].ToString()),
                            QuantityDiscount = (reader[7] == null) ? 0 : double.Parse(reader[7].ToString()),
                            PurchasingManager = reader[8].ToString(),
                            ConfirmedDeliveryDate = (reader[9] == null) ? default(DateTime) : DateTime.Parse(reader[9].ToString()),
                            CountryOfOrigin = reader[10].ToString()
                        });
                    }
                }
            }
            return products;
        }
    }
}
