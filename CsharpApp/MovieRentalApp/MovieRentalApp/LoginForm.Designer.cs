namespace MovieRentalApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnTestConnection = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // Label Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(115, 20);
            this.lblTitle.Text = "Employee Login";

            // Username textbox
            this.txtUsername.Location = new System.Drawing.Point(140, 80);
            this.txtUsername.Size = new System.Drawing.Size(160, 22);

            // Password textbox
            this.txtPassword.Location = new System.Drawing.Point(140, 120);
            this.txtPassword.Size = new System.Drawing.Size(160, 22);
            this.txtPassword.UseSystemPasswordChar = true;

            // Username label
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(60, 83);
            this.lblUsername.Text = "Username:";

            // Password label
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(60, 123);
            this.lblPassword.Text = "Password:";

            // Login button
            this.btnLogin.Location = new System.Drawing.Point(140, 170);
            this.btnLogin.Size = new System.Drawing.Size(160, 30);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Show password
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Location = new System.Drawing.Point(140, 150);
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);

            // Test DB connection button
            this.btnTestConnection.Location = new System.Drawing.Point(140, 215);
            this.btnTestConnection.Size = new System.Drawing.Size(160, 30);
            this.btnTestConnection.Text = "Test Database";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);

            // Form settings
            this.ClientSize = new System.Drawing.Size(380, 280);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnTestConnection);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoginForm_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnTestConnection;
    }
}
