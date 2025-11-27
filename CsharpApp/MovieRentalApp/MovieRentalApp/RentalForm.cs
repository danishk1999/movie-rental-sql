using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class RentalForm : Form
    {
        private int selectedCustomerId = -1;
        private int selectedMovieId = -1;
        private int selectedRentalRecordId = -1;

        private int employeeId = 2004;

        public RentalForm()
        {
            InitializeComponent();

            try { ClearCustomerDetails(); } catch { }

            SearchCustomers();
            SearchMovies();
        }

        private void ClearCustomerDetails()
        {
            // placeholder if you later add labels
        }

        private void RentalForm_Load(object sender, EventArgs e)
        {
            SearchCustomers();
            SearchMovies();
        }

        // ==========================
        // CUSTOMER SEARCH + SELECT
        // ==========================
        private void SearchCustomers()
        {
            string keyword = txtCustomerSearch.Text.Trim();

            string sql = @"
        SELECT 
            c.CustomerID,
            c.FirstName + ' ' + c.LastName AS FullName,
            c.Email,
            c.CreationDate,
            ISNULL(cp.PhoneNum, '') AS PhoneNum
        FROM Customer c
        LEFT JOIN CustomerPhone cp 
            ON c.CustomerID = cp.CustomerID
            AND cp.EndTime IS NULL             -- only current phone
        WHERE (@key = '')
           OR c.FirstName LIKE @like
           OR c.LastName  LIKE @like
           OR c.Email     LIKE @like
        ORDER BY c.FirstName, c.LastName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@key", keyword),
                new SqlParameter("@like", "%" + keyword + "%"));

            gridCustomerResults.DataSource = dt;

            // Hide internal ID
            if (gridCustomerResults.Columns.Contains("CustomerID"))
                gridCustomerResults.Columns["CustomerID"].Visible = false;

            // Nice header for phone column
            if (gridCustomerResults.Columns.Contains("PhoneNum"))
                gridCustomerResults.Columns["PhoneNum"].HeaderText = "Phone";
        }


        private void gridCustomerResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = gridCustomerResults.Rows[e.RowIndex];
            selectedCustomerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
            LoadRentalHistory(selectedCustomerId);
        }

        private void gridCustomerResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gridCustomerResults_CellClick(sender, e);
        }

        private void txtCustomerSearch_TextChanged(object sender, EventArgs e) { }

        // ==========================
        // MOVIE SEARCH + SELECT
        // ==========================
        private void SearchMovies()
        {
            string keyword = txtMovieSearch.Text.Trim();

            string sql = @"
                SELECT 
                    MovieID,
                    MovieName,
                    MovieType,
                    Fee,
                    NumOfCopy
                FROM Movie
                WHERE (@key = '')
                   OR MovieName LIKE @like
                ORDER BY MovieName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@key", keyword),
                new SqlParameter("@like", "%" + keyword + "%"));

            gridMovieResults.DataSource = dt;
            if (gridMovieResults.Columns.Contains("MovieID"))
                gridMovieResults.Columns["MovieID"].Visible = false;
        }

        private void gridMovieResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = gridMovieResults.Rows[e.RowIndex];
            selectedMovieId = Convert.ToInt32(row.Cells["MovieID"].Value);
        }

        private void txtMovieSearch_TextChanged(object sender, EventArgs e) { }

        // ==========================
        // RENTAL HISTORY
        // ==========================
        private void LoadRentalHistory(int customerId)
        {
            string sql = @"
        SELECT 
            rr.RentalRecordID,
            m.MovieName,
            rr.CheckoutTime,
            rr.ReturnTime,
            rr.MovieRate
        FROM RentalRecord rr
        INNER JOIN Movie m ON rr.MovieID = m.MovieID
        WHERE rr.CustomerID = @cid
        ORDER BY rr.CheckoutTime DESC;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@cid", customerId));

            gridRentalHistory.DataSource = dt;

            // 🔒 keep ID for logic, but hide it from the user
            if (gridRentalHistory.Columns.Contains("RentalRecordID"))
                gridRentalHistory.Columns["RentalRecordID"].Visible = false;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No rentals found for this customer.");
            }
        }

        private void gridRentalHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = gridRentalHistory.Rows[e.RowIndex];
            selectedRentalRecordId = Convert.ToInt32(row.Cells["RentalRecordID"].Value);
        }

        // ==========================
        // RENT BUTTON
        // ==========================
        private void btnRent_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            if (selectedMovieId == -1)
            {
                MessageBox.Show("Please select a movie first.");
                return;
            }

            try
            {
                // Check stock first
                string checkStockSql = "SELECT NumOfCopy FROM Movie WHERE MovieID = @mid";
                DataTable stockDt = DatabaseHelper.ExecuteSelect(checkStockSql, new SqlParameter("@mid", selectedMovieId));

                if (stockDt.Rows.Count == 0)
                {
                    MessageBox.Show("Movie not found in database.");
                    return;
                }

                int stock = Convert.ToInt32(stockDt.Rows[0]["NumOfCopy"]);
                if (stock <= 0)
                {
                    MessageBox.Show("Movie is out of stock!");
                    return;
                }

                // Rent movie
                string rentSql = @"
                    INSERT INTO RentalRecord (EmployeeID, CustomerID, MovieID, CheckoutTime, ReturnTime, MovieRate)
                    VALUES (@EmpID, @CustID, @MovieID, GETDATE(), NULL, NULL);";

                int rows = DatabaseHelper.ExecuteNonQuery(
                    rentSql,
                    new SqlParameter("@EmpID", employeeId),
                    new SqlParameter("@CustID", selectedCustomerId),
                    new SqlParameter("@MovieID", selectedMovieId));

                if (rows > 0)
                {
                    // Decrease stock
                    string updateStockSql = "UPDATE Movie SET NumOfCopy = NumOfCopy - 1 WHERE MovieID = @mid";
                    DatabaseHelper.ExecuteNonQuery(updateStockSql, new SqlParameter("@mid", selectedMovieId));

                    MessageBox.Show("Movie rented successfully!");
                    LoadRentalHistory(selectedCustomerId);
                    SearchMovies(); // refresh stock count in grid
                }
                else
                {
                    MessageBox.Show("Rental failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during rental: " + ex.Message);
            }
        }

        // ==========================
        // RETURN BUTTON
        // ==========================
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (selectedRentalRecordId == -1)
            {
                MessageBox.Show("Please select a rental in the history grid first.");
                return;
            }

            try
            {
                // Get movie ID of this rental
                string getMovieSql = "SELECT MovieID, ReturnTime FROM RentalRecord WHERE RentalRecordID = @rid";
                DataTable dt = DatabaseHelper.ExecuteSelect(getMovieSql, new SqlParameter("@rid", selectedRentalRecordId));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Rental record not found.");
                    return;
                }

                int movieId = Convert.ToInt32(dt.Rows[0]["MovieID"]);
                bool alreadyReturned = dt.Rows[0]["ReturnTime"] != DBNull.Value;

                if (alreadyReturned)
                {
                    MessageBox.Show("This movie has already been returned.");
                    return;
                }

                // Update return time
                string returnSql = @"
                    UPDATE RentalRecord
                    SET ReturnTime = GETDATE()
                    WHERE RentalRecordID = @rid;";

                int rows = DatabaseHelper.ExecuteNonQuery(returnSql, new SqlParameter("@rid", selectedRentalRecordId));

                if (rows > 0)
                {
                    // Increase stock
                    string incStockSql = "UPDATE Movie SET NumOfCopy = NumOfCopy + 1 WHERE MovieID = @mid";
                    DatabaseHelper.ExecuteNonQuery(incStockSql, new SqlParameter("@mid", movieId));

                    MessageBox.Show("Movie returned successfully!");
                    LoadRentalHistory(selectedCustomerId);
                    SearchMovies(); // refresh stock count in grid
                }
                else
                {
                    MessageBox.Show("Unable to update return record.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error returning movie: " + ex.Message);
            }
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        private void btnMovieSearch_Click(object sender, EventArgs e)
        {
            SearchMovies();
        }
    }
}
