using System.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MovieRentalApp
{
    internal class DatabaseHelper
    {
        // Change 'ABOOD' to your computer name if needed (check SSMS top-left server name)
        private static readonly string connectionString =
            "Server=ABOOD\\MSSQLSERVER01;Database=Proj2025F;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
