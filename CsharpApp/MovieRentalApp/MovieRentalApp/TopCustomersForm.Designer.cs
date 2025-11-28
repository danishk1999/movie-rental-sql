namespace MovieRentalApp
{
    partial class TopCustomersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.gridTopCustomers = new System.Windows.Forms.DataGridView();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.btnLoadTopCustomers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(261, 59);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(239, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Top Customers in Renting";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(61, 110);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(88, 15);
            this.lblMonth.TabIndex = 1;
            this.lblMonth.Text = "Select Month : ";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(364, 110);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(71, 15);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Select Year :";
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cmbMonth.Location = new System.Drawing.Point(155, 108);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 3;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // gridTopCustomers
            // 
            this.gridTopCustomers.AllowUserToAddRows = false;
            this.gridTopCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopCustomers.Location = new System.Drawing.Point(64, 170);
            this.gridTopCustomers.Name = "gridTopCustomers";
            this.gridTopCustomers.ReadOnly = true;
            this.gridTopCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTopCustomers.Size = new System.Drawing.Size(426, 221);
            this.gridTopCustomers.TabIndex = 5;
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(442, 107);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 7;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged_1);
            // 
            // btnLoadTopCustomers
            // 
            this.btnLoadTopCustomers.Location = new System.Drawing.Point(600, 106);
            this.btnLoadTopCustomers.Name = "btnLoadTopCustomers";
            this.btnLoadTopCustomers.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTopCustomers.TabIndex = 8;
            this.btnLoadTopCustomers.Text = "Load";
            this.btnLoadTopCustomers.UseVisualStyleBackColor = true;
            this.btnLoadTopCustomers.Click += new System.EventHandler(this.btnLoadTopCustomers_Click);
            // 
            // TopCustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoadTopCustomers);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.gridTopCustomers);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblTitle);
            this.Name = "TopCustomersForm";
            this.Text = "TopCustomersForm";
            this.Load += new System.EventHandler(this.TopCustomersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTopCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.DataGridView gridTopCustomers;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Button btnLoadTopCustomers;
    }
}