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

            // IMPORTANT: we do NOT auto-load customers/movies.
            // Grids start empty until user searches.
        }

        private void ClearCustomerDetails()
        {
            // placeholder for future labels if needed
        }

        // ==========================
        // FORM LOAD – start with empty grids + disabled rating
        // ==========================
        private void RentalForm_Load(object sender, EventArgs e)
        {
            gridCustomerResults.DataSource = null;
            gridMovieResults.DataSource = null;
            gridRentalHistory.DataSource = null;

            selectedCustomerId = -1;
            selectedMovieId = -1;
            selectedRentalRecordId = -1;

            // rating UI disabled until a return happens
            try
            {
                numRating.Enabled = false;
                btnSaveRating.Enabled = false;
            }
            catch { }
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

        private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            // search happens only on button click
        }

        private void gridCustomerResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridCustomerResults.DataSource == null)
                return;

            DataGridViewRow row = gridCustomerResults.Rows[e.RowIndex];

            selectedCustomerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
            LoadRentalHistory(selectedCustomerId);

            // When selecting a different customer, disable rating until a return is done
            numRating.Enabled = false;
            btnSaveRating.Enabled = false;
            selectedRentalRecordId = -1;
        }

        private void gridCustomerResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gridCustomerResults_CellClick(sender, e);
        }

        // ==========================
        // MOVIE SEARCH + SELECT (with average rating)
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
                    m.MovieID,
                    m.MovieName,
                    m.MovieType,
                    m.Fee,
                    m.NumOfCopy,
                    AVG(CAST(rr.MovieRate AS float)) AS AvgRating
                FROM Movie m
                LEFT JOIN RentalRecord rr
                    ON rr.MovieID = m.MovieID
                    AND rr.MovieRate IS NOT NULL
                WHERE m.MovieName LIKE @like
                GROUP BY 
                    m.MovieID,
                    m.MovieName,
                    m.MovieType,
                    m.Fee,
                    m.NumOfCopy
                ORDER BY m.MovieName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@like", "%" + keyword + "%"));

            gridMovieResults.DataSource = dt;

            if (gridMovieResults.Columns.Contains("MovieID"))
                gridMovieResults.Columns["MovieID"].Visible = false;

            if (gridMovieResults.Columns.Contains("AvgRating"))
                gridMovieResults.Columns["AvgRating"].HeaderText = "Avg Rating";

            selectedMovieId = -1;
        }

        private void btnMovieSearch_Click(object sender, EventArgs e)
        {
            SearchMovies();
        }

        private void txtMovieSearch_TextChanged(object sender, EventArgs e)
        {
            // search happens only on button click
        }

        private void gridMovieResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridMovieResults.DataSource == null)
                return;

            DataGridViewRow row = gridMovieResults.Rows[e.RowIndex];
            selectedMovieId = Convert.ToInt32(row.Cells["MovieID"].Value);
        }

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

            // hide internal id column but keep it available for Return
            if (gridRentalHistory.Columns.Contains("RentalRecordID"))
                gridRentalHistory.Columns["RentalRecordID"].Visible = false;

            selectedRentalRecordId = -1;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No rentals found for this customer.");
            }

            // When history reloads, disable rating until a return is done
            numRating.Enabled = false;
            btnSaveRating.Enabled = false;
        }

        private void gridRentalHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridRentalHistory.DataSource == null)
                return;

            DataGridViewRow row = gridRentalHistory.Rows[e.RowIndex];
            selectedRentalRecordId = Convert.ToInt32(row.Cells["RentalRecordID"].Value);

            // We do NOT allow rating on click alone – rating should be triggered after return.
            // But we can still show existing rating in the control (read-only feeling).
            try
            {
                if (row.Cells["MovieRate"] != null && row.Cells["MovieRate"].Value != DBNull.Value)
                {
                    int currentRate = Convert.ToInt32(row.Cells["MovieRate"].Value);

                    if (currentRate >= numRating.Minimum && currentRate <= numRating.Maximum)
                        numRating.Value = currentRate;
                    else
                        numRating.Value = numRating.Minimum;
                }
                else
                {
                    numRating.Value = numRating.Minimum;
                }
            }
            catch
            {
                numRating.Value = numRating.Minimum;
            }
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

        // ==========================
        // RETURN BUTTON
        // ==========================
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

                    // 🔥 NOW enable rating controls for this just-returned rental
                    numRating.Enabled = true;
                    btnSaveRating.Enabled = true;

                    // Default rating to 3 (neutral)
                    numRating.Value = 3;

                    MessageBox.Show("Please rate the movie you just returned (1–5) and click 'Save Rating'.");
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

        // ==========================
        // SAVE RATING – only meaningful after return
        // ==========================
        private void btnSaveRating_Click(object sender, EventArgs e)
        {
            if (selectedRentalRecordId == -1)
            {
                MessageBox.Show("Please select a rental in the history grid first.");
                return;
            }

            int rating = (int)numRating.Value;

            if (rating < 1 || rating > 5)
            {
                MessageBox.Show("Rating must be between 1 and 5.");
                return;
            }

            try
            {
                string sql = @"
                    UPDATE RentalRecord
                    SET MovieRate = @rate
                    WHERE RentalRecordID = @rid;";

                int rows = DatabaseHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@rate", rating),
                    new SqlParameter("@rid", selectedRentalRecordId));

                if (rows > 0)
                {
                    MessageBox.Show("Rating saved!");

                    if (selectedCustomerId != -1)
                        LoadRentalHistory(selectedCustomerId);

                    // Refresh movie grid to update AvgRating
                    SearchMovies();

                    // Optionally disable rating again until next return
                    numRating.Enabled = false;
                    btnSaveRating.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Could not update rating (no rows affected).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving rating: " + ex.Message);
            }
        }
    }
}
