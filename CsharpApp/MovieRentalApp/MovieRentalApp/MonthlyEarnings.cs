using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // Load months
            comboBoxMonth.Items.AddRange(new object[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });
            comboBoxMonth.SelectedIndex = 0;
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
            if (comboBoxMonth.SelectedIndex == -1 || comboBoxYear.SelectedIndex == -1)
            {
                MessageBox.Show("Please select both month and year.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int month = comboBoxMonth.SelectedIndex + 1;
            int year = int.Parse(comboBoxYear.SelectedItem.ToString());

            ShowReport(month, year);

        }

        private void ShowReport(int month, int year)
        {   
            string query = @"
                SELECT COUNT(*) AS NumberOfRentals,
                    SUM(M.Fee) AS TotalSales
                FROM RentalRecord R
                JOIN Movie M ON R.MovieID = M.MovieID
                WHERE YEAR(R.CheckoutTime) = @Year
                  AND MONTH(R.CheckoutTime) = @Month;";

            DataTable dt = DatabaseHelper.ExecuteSelect(query,
                new System.Data.SqlClient.SqlParameter("@Year", year),
                new System.Data.SqlClient.SqlParameter("@Month", month)
            );

            if(dt.Rows.Count > 0 && dt.Rows[0]["NumberOfRentals"] != DBNull.Value)
            {
                int numberOfRentals = Convert.ToInt32(dt.Rows[0]["NumberOfRentals"]);
                decimal totalSales = Convert.ToDecimal(dt.Rows[0]["TotalSales"]);
                MessageBox.Show(
                    $"Number of Rentals: {numberOfRentals}\nTotal Sales: ${totalSales:F2}",
                    "Monthly Earnings Report"
                );
            }
            else
            {
                MessageBox.Show("No rentals found for the selected month and year.", "Monthly Earnings Report");
            }
            

        }


    }
}
