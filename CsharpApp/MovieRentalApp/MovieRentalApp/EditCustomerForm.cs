using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class EditCustomerForm : Form
    {
        private int customerID;

        public EditCustomerForm(int custID)
        {
            InitializeComponent();
            customerID = custID;
        }

        private void EditCustomerForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Editing CustomerID: {customerID}";

            LoadCustomerData();
            LoadPhoneNumbers();
        }

        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT FirstName, LastName, Address, City, Province, PostalCode, 
                               Email, AccountNum, CreditCardNum
                        FROM Customer
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFirstName.Text = reader["FirstName"].ToString();

                                txtLastName.Text = reader["LastName"].ToString();

                                txtAddress.Text = reader["Address"].ToString();
                                txtCity.Text = reader["City"].ToString();

                                txtState.Text = reader["Province"].ToString();

                                txtZipCode.Text = reader["PostalCode"].ToString();

                                txtEmail.Text = reader["Email"].ToString();
                                txtAccountNum.Text = reader["AccountNum"].ToString();
                                txtCreditCard.Text = reader["CreditCardNum"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer: " + ex.Message);
            }
        }

        private void LoadPhoneNumbers()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT PhoneNum 
                        FROM CustomerPhone 
                        WHERE CustomerID = @CustomerID AND EndTime IS NULL";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        listPhones.Items.Clear();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listPhones.Items.Add(reader["PhoneNum"].ToString().Trim());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading phones: " + ex.Message);
            }
        }

        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            string phone = txtNewPhone.Text.Trim();

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter a phone number.");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO CustomerPhone (CustomerID, PhoneNum, PhoneType)
                        VALUES (@CustomerID, @PhoneNum, @PhoneType)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);


                        cmd.Parameters.AddWithValue("@PhoneNum", phone.PadRight(10));

                        cmd.Parameters.AddWithValue("@PhoneType", "Mobile");

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Phone added successfully!");
                txtNewPhone.Text = "";
                LoadPhoneNumbers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding phone: " + ex.Message);
            }
        }

        private void btnRemovePhone_Click(object sender, EventArgs e)
        {
            if (listPhones.SelectedItem == null)
            {
                MessageBox.Show("Please select a phone number to remove.");
                return;
            }

            string phone = listPhones.SelectedItem.ToString();

            DialogResult result = MessageBox.Show(
                $"Remove phone {phone}?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        DELETE FROM CustomerPhone 
                        WHERE CustomerID = @CustomerID AND PhoneNum = @PhoneNum";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.Parameters.AddWithValue("@PhoneNum", phone.PadRight(10));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Phone removed successfully!");
                LoadPhoneNumbers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing phone: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string zipCode = txtZipCode.Text.Trim();
            string email = txtEmail.Text.Trim();
            string accountNum = txtAccountNum.Text.Trim();
            string creditCard = txtCreditCard.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("First Name, Last Name, and Email are required.");
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
                            Address = @Address,
                            City = @City,
                            Province = @Province,
                            PostalCode = @PostalCode,
                            Email = @Email,
                            AccountNum = @AccountNum,
                            CreditCardNum = @CreditCardNum
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@City", city);
                        cmd.Parameters.AddWithValue("@Province", state);
                        cmd.Parameters.AddWithValue("@PostalCode", zipCode);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@AccountNum", accountNum);
                        cmd.Parameters.AddWithValue("@CreditCardNum", creditCard);
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Customer updated successfully with CustomerID: {customerID}");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}