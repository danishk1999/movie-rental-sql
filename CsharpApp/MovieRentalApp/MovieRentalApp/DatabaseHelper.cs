using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace MovieRentalApp
{
    internal static class DatabaseHelper
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager
                   .ConnectionStrings["Proj2025F"]
                   .ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

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

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
