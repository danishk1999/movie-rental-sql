using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class TopCustomersForm : Form
    {
        public TopCustomersForm()
        {
            InitializeComponent();
        }

        // ==========================
        // FORM LOAD
        // ==========================
        private void TopCustomersForm_Load(object sender, EventArgs e)
        {
            // ---- Month combo: names ----
            cmbMonth.Items.Clear();
            cmbMonth.Items.AddRange(new object[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;   // current month

            // ---- Year combo: calendar-style range ----
            cmbYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - 10;   // 10 years back
            int endYear = currentYear + 1;    // up to next year

            for (int y = startYear; y <= endYear; y++)
            {
                cmbYear.Items.Add(y);
            }
            cmbYear.SelectedItem = currentYear; // current year

            // Grid initial state
            gridTopCustomers.AutoGenerateColumns = true;
            gridTopCustomers.DataSource = null;
        }

        // ==========================
        // LOAD BUTTON
        // ==========================
        private void btnLoadTopCustomers_Click(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a month.");
                return;
            }

            if (cmbYear.SelectedItem == null)
            {
                MessageBox.Show("Please select a year.");
                return;
            }

            int month = cmbMonth.SelectedIndex + 1;            // 1–12
            int year = Convert.ToInt32(cmbYear.SelectedItem); // selected year

            string sql = @"
WITH RankedCustomers AS (
    SELECT 
        c.CustomerID,
        c.FirstName + ' ' + c.LastName AS FullName,
        COUNT(*) AS TotalRentals,
        MONTH(rr.CheckoutTime) AS [Month],
        YEAR(rr.CheckoutTime)  AS [Year],
        DENSE_RANK() OVER (ORDER BY COUNT(*) DESC) AS [Rank]
    FROM RentalRecord rr
    INNER JOIN Customer c ON rr.CustomerID = c.CustomerID
    WHERE MONTH(rr.CheckoutTime) = @Month
      AND YEAR(rr.CheckoutTime)  = @Year
    GROUP BY c.CustomerID, c.FirstName, c.LastName,
             MONTH(rr.CheckoutTime), YEAR(rr.CheckoutTime)
)
SELECT [Rank], CustomerID, FullName, TotalRentals, [Month], [Year]
FROM RankedCustomers
WHERE [Rank] <= 5
ORDER BY [Rank], FullName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@Month", month),
                new SqlParameter("@Year", year));

            // Show how many rows we got (for debugging / confirmation)
            //MessageBox.Show("Rows returned: " + dt.Rows.Count);

            gridTopCustomers.DataSource = dt;

            if (gridTopCustomers.Columns.Contains("CustomerID"))
                gridTopCustomers.Columns["CustomerID"].Visible = false;
        }


        // ==========================
        // EMPTY HANDLERS FOR DESIGNER
        // (to avoid CS1061 errors)
        // ==========================
        private void label1_Click(object sender, EventArgs e)
        {
            // no-op
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            // optional: you could auto-load when month changes
        }

        private void cmbYear_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // optional: you could auto-load when year changes
        }
    }
}
