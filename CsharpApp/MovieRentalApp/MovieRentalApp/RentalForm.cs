using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MovieRentalApp
{
    public partial class rentalform : Form
    {
        private int selectedCustomerId = -1;
        private int selectedMovieId = -1;
        private int selectedRentalRecordId = -1;

        private int employeeId = 2004; // temp hard-coded employee

        public rentalform()
        {
            InitializeComponent();

            try { ClearCustomerDetails(); } catch { }
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
            gridCustomerQueue.DataSource = null;

            selectedCustomerId = -1;
            selectedMovieId = -1;
            selectedRentalRecordId = -1;

            // movie rating
            try
            {
                numRating.Enabled = false;
                btnSaveRating.Enabled = false;
            }
            catch { }

            // actor rating
            try
            {
                dgvActorRatings.Rows.Clear();
                dgvActorRatings.Enabled = false;
                btnSaveActorRatings.Enabled = false;
            }
            catch { }
        }

        // ==========================
        // CUSTOMER SEARCH + SELECT
        // ==========================
        private void SearchCustomers()
        {
            string keyword = txtCustomerSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                gridCustomerResults.DataSource = null;
                selectedCustomerId = -1;
                gridRentalHistory.DataSource = null;
                gridCustomerQueue.DataSource = null;
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
                    AND cp.EndTime IS NULL
                WHERE c.FirstName LIKE @like
                   OR c.LastName  LIKE @like
                   OR c.Email     LIKE @like
                ORDER BY c.FirstName, c.LastName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@like", "%" + keyword + "%"));

            gridCustomerResults.DataSource = dt;

            if (gridCustomerResults.Columns.Contains("CustomerID"))
                gridCustomerResults.Columns["CustomerID"].Visible = false;

            if (gridCustomerResults.Columns.Contains("PhoneNum"))
                gridCustomerResults.Columns["PhoneNum"].HeaderText = "Phone";

            selectedCustomerId = -1;
            gridRentalHistory.DataSource = null;
            gridCustomerQueue.DataSource = null;

            // clear actor rating area
            try
            {
                dgvActorRatings.Rows.Clear();
                dgvActorRatings.Enabled = false;
                btnSaveActorRatings.Enabled = false;
            }
            catch { }
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void gridCustomerResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridCustomerResults.DataSource == null)
                return;

            DataGridViewRow row = gridCustomerResults.Rows[e.RowIndex];

            selectedCustomerId = Convert.ToInt32(row.Cells["CustomerID"].Value);

            LoadRentalHistory(selectedCustomerId);
            LoadCustomerQueue(selectedCustomerId);

            numRating.Enabled = false;
            btnSaveRating.Enabled = false;
            selectedRentalRecordId = -1;

            try
            {
                dgvActorRatings.Rows.Clear();
                dgvActorRatings.Enabled = false;
                btnSaveActorRatings.Enabled = false;
            }
            catch { }
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
        }

        private void gridMovieResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridMovieResults.DataSource == null)
                return;

            DataGridViewRow row = gridMovieResults.Rows[e.RowIndex];
            selectedMovieId = Convert.ToInt32(row.Cells["MovieID"].Value);
        }

        // ==========================
        // CUSTOMER QUEUE – LOAD + SELECT
        // ==========================
        private void LoadCustomerQueue(int customerId)
        {
            string sql = @"
SELECT 
    cq.MovieID,
    m.MovieName,
    cq.SortNum,
    m.NumOfCopy
FROM CustomerQueue cq
JOIN Movie m ON cq.MovieID = m.MovieID
WHERE cq.CustomerID = @cid
ORDER BY cq.SortNum;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@cid", customerId));

            gridCustomerQueue.DataSource = dt;

            if (gridCustomerQueue.Columns.Contains("MovieID"))
                gridCustomerQueue.Columns["MovieID"].Visible = false;

            if (gridCustomerQueue.Columns.Contains("MovieName"))
                gridCustomerQueue.Columns["MovieName"].HeaderText = "Queued Movie";

            if (gridCustomerQueue.Columns.Contains("SortNum"))
                gridCustomerQueue.Columns["SortNum"].HeaderText = "Position";

            if (gridCustomerQueue.Columns.Contains("NumOfCopy"))
                gridCustomerQueue.Columns["NumOfCopy"].HeaderText = "Copies Available";
        }

        private void gridCustomerQueue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridCustomerQueue.DataSource == null)
                return;

            DataGridViewRow row = gridCustomerQueue.Rows[e.RowIndex];

            if (row.Cells["MovieID"] != null && row.Cells["MovieID"].Value != DBNull.Value)
            {
                selectedMovieId = Convert.ToInt32(row.Cells["MovieID"].Value);
            }
        }

        // ==========================
        // ADD TO QUEUE BUTTON
        // ==========================
        private void btnAddToQueue_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            if (selectedMovieId == -1)
            {
                MessageBox.Show("Please select a movie to add to the queue (from the movie search list).");
                return;
            }

            try
            {
                string sql = @"
IF NOT EXISTS (
    SELECT 1 
    FROM CustomerQueue 
    WHERE CustomerID = @cid AND MovieID = @mid
)
BEGIN
    INSERT INTO CustomerQueue (CustomerID, MovieID, SortNum)
    SELECT @cid, @mid,
           ISNULL(MAX(SortNum), 0) + 1
    FROM CustomerQueue
    WHERE CustomerID = @cid;
END";

                DatabaseHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@cid", selectedCustomerId),
                    new SqlParameter("@mid", selectedMovieId));

                MessageBox.Show("Movie added to customer's queue.");

                // refresh queue display
                LoadCustomerQueue(selectedCustomerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding movie to queue: " + ex.Message);
            }
        }

        // ==========================
        // REMOVE FROM QUEUE BUTTON
        // ==========================
        private void btnRemoveFromQueue_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            if (selectedMovieId == -1)
            {
                MessageBox.Show(
                    "Please select a movie to remove from the queue.\n" +
                    "Tip: click the movie in the Queue grid.");
                return;
            }

            try
            {
                string sql = @"
DELETE FROM CustomerQueue
WHERE CustomerID = @cid
  AND MovieID    = @mid;";

                int rows = DatabaseHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@cid", selectedCustomerId),
                    new SqlParameter("@mid", selectedMovieId));

                if (rows > 0)
                {
                    MessageBox.Show("Movie removed from customer's queue.");
                }
                else
                {
                    MessageBox.Show("This movie is not currently in the customer's queue.");
                }

                // refresh queue display
                LoadCustomerQueue(selectedCustomerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing movie from queue: " + ex.Message);
            }
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

            if (gridRentalHistory.Columns.Contains("RentalRecordID"))
                gridRentalHistory.Columns["RentalRecordID"].Visible = false;

            selectedRentalRecordId = -1;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No rentals found for this customer.");
            }

            numRating.Enabled = false;
            btnSaveRating.Enabled = false;

            try
            {
                dgvActorRatings.Rows.Clear();
                dgvActorRatings.Enabled = false;
                btnSaveActorRatings.Enabled = false;
            }
            catch { }
        }

        private void gridRentalHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridRentalHistory.DataSource == null)
                return;

            DataGridViewRow row = gridRentalHistory.Rows[e.RowIndex];
            selectedRentalRecordId = Convert.ToInt32(row.Cells["RentalRecordID"].Value);

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
        // RENT BUTTON – process QUEUE ONLY
        // ==========================
        private void btnRent_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            try
            {
                string queueSql = @"
SELECT 
    cq.MovieID,
    cq.SortNum,
    m.MovieName,
    m.NumOfCopy
FROM CustomerQueue cq
JOIN Movie m ON cq.MovieID = m.MovieID
WHERE cq.CustomerID = @cid
ORDER BY cq.SortNum;";

                DataTable queueDt = DatabaseHelper.ExecuteSelect(
                    queueSql,
                    new SqlParameter("@cid", selectedCustomerId));

                if (queueDt.Rows.Count == 0)
                {
                    MessageBox.Show("This customer has no movies in their queue.\nAdd movies to the queue first.");
                    return;
                }

                List<string> rentedMovies = new List<string>();
                List<string> stillQueued = new List<string>();

                foreach (DataRow row in queueDt.Rows)
                {
                    int movieId = Convert.ToInt32(row["MovieID"]);
                    string movieName = row["MovieName"].ToString();
                    int numOfCopy = Convert.ToInt32(row["NumOfCopy"]);

                    if (numOfCopy > 0)
                    {
                        // Insert into RentalRecord
                        string rentSql = @"
INSERT INTO RentalRecord (EmployeeID, CustomerID, MovieID, CheckoutTime, ReturnTime, MovieRate)
VALUES (@EmpID, @CustID, @MovieID, GETDATE(), NULL, NULL);";

                        DatabaseHelper.ExecuteNonQuery(
                            rentSql,
                            new SqlParameter("@EmpID", employeeId),
                            new SqlParameter("@CustID", selectedCustomerId),
                            new SqlParameter("@MovieID", movieId));

                        // Decrease stock
                        string updateStockSql = "UPDATE Movie SET NumOfCopy = NumOfCopy - 1 WHERE MovieID = @mid";
                        DatabaseHelper.ExecuteNonQuery(
                            updateStockSql,
                            new SqlParameter("@mid", movieId));

                        // Remove from queue
                        string deleteQueueSql = @"
DELETE FROM CustomerQueue
WHERE CustomerID = @cid AND MovieID = @mid;";
                        DatabaseHelper.ExecuteNonQuery(
                            deleteQueueSql,
                            new SqlParameter("@cid", selectedCustomerId),
                            new SqlParameter("@mid", movieId));

                        rentedMovies.Add(movieName);
                    }
                    else
                    {
                        // No copy available; leave in queue
                        stillQueued.Add(movieName);
                    }
                }

                // Refresh UI
                LoadRentalHistory(selectedCustomerId);
                LoadCustomerQueue(selectedCustomerId);
                SearchMovies();

                string message = "";
                if (rentedMovies.Count > 0)
                {
                    message += "Rented: " + string.Join(", ", rentedMovies) + ".";
                }
                if (stillQueued.Count > 0)
                {
                    if (message.Length > 0) message += Environment.NewLine;
                    message += "Still queued (no copies available): " + string.Join(", ", stillQueued) + ".";
                }
                if (message.Length == 0)
                {
                    message = "No movies were rented (no copies available for any queued movies).";
                }

                MessageBox.Show(message);
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
                int rentalId = selectedRentalRecordId;

                string getMovieSql = "SELECT MovieID, ReturnTime FROM RentalRecord WHERE RentalRecordID = @rid";
                DataTable dt = DatabaseHelper.ExecuteSelect(
                    getMovieSql,
                    new SqlParameter("@rid", rentalId));

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

                string returnSql = @"
                    UPDATE RentalRecord
                    SET ReturnTime = GETDATE()
                    WHERE RentalRecordID = @rid;";

                int rows = DatabaseHelper.ExecuteNonQuery(
                    returnSql,
                    new SqlParameter("@rid", rentalId));

                if (rows > 0)
                {
                    string incStockSql = "UPDATE Movie SET NumOfCopy = NumOfCopy + 1 WHERE MovieID = @mid";
                    DatabaseHelper.ExecuteNonQuery(
                        incStockSql,
                        new SqlParameter("@mid", movieId));

                    MessageBox.Show("Movie returned successfully!");

                    LoadRentalHistory(selectedCustomerId);

                    // restore rental id for rating
                    selectedRentalRecordId = rentalId;
                    selectedMovieId = movieId;

                    SearchMovies();

                    // enable movie rating
                    numRating.Enabled = true;
                    btnSaveRating.Enabled = true;
                    numRating.Value = 3;

                    // load actors for this movie
                    LoadActorsForMovie(movieId, rentalId);

                    MessageBox.Show("Please rate the movie and/or the actors, then click Save.");
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
        // MOVIE RATING SAVE
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
                    MessageBox.Show("Movie rating saved!");

                    if (selectedCustomerId != -1)
                        LoadRentalHistory(selectedCustomerId);

                    SearchMovies();

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

        // ==========================
        // ACTOR LOADING + RATING
        // ==========================
        private void LoadActorsForMovie(int movieId, int rentalRecordId)
        {
            try
            {
                dgvActorRatings.Rows.Clear();
            }
            catch { }

            string sql = @"
        SELECT 
            a.ActorID,
            a.Name AS ActorName,
            ar.ActorRate,  -- this customer's rating for this rental (may be NULL)
            (
                SELECT AVG(CAST(ar2.ActorRate AS float))
                FROM dbo.ActorRate ar2
                JOIN dbo.RentalRecord rr2 
                    ON rr2.RentalRecordID = ar2.RentalRecordID
                WHERE rr2.MovieID = @mid
                  AND ar2.ActorID = a.ActorID
            ) AS AvgActorRate
        FROM dbo.Actor a
        INNER JOIN dbo.ActorAppear aa
            ON a.ActorID = aa.ActorID
        LEFT JOIN dbo.ActorRate ar
            ON ar.ActorID = a.ActorID
           AND ar.RentalRecordID = @rid
        WHERE aa.MovieID = @mid;";

            DataTable dt = DatabaseHelper.ExecuteSelect(
                sql,
                new SqlParameter("@mid", movieId),
                new SqlParameter("@rid", rentalRecordId));

            if (dt.Rows.Count == 0)
            {
                dgvActorRatings.Enabled = false;
                btnSaveActorRatings.Enabled = false;
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {
                int rowIndex = dgvActorRatings.Rows.Add();
                DataGridViewRow row = dgvActorRatings.Rows[rowIndex];

                row.Cells["colActorID"].Value = dr["ActorID"];
                row.Cells["colActorName"].Value = dr["ActorName"];

                // your rating for THIS rental (if it exists in ActorRate)
                if (dr["ActorRate"] != DBNull.Value)
                {
                    row.Cells["colActorRating"].Value = Convert.ToInt32(dr["ActorRate"]);
                }

                // average rating for this actor in this movie (across all customers)
                if (dr["AvgActorRate"] != DBNull.Value)
                {
                    double avg = Convert.ToDouble(dr["AvgActorRate"]);
                    row.Cells["colActorAvgRate"].Value = Math.Round(avg, 1); // e.g. 4.3
                }
            }

            dgvActorRatings.Enabled = true;
            btnSaveActorRatings.Enabled = true;
        }

        private void btnSaveActorRatings_Click(object sender, EventArgs e)
        {
            if (selectedRentalRecordId == -1)
            {
                MessageBox.Show("Please return a movie first (so we know which rental to attach ratings to).");
                return;
            }

            bool anySaved = false;

            try
            {
                foreach (DataGridViewRow row in dgvActorRatings.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    object actorIdObj = row.Cells["colActorID"].Value;
                    object ratingObj = row.Cells["colActorRating"].Value;

                    if (actorIdObj == null || ratingObj == null || ratingObj == DBNull.Value)
                        continue;

                    int actorId = Convert.ToInt32(actorIdObj);
                    int rating = Convert.ToInt32(ratingObj);

                    if (rating < 1 || rating > 5)
                        continue;

                    string sql = @"
IF EXISTS (SELECT 1 
           FROM dbo.ActorRate 
           WHERE RentalRecordID = @rid AND ActorID = @aid)
BEGIN
    UPDATE dbo.ActorRate
       SET ActorRate = @rate
     WHERE RentalRecordID = @rid AND ActorID = @aid;
END
ELSE
BEGIN
    INSERT INTO dbo.ActorRate (RentalRecordID, ActorID, ActorRate)
    VALUES (@rid, @aid, @rate);
END";

                    DatabaseHelper.ExecuteNonQuery(
                        sql,
                        new SqlParameter("@rid", selectedRentalRecordId),
                        new SqlParameter("@aid", actorId),
                        new SqlParameter("@rate", rating));

                    anySaved = true;
                }

                if (anySaved)
                {
                    MessageBox.Show("Actor ratings saved successfully.");
                }
                else
                {
                    MessageBox.Show("No actor ratings were selected to save.");
                }

                if (selectedMovieId != -1 && selectedRentalRecordId != -1)
                {
                    LoadActorsForMovie(selectedMovieId, selectedRentalRecordId);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving actor ratings: " + ex.Message);
            }
        }

        private void lblActorRatings_Click(object sender, EventArgs e)
        {
        }
    }
}
