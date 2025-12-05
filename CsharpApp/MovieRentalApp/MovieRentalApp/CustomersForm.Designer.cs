using System.Windows.Forms;

namespace MovieRentalApp
{
    partial class CustomersForm
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
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();

            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();

            this.gridCustomers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            this.SuspendLayout();

            // TITLE
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(250, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(196, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Customer Management";

            // First Name
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(40, 65);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(69, 16);
            this.lblFirstName.Text = "First Name:";

            this.txtFirstName.Location = new System.Drawing.Point(115, 62);
            this.txtFirstName.Size = new System.Drawing.Size(130, 22);

            // Last Name
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(270, 65);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(69, 16);
            this.lblLastName.Text = "Last Name:";

            this.txtLastName.Location = new System.Drawing.Point(345, 62);
            this.txtLastName.Size = new System.Drawing.Size(130, 22);

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(500, 65);
            this.lblEmail.Text = "E-mail:";

            this.txtEmail.Location = new System.Drawing.Point(550, 62);
            this.txtEmail.Size = new System.Drawing.Size(150, 22);

            // Phone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(40, 100);
            this.lblPhone.Text = "Phone:";

            this.txtPhone.Location = new System.Drawing.Point(115, 97);
            this.txtPhone.Size = new System.Drawing.Size(130, 22);

            // ADD BUTTON
            this.btnAdd.Location = new System.Drawing.Point(80, 135);
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // UPDATE BUTTON
            this.btnUpdate.Location = new System.Drawing.Point(180, 135);
            this.btnUpdate.Size = new System.Drawing.Size(75, 25);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // DELETE BUTTON — FIXED EVENT NAME
            this.btnDelete.Location = new System.Drawing.Point(280, 135);
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDeleteCustomer_Click);

            // CLEAR BUTTON
            this.btnClear.Location = new System.Drawing.Point(380, 135);
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // SEARCH LABEL
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(480, 140);
            this.lblSearch.Text = "Search:";

            // SEARCH TEXTBOX
            this.txtSearch.Location = new System.Drawing.Point(540, 137);
            this.txtSearch.Size = new System.Drawing.Size(120, 22);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // SEARCH BUTTON
            this.btnSearch.Location = new System.Drawing.Point(670, 135);
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // DATA GRID
            this.gridCustomers.AllowUserToAddRows = false;
            this.gridCustomers.AllowUserToDeleteRows = false;
            this.gridCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCustomers.Location = new System.Drawing.Point(40, 180);
            this.gridCustomers.ReadOnly = true;
            this.gridCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomers.Size = new System.Drawing.Size(705, 210);
            this.gridCustomers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomers_CellClick);

            // FORM SETTINGS
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.gridCustomers);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblTitle);
            this.Text = "Customer Management";
            this.Load += new System.EventHandler(this.CustomersForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblSearch;

        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtSearch;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.DataGridView gridCustomers;
    }
}
