using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace MovieRentalApp
{
    internal static class DatabaseHelper
    {
        // Read the connection string from App.config
        private static string GetConnectionString()
        {
            return ConfigurationManager
                   .ConnectionStrings["Proj2025F"]
                   .ConnectionString;
        }

        // Basic connection helper used by all forms
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        // Simple test connection method (used by your Test DB button)
        public static void TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT 1", conn))
                {
                    conn.Open();
                    int result = (int)cmd.ExecuteScalar();

                    MessageBox.Show(
                        "Database connection OK! Test query returned: " + result,
                        "Success"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database connection FAILED:\n" + ex.Message,
                    "Error"
                );
            }
        }

        // Generic SELECT helper (used by CustomersForm, RentalForm, Reports, etc.)
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

        // Generic INSERT/UPDATE/DELETE helper
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
