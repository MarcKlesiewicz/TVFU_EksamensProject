using System.Data.SqlClient;
using System.IO;

namespace Persistence.Data
{
    public class Connector
    {
        public string ConnectionString { get; }

        public Connector()
        {
            using (StreamReader reader = new StreamReader(@"Data\config.ini"))
            {
                ConnectionString = reader.ReadLine().Split(':')[1];
            }
        }

        public Connector(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
