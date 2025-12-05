namespace MovieRentalApp
{
    partial class rentalform
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMovieSearch = new System.Windows.Forms.TextBox();
            this.gridCustomerResults = new System.Windows.Forms.DataGridView();
            this.gridMovieResults = new System.Windows.Forms.DataGridView();
            this.gridRentalHistory = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRent = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnCustomerSearch = new System.Windows.Forms.Button();
            this.btnMovieSearch = new System.Windows.Forms.Button();
            this.lblRate = new System.Windows.Forms.Label();
            this.numRating = new System.Windows.Forms.NumericUpDown();
            this.btnSaveRating = new System.Windows.Forms.Button();
            this.lblActorRatings = new System.Windows.Forms.Label();
            this.btnSaveActorRatings = new System.Windows.Forms.Button();
            this.dgvActorRatings = new System.Windows.Forms.DataGridView();
            this.colActorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActorAvgRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActorRating = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnAddToQueue = new System.Windows.Forms.Button();
            this.gridCustomerQueue = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRemoveFromQueue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMovieResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRentalHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActorRatings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Customers: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(662, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search Movies:";
            // 
            // txtCustomerSearch
            // 
            this.txtCustomerSearch.Location = new System.Drawing.Point(186, 76);
            this.txtCustomerSearch.Name = "txtCustomerSearch";
            this.txtCustomerSearch.Size = new System.Drawing.Size(252, 20);
            this.txtCustomerSearch.TabIndex = 2;
            this.txtCustomerSearch.TextChanged += new System.EventHandler(this.txtCustomerSearch_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(565, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 33);
            this.label3.TabIndex = 3;
            this.label3.Text = "Rentals";
            // 
            // txtMovieSearch
            // 
            this.txtMovieSearch.Location = new System.Drawing.Point(785, 77);
            this.txtMovieSearch.Name = "txtMovieSearch";
            this.txtMovieSearch.Size = new System.Drawing.Size(270, 20);
            this.txtMovieSearch.TabIndex = 4;
            this.txtMovieSearch.TextChanged += new System.EventHandler(this.txtMovieSearch_TextChanged);
            // 
            // gridCustomerResults
            // 
            this.gridCustomerResults.AllowUserToAddRows = false;
            this.gridCustomerResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomerResults.Location = new System.Drawing.Point(35, 120);
            this.gridCustomerResults.MultiSelect = false;
            this.gridCustomerResults.Name = "gridCustomerResults";
            this.gridCustomerResults.ReadOnly = true;
            this.gridCustomerResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomerResults.Size = new System.Drawing.Size(467, 188);
            this.gridCustomerResults.TabIndex = 5;
            this.gridCustomerResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomerResults_CellClick);
            this.gridCustomerResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomerResults_CellContentClick);
            // 
            // gridMovieResults
            // 
            this.gridMovieResults.AllowUserToAddRows = false;
            this.gridMovieResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMovieResults.Location = new System.Drawing.Point(661, 120);
            this.gridMovieResults.MultiSelect = false;
            this.gridMovieResults.Name = "gridMovieResults";
            this.gridMovieResults.ReadOnly = true;
            this.gridMovieResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMovieResults.Size = new System.Drawing.Size(537, 188);
            this.gridMovieResults.TabIndex = 6;
            this.gridMovieResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMovieResults_CellClick);
            // 
            // gridRentalHistory
            // 
            this.gridRentalHistory.AllowUserToAddRows = false;
            this.gridRentalHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRentalHistory.Location = new System.Drawing.Point(35, 343);
            this.gridRentalHistory.MultiSelect = false;
            this.gridRentalHistory.Name = "gridRentalHistory";
            this.gridRentalHistory.ReadOnly = true;
            this.gridRentalHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRentalHistory.Size = new System.Drawing.Size(467, 150);
            this.gridRentalHistory.TabIndex = 7;
            this.gridRentalHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRentalHistory_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Rental History";
            // 
            // btnRent
            // 
            this.btnRent.Location = new System.Drawing.Point(999, 430);
            this.btnRent.Name = "btnRent";
            this.btnRent.Size = new System.Drawing.Size(75, 23);
            this.btnRent.TabIndex = 9;
            this.btnRent.Text = "Rent";
            this.btnRent.UseVisualStyleBackColor = true;
            this.btnRent.Click += new System.EventHandler(this.btnRent_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(528, 455);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 10;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnCustomerSearch
            // 
            this.btnCustomerSearch.Location = new System.Drawing.Point(467, 77);
            this.btnCustomerSearch.Name = "btnCustomerSearch";
            this.btnCustomerSearch.Size = new System.Drawing.Size(49, 20);
            this.btnCustomerSearch.TabIndex = 11;
            this.btnCustomerSearch.Text = "Search";
            this.btnCustomerSearch.UseVisualStyleBackColor = true;
            this.btnCustomerSearch.Click += new System.EventHandler(this.btnCustomerSearch_Click);
            // 
            // btnMovieSearch
            // 
            this.btnMovieSearch.Location = new System.Drawing.Point(1076, 78);
            this.btnMovieSearch.Name = "btnMovieSearch";
            this.btnMovieSearch.Size = new System.Drawing.Size(64, 20);
            this.btnMovieSearch.TabIndex = 12;
            this.btnMovieSearch.Text = " Search";
            this.btnMovieSearch.UseVisualStyleBackColor = true;
            this.btnMovieSearch.Click += new System.EventHandler(this.btnMovieSearch_Click);
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.Location = new System.Drawing.Point(437, 508);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(119, 15);
            this.lblRate.TabIndex = 13;
            this.lblRate.Text = "Rate this movie (1-5):";
            // 
            // numRating
            // 
            this.numRating.Enabled = false;
            this.numRating.Location = new System.Drawing.Point(482, 540);
            this.numRating.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRating.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRating.Name = "numRating";
            this.numRating.Size = new System.Drawing.Size(34, 20);
            this.numRating.TabIndex = 14;
            this.numRating.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnSaveRating
            // 
            this.btnSaveRating.Enabled = false;
            this.btnSaveRating.Location = new System.Drawing.Point(440, 566);
            this.btnSaveRating.Name = "btnSaveRating";
            this.btnSaveRating.Size = new System.Drawing.Size(116, 31);
            this.btnSaveRating.TabIndex = 15;
            this.btnSaveRating.Text = "Save Movie Rating";
            this.btnSaveRating.UseVisualStyleBackColor = true;
            this.btnSaveRating.Click += new System.EventHandler(this.btnSaveRating_Click);
            // 
            // lblActorRatings
            // 
            this.lblActorRatings.AutoSize = true;
            this.lblActorRatings.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActorRatings.Location = new System.Drawing.Point(32, 512);
            this.lblActorRatings.Name = "lblActorRatings";
            this.lblActorRatings.Size = new System.Drawing.Size(176, 15);
            this.lblActorRatings.TabIndex = 16;
            this.lblActorRatings.Text = "Rate Actors for this Movie (1-5)";
            this.lblActorRatings.Click += new System.EventHandler(this.lblActorRatings_Click);
            // 
            // btnSaveActorRatings
            // 
            this.btnSaveActorRatings.Location = new System.Drawing.Point(227, 508);
            this.btnSaveActorRatings.Name = "btnSaveActorRatings";
            this.btnSaveActorRatings.Size = new System.Drawing.Size(112, 23);
            this.btnSaveActorRatings.TabIndex = 18;
            this.btnSaveActorRatings.Text = "Save Actor Ratings";
            this.btnSaveActorRatings.UseVisualStyleBackColor = true;
            this.btnSaveActorRatings.Click += new System.EventHandler(this.btnSaveActorRatings_Click);
            // 
            // dgvActorRatings
            // 
            this.dgvActorRatings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActorRatings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colActorID,
            this.colActorName,
            this.colActorAvgRate,
            this.colActorRating});
            this.dgvActorRatings.Location = new System.Drawing.Point(35, 540);
            this.dgvActorRatings.Name = "dgvActorRatings";
            this.dgvActorRatings.Size = new System.Drawing.Size(350, 75);
            this.dgvActorRatings.TabIndex = 19;
            // 
            // colActorID
            // 
            this.colActorID.HeaderText = "Actor ID";
            this.colActorID.Name = "colActorID";
            this.colActorID.ReadOnly = true;
            this.colActorID.Visible = false;
            // 
            // colActorName
            // 
            this.colActorName.HeaderText = "Actor";
            this.colActorName.Name = "colActorName";
            this.colActorName.ReadOnly = true;
            // 
            // colActorAvgRate
            // 
            this.colActorAvgRate.HeaderText = "Avg Rating";
            this.colActorAvgRate.Name = "colActorAvgRate";
            this.colActorAvgRate.ReadOnly = true;
            // 
            // colActorRating
            // 
            this.colActorRating.HeaderText = "Your Rating";
            this.colActorRating.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.colActorRating.Name = "colActorRating";
            // 
            // btnAddToQueue
            // 
            this.btnAddToQueue.Location = new System.Drawing.Point(999, 380);
            this.btnAddToQueue.Name = "btnAddToQueue";
            this.btnAddToQueue.Size = new System.Drawing.Size(102, 23);
            this.btnAddToQueue.TabIndex = 20;
            this.btnAddToQueue.Text = "Add to Queue";
            this.btnAddToQueue.UseVisualStyleBackColor = true;
            this.btnAddToQueue.Click += new System.EventHandler(this.btnAddToQueue_Click);
            // 
            // gridCustomerQueue
            // 
            this.gridCustomerQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomerQueue.Location = new System.Drawing.Point(661, 380);
            this.gridCustomerQueue.MultiSelect = false;
            this.gridCustomerQueue.Name = "gridCustomerQueue";
            this.gridCustomerQueue.ReadOnly = true;
            this.gridCustomerQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomerQueue.Size = new System.Drawing.Size(302, 117);
            this.gridCustomerQueue.TabIndex = 21;
            this.gridCustomerQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomerQueue_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(663, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "Customer\'s Queue";
            // 
            // btnRemoveFromQueue
            // 
            this.btnRemoveFromQueue.Location = new System.Drawing.Point(999, 474);
            this.btnRemoveFromQueue.Name = "btnRemoveFromQueue";
            this.btnRemoveFromQueue.Size = new System.Drawing.Size(119, 23);
            this.btnRemoveFromQueue.TabIndex = 23;
            this.btnRemoveFromQueue.Text = "Remove From Queue";
            this.btnRemoveFromQueue.UseVisualStyleBackColor = true;
            this.btnRemoveFromQueue.Click += new System.EventHandler(this.btnRemoveFromQueue_Click);
            // 
            // rentalform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 637);
            this.Controls.Add(this.btnRemoveFromQueue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridCustomerQueue);
            this.Controls.Add(this.btnAddToQueue);
            this.Controls.Add(this.dgvActorRatings);
            this.Controls.Add(this.btnSaveActorRatings);
            this.Controls.Add(this.lblActorRatings);
            this.Controls.Add(this.btnSaveRating);
            this.Controls.Add(this.numRating);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.btnMovieSearch);
            this.Controls.Add(this.btnCustomerSearch);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnRent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gridRentalHistory);
            this.Controls.Add(this.gridMovieResults);
            this.Controls.Add(this.gridCustomerResults);
            this.Controls.Add(this.txtMovieSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCustomerSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "rentalform";
            this.Text = "RentalForm";
            this.Load += new System.EventHandler(this.RentalForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMovieResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRentalHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActorRatings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerQueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMovieSearch;
        private System.Windows.Forms.DataGridView gridCustomerResults;
        private System.Windows.Forms.DataGridView gridMovieResults;
        private System.Windows.Forms.DataGridView gridRentalHistory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRent;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCustomerSearch;
        private System.Windows.Forms.Button btnMovieSearch;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.NumericUpDown numRating;
        private System.Windows.Forms.Button btnSaveRating;
        private System.Windows.Forms.Label lblActorRatings;
        private System.Windows.Forms.Button btnSaveActorRatings;
        private System.Windows.Forms.DataGridView dgvActorRatings;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActorAvgRate;
        private System.Windows.Forms.DataGridViewComboBoxColumn colActorRating;
        private System.Windows.Forms.Button btnAddToQueue;
        private System.Windows.Forms.DataGridView gridCustomerQueue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRemoveFromQueue;
    }
}