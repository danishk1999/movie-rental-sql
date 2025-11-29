using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class MovieRanks : Form
    {
        public MovieRanks()
        {
            InitializeComponent();
            LoadMonth();
            LoadYear();
        }

        private void LoadMonth()
        {
            // Load months
            monthComboBox.Items.AddRange(new string[] {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });

            monthComboBox.SelectedIndex = 0;
        }

        private void LoadYear()
        {
            // Load years (e.g., last 10 years)
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= currentYear - 10; year--)
            {
                yearComboBox.Items.Add(year.ToString());
            }
            yearComboBox.SelectedIndex = 0;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            int month = monthComboBox.SelectedIndex + 1;
            int year = int.Parse(yearComboBox.SelectedItem.ToString());
            ShowTopThree(month, year);
        }

        private void ShowTopThree(int month, int year)
        {
            string query = @"
                SELECT TOP 3 
                    M.MovieName as MovieTitle,
                    COUNT(*) AS RentalCount
                FROM RentalRecord R
                JOIN Movie M ON R.MovieID = M.MovieID
                WHERE MONTH(R.CheckoutTime) = @Month 
                    AND YEAR(R.CheckoutTime) = @Year
                GROUP BY M.MovieName
                ORDER BY RentalCount DESC";
            
            DataTable dt = DatabaseHelper.ExecuteSelect(query, new SqlParameter("@Month", month),
                new SqlParameter("@Year", year));
            
            movieRankGridView.DataSource = dt;

            movieRankGridView.Columns["MovieTitle"].HeaderText = "Movie Title";
            movieRankGridView.Columns["RentalCount"].HeaderText = "Number of Rentals";

            movieRankGridView.AutoResizeColumns();

        }
    }
}
