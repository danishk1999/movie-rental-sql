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

        private int employeeId = 2004; // temp hard-coded employee

        public RentalForm()
        {
            InitializeComponent();

            try { ClearCustomerDetails(); } catch { }
            // IMPORTANT: do NOT auto-load customers/movies here
            // We want empty grids until user searches.
        }

        private void ClearCustomerDetails()
        {
            // If you later add labels like lblCustName, etc., reset them here.
            // For now this is just a safe placeholder.
        }

        // ==========================
        // FORM LOAD – start with empty grids
        // ==========================
        private void RentalForm_Load(object sender, EventArgs e)
        {
            gridCustomerResults.DataSource = null;
            gridMovieResults.DataSource = null;
            gridRentalHistory.DataSource = null;

            selectedCustomerId = -1;
            selectedMovieId = -1;
            selectedRentalRecordId = -1;
        }

        // ==========================
        // CUSTOMER SEARCH + SELECT
        // ==========================
        private void SearchCustomers()
        {
            string keyword = txtCustomerSearch.Text.Trim();

            // If no keyword, show nothing
            if (string.IsNullOrEmpty(keyword))
            {
                gridCustomerResults.DataSource = null;
                selectedCustomerId = -1;
                gridRentalHistory.DataSource = null;
                return;
            }

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
                    AND cp.EndTime IS NULL      -- only current phone
                WHERE c.FirstName LIKE @like
                   OR c.LastName  LIKE @like
                   OR c.Email     LIKE @like
                ORDER BY c.FirstName, c.LastName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@like", "%" + keyword + "%"));

            gridCustomerResults.DataSource = dt;

            // Hide internal ID, show phone nicely
            if (gridCustomerResults.Columns.Contains("CustomerID"))
                gridCustomerResults.Columns["CustomerID"].Visible = false;

            if (gridCustomerResults.Columns.Contains("PhoneNum"))
                gridCustomerResults.Columns["PhoneNum"].HeaderText = "Phone";

            selectedCustomerId = -1;
            gridRentalHistory.DataSource = null;
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        // we don’t use live search anymore
        private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            // leave empty – search happens on button click
        }

        private void gridCustomerResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridCustomerResults.DataSource == null)
                return;

            DataGridViewRow row = gridCustomerResults.Rows[e.RowIndex];

            selectedCustomerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
            LoadRentalHistory(selectedCustomerId);
        }

        private void gridCustomerResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gridCustomerResults_CellClick(sender, e);
        }

        // ==========================
        // MOVIE SEARCH + SELECT
        // ==========================
        private void SearchMovies()
        {
            string keyword = txtMovieSearch.Text.Trim();

            // If no keyword, keep movie grid empty
            if (string.IsNullOrEmpty(keyword))
            {
                gridMovieResults.DataSource = null;
                selectedMovieId = -1;
                return;
            }

            string sql = @"
                SELECT 
                    MovieID,
                    MovieName,
                    MovieType,
                    Fee,
                    NumOfCopy
                FROM Movie
                WHERE MovieName LIKE @like
                ORDER BY MovieName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@like", "%" + keyword + "%"));

            gridMovieResults.DataSource = dt;

            if (gridMovieResults.Columns.Contains("MovieID"))
                gridMovieResults.Columns["MovieID"].Visible = false;

            selectedMovieId = -1;
        }

        private void btnMovieSearch_Click(object sender, EventArgs e)
        {
            SearchMovies();
        }

        private void txtMovieSearch_TextChanged(object sender, EventArgs e)
        {
            // leave empty – search happens on button click
        }

        private void gridMovieResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridMovieResults.DataSource == null)
                return;

            DataGridViewRow row = gridMovieResults.Rows[e.RowIndex];
            selectedMovieId = Convert.ToInt32(row.Cells["MovieID"].Value);
        }

        
        // RENTAL HISTORY
        
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

            // hide internal id column but keep it available for Return
            if (gridRentalHistory.Columns.Contains("RentalRecordID"))
                gridRentalHistory.Columns["RentalRecordID"].Visible = false;

            if (dt.Rows.Count == 0)
            {
                // Optional: only show this the first time, but for now:
                MessageBox.Show("No rentals found for this customer.");
            }

            selectedRentalRecordId = -1;
        }

        private void gridRentalHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridRentalHistory.DataSource == null)
                return;

            DataGridViewRow row = gridRentalHistory.Rows[e.RowIndex];
            selectedRentalRecordId = Convert.ToInt32(row.Cells["RentalRecordID"].Value);
        }

        
        // RENT BUTTON
        
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
                // 1) Check stock
                string checkStockSql = "SELECT NumOfCopy FROM Movie WHERE MovieID = @mid";
                DataTable stockDt = DatabaseHelper.ExecuteSelect(
                    checkStockSql,
                    new SqlParameter("@mid", selectedMovieId));

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

                // 2) Insert rental
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
                    // 3) Decrement stock
                    string updateStockSql = "UPDATE Movie SET NumOfCopy = NumOfCopy - 1 WHERE MovieID = @mid";
                    DatabaseHelper.ExecuteNonQuery(
                        updateStockSql,
                        new SqlParameter("@mid", selectedMovieId));

                    MessageBox.Show("Movie rented successfully!");

                    // Refresh history and movie list with current keyword
                    LoadRentalHistory(selectedCustomerId);
                    SearchMovies();
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

        
        // RETURN BUTTON
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            if (selectedRentalRecordId == -1)
            {
                MessageBox.Show("Please select a rental in the history grid first.");
                return;
            }

            try
            {
                // 1) Get movie for this rental and check if already returned
                string getMovieSql = "SELECT MovieID, ReturnTime FROM RentalRecord WHERE RentalRecordID = @rid";
                DataTable dt = DatabaseHelper.ExecuteSelect(
                    getMovieSql,
                    new SqlParameter("@rid", selectedRentalRecordId));

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

                // 2) Update return time
                string returnSql = @"
                    UPDATE RentalRecord
                    SET ReturnTime = GETDATE()
                    WHERE RentalRecordID = @rid;";

                int rows = DatabaseHelper.ExecuteNonQuery(
                    returnSql,
                    new SqlParameter("@rid", selectedRentalRecordId));

                if (rows > 0)
                {
                    // 3) Increment stock
                    string incStockSql = "UPDATE Movie SET NumOfCopy = NumOfCopy + 1 WHERE MovieID = @mid";
                    DatabaseHelper.ExecuteNonQuery(
                        incStockSql,
                        new SqlParameter("@mid", movieId));

                    MessageBox.Show("Movie returned successfully!");

                    // Refresh history and movie list
                    LoadRentalHistory(selectedCustomerId);
                    SearchMovies();
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
    }
}
