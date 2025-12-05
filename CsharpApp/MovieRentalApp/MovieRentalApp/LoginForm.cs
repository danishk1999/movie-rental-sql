using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Text = "Employee Login";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Please enter both username and password.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            EmployeeID,
                            EmployeePassword, 
                            FirstName, 
                            LastName
                        FROM Employee
                        WHERE EmployeeUsername = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Username not found.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }

                            int empID = Convert.ToInt32(reader["EmployeeID"]);
                            string dbPassword = reader["EmployeePassword"].ToString();
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();

                            string hashedInput = DatabaseHelper.HashPassword(password);

                            if (!string.Equals(dbPassword, hashedInput))
                            {
                                MessageBox.Show(
                                    "Invalid password. Please try again.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }

                            // 🔥 NEW: Save login session info
                            Session.LoggedInEmployeeID = empID;
                            Session.LoggedInEmployeeName = firstName + " " + lastName;

                            MessageBox.Show(
                                $"Login successful. Welcome, {firstName}!",
                                "Login Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }

                // Go to main form
                this.Hide();
                using (MainForm mainForm = new MainForm())
                {
                    mainForm.ShowDialog();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseHelper.TestConnection();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }
    }
}
