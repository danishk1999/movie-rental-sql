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

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            this.Text = "Customer Management";
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT CustomerID, FirstName, LastName, Email, CreationDate
                        FROM Customer
                        ORDER BY CustomerID";

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
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please fill First Name, Last Name, Email and Phone.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sqlCustomer = @"
                        INSERT INTO Customer (
                            CustomerID,
                            LastName,
                            FirstName,
                            Address,
                            City,
                            Province,
                            PostalCode,
                            Email,
                            AccountNum,
                            CreditCardNum,
                            CreditCardExp,
                            CreditCardCvv,
                            CreationDate
                        )
                        OUTPUT INSERTED.CustomerID
                        VALUES (
                            NEXT VALUE FOR Customer_CustomerID_Seq,
                            @LastName,
                            @FirstName,
                            'N/A',
                            'N/A',
                            'AB',
                            'A1A1A1',
                            @Email,
                            'ACC123',
                            '0000111122223333',
                            '1225',
                            '123',
                            GETDATE()
                        );";

                    int newCustomerID;
                    using (SqlCommand cmd = new SqlCommand(sqlCustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);

                        newCustomerID = (int)cmd.ExecuteScalar();
                    }

                    string sqlPhone = @"
                        INSERT INTO CustomerPhone (CustomerID, PhoneNum, PhoneType, StartTime, EndTime)
                        VALUES (@CustomerID, @PhoneNum, @PhoneType, GETDATE(), NULL);";

                    using (SqlCommand cmdPhone = new SqlCommand(sqlPhone, conn))
                    {
                        cmdPhone.Parameters.AddWithValue("@CustomerID", newCustomerID);
                        cmdPhone.Parameters.AddWithValue("@PhoneNum", phone.PadRight(10));
                        cmdPhone.Parameters.AddWithValue("@PhoneType", "Mobile");
                        cmdPhone.ExecuteNonQuery();
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
            if (gridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit.");
                return;
            }

            int customerID = Convert.ToInt32(gridCustomers.SelectedRows[0].Cells["CustomerID"].Value);

            EditCustomerForm editForm = new EditCustomerForm(customerID);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomerData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            int customerID = Convert.ToInt32(gridCustomers.SelectedRows[0]
                                             .Cells["CustomerID"].Value);

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

                    string deleteActorRate = @"
                        DELETE FROM ActorRate
                        WHERE RentalRecordID IN (
                            SELECT RentalRecordID
                            FROM RentalRecord
                            WHERE CustomerID = @CustomerID
                        );";

                    using (SqlCommand cmd = new SqlCommand(deleteActorRate, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.ExecuteNonQuery();
                    }

                    string deletePhones = "DELETE FROM CustomerPhone WHERE CustomerID = @CustomerID;";
                    using (SqlCommand cmd = new SqlCommand(deletePhones, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.ExecuteNonQuery();
                    }

                    string deleteQueue = "DELETE FROM CustomerQueue WHERE CustomerID = @CustomerID;";
                    using (SqlCommand cmd = new SqlCommand(deleteQueue, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.ExecuteNonQuery();
                    }

                    string deleteRentals = "DELETE FROM RentalRecord WHERE CustomerID = @CustomerID;";
                    using (SqlCommand cmd = new SqlCommand(deleteRentals, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.ExecuteNonQuery();
                    }

                    string deleteCustomer = "DELETE FROM Customer WHERE CustomerID = @CustomerID;";
                    using (SqlCommand cmd = new SqlCommand(deleteCustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.ExecuteNonQuery();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadCustomerData();
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
                           OR LastName  LIKE @key
                           OR Email     LIKE @key
                        ORDER BY CustomerID;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No customers found.");
                        }

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
            if (e.RowIndex < 0) return;

            DataGridViewRow row = gridCustomers.Rows[e.RowIndex];
            selectedCustomerID = Convert.ToInt32(row.Cells["CustomerID"].Value);

            txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
            txtLastName.Text = row.Cells["LastName"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string queryPhone = @"
                        SELECT TOP 1 PhoneNum
                        FROM CustomerPhone
                        WHERE CustomerID = @CustomerID
                        ORDER BY StartTime DESC;";

                    using (SqlCommand cmdPhone = new SqlCommand(queryPhone, conn))
                    {
                        cmdPhone.Parameters.AddWithValue("@CustomerID", selectedCustomerID);

                        object result = cmdPhone.ExecuteScalar();
                        if (result != null)
                            txtPhone.Text = result.ToString().Trim();
                        else
                            txtPhone.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading phone number: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtSearch.Text = "";
            gridCustomers.ClearSelection();
            selectedCustomerID = -1;
            txtFirstName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadCustomerData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) { }
        private void lblFirstName_Click(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
    }
}