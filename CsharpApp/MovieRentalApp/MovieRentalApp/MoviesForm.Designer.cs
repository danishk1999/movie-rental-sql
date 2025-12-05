namespace MovieRentalApp
{
    partial class MoviesForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvMovies;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnManageActors;

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
            this.dgvMovies = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnManageActors = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvMovies
            // 
            this.dgvMovies.Location = new System.Drawing.Point(20, 20);
            this.dgvMovies.Size = new System.Drawing.Size(1020, 300);
            this.dgvMovies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovies.MultiSelect = false;

            // 
            // btnAdd
            // 
            this.btnAdd.Text = "Add Movie";
            this.btnAdd.Location = new System.Drawing.Point(20, 340);
            this.btnAdd.Size = new System.Drawing.Size(100, 40);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // btnEdit
            // 
            this.btnEdit.Text = "Edit Movie";
            this.btnEdit.Location = new System.Drawing.Point(140, 340);
            this.btnEdit.Size = new System.Drawing.Size(100, 40);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Text = "Delete Movie";
            this.btnDelete.Location = new System.Drawing.Point(260, 340);
            this.btnDelete.Size = new System.Drawing.Size(100, 40);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // btnManageActors
            //
            this.btnManageActors.Text = "Manage Actors";
            this.btnManageActors.Location = new System.Drawing.Point(380, 340);
            this.btnManageActors.Size = new System.Drawing.Size(140, 40);
            this.btnManageActors.Click += new System.EventHandler(this.btnManageActors_Click);

            // 
            // MoviesForm
            // 
            this.ClientSize = new System.Drawing.Size(1060, 400);
            this.Controls.Add(this.dgvMovies);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnManageActors);
            this.Name = "MoviesForm";
            this.Text = "Movies Management";
            this.Load += new System.EventHandler(this.MoviesForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
