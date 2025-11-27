using System;
using System.Data;
using System.Data.SqlClient;

namespace MovieRentalApp
{
    public partial class RentalForm : Form
    {
        public RentalForm()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //lets keep it empty for now reminder: this is event handles for a text
        }


        private void LoadCustomers()
        {
            try
            {
                string query = @"
                    SELECT CustomerID,
                           FirstName + ' ' + LastName AS FullName
                    FROM Customer
                    ORDER BY FirstName, LastName;";

                DataTable dt = DatabaseHelper.ExecuteSelect(query);

                // Debug message so we can see if it actually got rows
                MessageBox.Show("LoadCustomers: rows returned = " + dt.Rows.Count);

                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "CustomerID";
                cmbCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
        }

        private void LoadMovies()
        {
            try
            {
                string query = @"
            SELECT MovieID, MovieName
            FROM Movie
            ORDER BY MovieName;";

                DataTable dt = DatabaseHelper.ExecuteSelect(query);

                cmbMovie.DataSource = dt;
                cmbMovie.DisplayMember = "MovieName";
                cmbMovie.ValueMember = "MovieID";
                cmbMovie.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading movies: " + ex.Message);
            }
        }

        private void LoadRentalsForCustomer(int customerId)
        {
            try
            {
                string query = @"
            SELECT 
                rr.RentalRecordID,
                m.MovieName,
                rr.CheckoutTime,
                rr.ReturnTime
            FROM RentalRecord rr
            INNER JOIN Movie m ON rr.MovieID = m.MovieID
            WHERE rr.CustomerID = @CustomerID
            ORDER BY rr.CheckoutTime DESC;";

                var param = new SqlParameter("@CustomerID", customerId);

                DataTable dt = DatabaseHelper.ExecuteSelect(query, param);

                gridRentals.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rentals: " + ex.Message);
            }
        }

        private void RentalForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadMovies();
        }

        private void gridRentals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nothing selected – do nothing
            if (cmbCustomer.SelectedIndex < 0)
                return;

            // Sometimes SelectedValue is a DataRowView the first time; handle that
            if (cmbCustomer.SelectedValue is DataRowView drv)
            {
                int customerId = (int)drv["CustomerID"];
                LoadRentalsForCustomer(customerId);
            }
            else if (cmbCustomer.SelectedValue != null)
            {
                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                LoadRentalsForCustomer(customerId);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            // Make sure a customer and movie are selected
            if (cmbCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            if (cmbMovie.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a movie to rent.");
                return;
            }

            // Get selected IDs
            int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            int movieId = Convert.ToInt32(cmbMovie.SelectedValue);

            // TODO: later, get EmployeeID from login form.
            // For now, use 1000 (John Smith from insert script)
            int employeeId = 1000;

            try
            {
                string sql = @"
            INSERT INTO RentalRecord
                (EmployeeID, CustomerID, MovieID, CheckoutTime, ReturnTime, MovieRate)
            VALUES
                (@EmpID, @CustID, @MovieID, GETDATE(), NULL, NULL);";

                var parameters = new[]
                {
            new SqlParameter("@EmpID",  employeeId),
            new SqlParameter("@CustID", customerId),
            new SqlParameter("@MovieID", movieId)
        };

                int rows = DatabaseHelper.ExecuteNonQuery(sql, parameters);

                if (rows > 0)
                {
                    MessageBox.Show("Rental created successfully!");

                    // Refresh the rentals grid for this customer
                    LoadRentalsForCustomer(customerId);
                }
                else
                {
                    MessageBox.Show("No rows inserted – something went wrong.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renting movie: " + ex.Message);
            }
        }

        private int selectedRentalRecordId = -1;

        // saves rental record id when a row is clicked
        private void gridRentals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridRentals.Rows[e.RowIndex];

                selectedRentalRecordId = Convert.ToInt32(row.Cells["RentalRecordID"].Value);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (selectedRentalRecordId == -1)
            {
                MessageBox.Show("Please select a rental to return.");
                return;
            }

            try
            {
                string sql = @"
            UPDATE RentalRecord
            SET ReturnTime = GETDATE()
            WHERE RentalRecordID = @RentalID
              AND ReturnTime IS NULL;  -- prevents double returns
        ";

                var param = new SqlParameter("@RentalID", selectedRentalRecordId);

                int rows = DatabaseHelper.ExecuteNonQuery(sql, param);

                if (rows > 0)
                {
                    MessageBox.Show("Movie returned!");

                    // Refresh rentals for the selected customer
                    if (cmbCustomer.SelectedIndex >= 0)
                    {
                        int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                        LoadRentalsForCustomer(customerId);
                    }

                    selectedRentalRecordId = -1; // reset selection
                }
                else
                {
                    MessageBox.Show("Unable to return this rental. It may already be returned.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error returning movie: " + ex.Message);
            }
        }
    }
}
