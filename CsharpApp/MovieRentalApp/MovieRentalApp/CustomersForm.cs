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

        // Load data when the form opens
        private void CustomersForm_Load(object sender, EventArgs e)
        {
            this.Text = "Customers Management";
            LoadCustomerData();
        }

        // ✅ Load all customers into the grid
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

        // ✅ Add a new customer
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
                LoadCustomerData(); // refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message);
            }
        }

        // Update button (placeholder for later)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Customer updated successfully!");
            ClearFields();
            LoadCustomerData();
        }

        //  Delete button (placeholder for later)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Customer deleted successfully!");
            ClearFields();
            LoadCustomerData();
        }

        //  Clear input fields
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        //  Helper method to clear textboxes
        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }

        private void gridCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: handle clicks on the grid later (like selecting a row)
        }
    }
}
