using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class CustomersForm : Form
    {
        private int selectedCustomerID = -1;

        public CustomersForm()
        {
            InitializeComponent();
        }

        // Load data when the form opens
        private void CustomersForm_Load(object sender, EventArgs e)
        {
            this.Text = "Customers Management";
            LoadCustomerData();
        }

        //  Load all customers into the grid
        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT CustomerID, FirstName, LastName, Email, CreationDate FROM Customer";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridCustomers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
        }

        //  Add a new customer
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO Customer (
                            CustomerID, LastName, FirstName, Address, City, Province, PostalCode, Email, 
                            AccountNum, CreditCardNum, CreditCardExp, CreditCardCvv, CreationDate
                        ) VALUES (
                            NEXT VALUE FOR Customer_CustomerID_Seq, @LastName, @FirstName, 'N/A', 'N/A', 'AB', 
                            'A1A1A1', @Email, 'ACC123', '0000111122223333', '1225', '123', GETDATE()
                        )";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", name);
                        cmd.Parameters.AddWithValue("@LastName", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Customer added successfully!");
                ClearFields();
                LoadCustomerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Make sure a customer was selected
            if (selectedCustomerID == -1)
            {
                MessageBox.Show("Please select a customer to update.");
                return;
            }

            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim(); 

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Name and Email cannot be empty.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                UPDATE Customer
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email
                WHERE CustomerID = @CustomerID
            ";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", name);
                        cmd.Parameters.AddWithValue("@LastName", name); 
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@CustomerID", selectedCustomerID);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Customer updated successfully!");

                // Refresh grid
                LoadCustomerData();

                // Reset selection
                selectedCustomerID = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            int customerID = Convert.ToInt32(
                gridCustomers.SelectedRows[0].Cells["CustomerID"].Value);

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // 1. Delete ActorRate
                    string deleteActorRate = @"
                        DELETE FROM ActorRate 
                        WHERE RentalRecordID IN (
                            SELECT RentalRecordID 
                            FROM RentalRecord 
                            WHERE CustomerID = @CustomerID
                        )";

                    using (SqlCommand cmd0 = new SqlCommand(deleteActorRate, conn))
                    {
                        cmd0.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd0.ExecuteNonQuery();
                    }

                    // 2. Delete CustomerPhone
                    string deletePhones = "DELETE FROM CustomerPhone WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd1 = new SqlCommand(deletePhones, conn))
                    {
                        cmd1.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd1.ExecuteNonQuery();
                    }

                    // 3. Delete CustomerQueue
                    string deleteQueue = "DELETE FROM CustomerQueue WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd2 = new SqlCommand(deleteQueue, conn))
                    {
                        cmd2.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd2.ExecuteNonQuery();
                    }

                    // 4. Delete RentalRecord
                    string deleteRentals = "DELETE FROM RentalRecord WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd3 = new SqlCommand(deleteRentals, conn))
                    {
                        cmd3.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd3.ExecuteNonQuery();
                    }

                    // 5. Delete Customer
                    string deleteCustomer = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd4 = new SqlCommand(deleteCustomer, conn))
                    {
                        cmd4.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd4.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Customer deleted successfully!");
                LoadCustomerData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message);
            }
        }

        // CLEAR BUTTON 
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

       
        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";

            // Clear any selected row
            gridCustomers.ClearSelection();

          
            txtName.Focus();
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a name or email to search.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                SELECT CustomerID, FirstName, LastName, Email, CreationDate
                FROM Customer
                WHERE FirstName LIKE @key
                   OR LastName LIKE @key
                   OR Email LIKE @key
            ";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gridCustomers.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching customers: " + ex.Message);
            }
        }

        private void gridCustomers_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            if (e.RowIndex >= 0) // make sure user didn't click header
            {
                DataGridViewRow row = gridCustomers.Rows[e.RowIndex];

                txtName.Text = row.Cells["FirstName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = ""; // you don't have phone stored in DB yet

                // Store CustomerID in a hidden place for updating
                selectedCustomerID = Convert.ToInt32(row.Cells["CustomerID"].Value);
            }
        }

    }
}
