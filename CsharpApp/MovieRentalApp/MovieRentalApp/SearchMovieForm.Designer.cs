
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MovieRentalApp
{
    partial class SearchMovieForm
    {
        private IContainer components = null;
        private DataGridView dgvMovies;
        private TextBox txtSearch;
        private Label lblSearch;
        private Button btnOK;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvMovies = new DataGridView();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();

            ((ISupportInitialize)(this.dgvMovies)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvMovies
            // 
            this.dgvMovies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovies.Location = new System.Drawing.Point(12, 50);
            this.dgvMovies.Name = "dgvMovies";
            this.dgvMovies.Size = new System.Drawing.Size(600, 300);
            this.dgvMovies.ReadOnly = true;
            this.dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovies.MultiSelect = false;
            this.dgvMovies.TabIndex = 0;

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(80, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(20, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search:";

            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(620, 50);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 30);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Select Movie";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(620, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // 
            // SearchMovieForm
            // 
            this.ClientSize = new System.Drawing.Size(760, 370);
            this.Controls.Add(this.dgvMovies);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "SearchMovieForm";
            this.Text = "Search Movies";

            ((ISupportInitialize)(this.dgvMovies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

