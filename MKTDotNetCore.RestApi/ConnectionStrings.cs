using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.RestApi
{
    internal class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "LAPTOP-TTIU8JF8",
            InitialCatalog = "TestDB",
            UserID = "sa",
            Password = "Minkhantthu3367",
            TrustServerCertificate = true
        };
    }
}
