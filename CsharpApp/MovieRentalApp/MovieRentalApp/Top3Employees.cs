using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class Top3Employees : Form
    {
        public Top3Employees()
        {
            InitializeComponent();
            LoadMonth();
            LoadYear();
        }

        // ==========================
        // Load month names into combo
        // ==========================
        private void LoadMonth()
        {
            monthComboBox.Items.Clear();
            monthComboBox.Items.AddRange(new string[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });

            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 12)
                monthComboBox.SelectedIndex = DateTime.Now.Month - 1;
            else
                monthComboBox.SelectedIndex = 0;
        }

        // ==========================
        // Load last 10 years into combo
        // ==========================
        private void LoadYear()
        {
            yearComboBox.Items.Clear();

            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= currentYear - 10; year--)
            {
                yearComboBox.Items.Add(year.ToString());
            }

            yearComboBox.SelectedIndex = 0; // current year
        }

        // ==========================
        // GO button click
        // ==========================
        private void goButton_Click(object sender, EventArgs e)
        {
            if (monthComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a month.");
                return;
            }

            if (yearComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a year.");
                return;
            }

            int month = monthComboBox.SelectedIndex + 1;
            int year = int.Parse(yearComboBox.SelectedItem.ToString());

            ShowTopThree(month, year);
        }

        // ==========================
        // Core logic: load Top 3 employees
        // ==========================
        private void ShowTopThree(int month, int year)
        {
            string query = @"
SELECT TOP 3
    e.EmployeeID,
    (e.FirstName + ' ' + e.LastName) AS EmployeeName,
    COUNT(*) AS TotalRentals
FROM dbo.RentalRecord r
JOIN dbo.Employee e ON r.EmployeeID = e.EmployeeID
WHERE MONTH(r.CheckoutTime) = @Month
  AND YEAR(r.CheckoutTime)  = @Year
GROUP BY e.EmployeeID, e.FirstName, e.LastName
ORDER BY TotalRentals DESC, EmployeeName;";

            try
            {
                DataTable dt = DatabaseHelper.ExecuteSelect(
                    query,
                    new SqlParameter("@Month", month),
                    new SqlParameter("@Year", year));

                topEmployeesDataGridView.DataSource = dt;

                if (topEmployeesDataGridView.Columns.Contains("EmployeeID"))
                    topEmployeesDataGridView.Columns["EmployeeID"].Visible = false;

                if (topEmployeesDataGridView.Columns.Contains("EmployeeName"))
                    topEmployeesDataGridView.Columns["EmployeeName"].HeaderText = "Employee Name";

                if (topEmployeesDataGridView.Columns.Contains("TotalRentals"))
                    topEmployeesDataGridView.Columns["TotalRentals"].HeaderText = "Total Rentals";

                topEmployeesDataGridView.AutoResizeColumns();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No rentals found for the selected month and year.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading top 3 employees: " + ex.Message);
            }
        }

        private void Top3Employees_Load(object sender, EventArgs e)
        {
            // nothing needed here; constructor already calls LoadMonth/LoadYear
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // unused – you can delete this handler if there's no second button
        }
    }
}
