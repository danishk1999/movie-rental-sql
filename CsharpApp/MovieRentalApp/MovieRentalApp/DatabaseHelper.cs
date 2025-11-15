using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MovieRentalApp
{
    internal static class DatabaseHelper
    {
        // 1) Reads the connection string from App.config
        private static string GetConnectionString()
        {
            return ConfigurationManager
                   .ConnectionStrings["MovieRentalDb"]
                   .ConnectionString;
        }

        // 2) Old-style helper that other forms expect
        //    (this fixes the 'GetConnection' errors)
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        // 3) Simple test connection method
        public static void TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT 1", conn))
                {
                    conn.Open();
                    int result = (int)cmd.ExecuteScalar();

                    System.Windows.Forms.MessageBox.Show(
                        "Database connection OK! Test query returned: " + result,
                        "Success"
                    );
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Database connection FAILED:\n" + ex.Message,
                    "Error"
                );
            }
        }

        // 4) (Optional but useful later) generic SELECT helper
        public static DataTable ExecuteSelect(string query, params SqlParameter[] parameters)
        {
            var dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        // 5) (Optional but useful later) generic INSERT/UPDATE/DELETE helper
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
