using System.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
//the data bases helper
namespace MovieRentalApp
{
    internal class DatabaseHelper
    {
        
        private static readonly string connectionString =
            "Server=ABOOD\\MSSQLSERVER01;Database=Proj2025F;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
