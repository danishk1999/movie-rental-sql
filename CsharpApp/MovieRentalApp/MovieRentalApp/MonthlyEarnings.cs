using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MovieRentalApp
{
    public partial class MonthlyEarnings : Form
    {
        public MonthlyEarnings()
        {
            InitializeComponent();
            LoadMonthYear();
        }

        private void LoadMonthYear()
        {

            // Load years (e.g., last 10 years)
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= currentYear - 10; year--)
            {
                comboBoxYear.Items.Add(year.ToString());
            }
            comboBoxYear.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

         
            int year = int.Parse(comboBoxYear.SelectedItem.ToString());

            ShowReport(year);

        }

        private void ShowReport(int year)
        {
            string query = @"
                WITH Months AS (
                    SELECT 1 AS MonthNumber, 'January' AS MonthName UNION ALL
                    SELECT 2, 'February' UNION ALL
                    SELECT 3, 'March' UNION ALL
                    SELECT 4, 'April' UNION ALL
                    SELECT 5, 'May' UNION ALL
                    SELECT 6, 'June' UNION ALL
                    SELECT 7, 'July' UNION ALL
                    SELECT 8, 'August' UNION ALL
                    SELECT 9, 'September' UNION ALL
                    SELECT 10, 'October' UNION ALL
                    SELECT 11, 'November' UNION ALL
                    SELECT 12, 'December'
                )
                SELECT 
                    MonthName AS [Month],
                    COUNT(R.MovieID) AS NumberOfRentals,
                    ISNULL(SUM(Mo.Fee), 0) AS TotalSales
                FROM Months M
                LEFT JOIN RentalRecord R 
                    ON MONTH(R.CheckoutTime) = M.MonthNumber
                    AND YEAR(R.CheckoutTime) = @Year
                LEFT JOIN Movie Mo 
                    ON R.MovieID = Mo.MovieID
                GROUP BY 
                    M.MonthNumber, M.MonthName
                ORDER BY 
                    M.MonthNumber;

                ";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                query,
                new SqlParameter("@Year", year)
            );

            dataGridView1.DataSource = dt;
        }


    }
}
