using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

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

                    string sql =
                        @"INSERT INTO Customer (
                            CustomerID, LastName, FirstName, Address, City, Province,
                            PostalCode, Email, AccountNum, CreditCardNum, CreditCardExp,
                            CreditCardCvv, CreationDate)
                          VALUES (
                            NEXT VALUE FOR Customer_CustomerID_Seq, @LastName, @FirstName,
                            'N/A','N/A','AB','A1A1A1', @Email,
                            'ACC123','0000111122223333','1225','123', GETDATE())";

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

        // DELETE CUSTOMER
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer.");
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

                    // Delete children first
                    string deletePhones = "DELETE FROM CustomerPhone WHERE CustomerID = @CustomerID";
                    string deleteQueue = "DELETE FROM CustomerQueue WHERE CustomerID = @CustomerID";
                    string deleteRentals = "DELETE FROM RentalRecord WHERE CustomerID = @CustomerID";

                    // Delete parent last
                    string deleteCustomer = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd1 = new SqlCommand(deletePhones, conn))
                    {
                        cmd1.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd1.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd2 = new SqlCommand(deleteQueue, conn))
                    {
                        cmd2.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd2.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd3 = new SqlCommand(deleteRentals, conn))
                    {
                        cmd3.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd3.ExecuteNonQuery();
                    }

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }
    }
}
