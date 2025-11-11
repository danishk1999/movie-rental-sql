using System;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "1234")
            {
                MessageBox.Show("Login successful!");
                this.Hide();

                var main = new MainForm();
                main.StartPosition = FormStartPosition.CenterScreen;
                main.FormClosed += (s, args) => this.Show();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}