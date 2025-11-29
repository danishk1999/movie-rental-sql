namespace MovieRentalApp
{
    partial class MovieRanks
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
            this.movieRankGridView = new System.Windows.Forms.DataGridView();
            this.monthComboBox = new System.Windows.Forms.ComboBox();
            this.yearComboBox = new System.Windows.Forms.ComboBox();
            this.monthRank = new System.Windows.Forms.Label();
            this.yearRank = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.movieRankGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // movieRankGridView
            // 
            this.movieRankGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movieRankGridView.Location = new System.Drawing.Point(25, 117);
            this.movieRankGridView.Name = "movieRankGridView";
            this.movieRankGridView.RowHeadersWidth = 51;
            this.movieRankGridView.RowTemplate.Height = 24;
            this.movieRankGridView.Size = new System.Drawing.Size(399, 262);
            this.movieRankGridView.TabIndex = 0;
            // 
            // monthComboBox
            // 
            this.monthComboBox.FormattingEnabled = true;
            this.monthComboBox.Location = new System.Drawing.Point(109, 23);
            this.monthComboBox.Name = "monthComboBox";
            this.monthComboBox.Size = new System.Drawing.Size(108, 24);
            this.monthComboBox.TabIndex = 1;
            // 
            // yearComboBox
            // 
            this.yearComboBox.FormattingEnabled = true;
            this.yearComboBox.Location = new System.Drawing.Point(303, 23);
            this.yearComboBox.Name = "yearComboBox";
            this.yearComboBox.Size = new System.Drawing.Size(121, 24);
            this.yearComboBox.TabIndex = 2;
            // 
            // monthRank
            // 
            this.monthRank.AutoSize = true;
            this.monthRank.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthRank.Location = new System.Drawing.Point(20, 22);
            this.monthRank.Name = "monthRank";
            this.monthRank.Size = new System.Drawing.Size(83, 25);
            this.monthRank.TabIndex = 3;
            this.monthRank.Text = "Month : ";
            // 
            // yearRank
            // 
            this.yearRank.AutoSize = true;
            this.yearRank.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearRank.Location = new System.Drawing.Point(233, 22);
            this.yearRank.Name = "yearRank";
            this.yearRank.Size = new System.Drawing.Size(64, 25);
            this.yearRank.TabIndex = 4;
            this.yearRank.Text = "Year :";
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(181, 64);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(92, 24);
            this.goButton.TabIndex = 5;
            this.goButton.Text = "GO";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // MovieRanks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 427);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.yearRank);
            this.Controls.Add(this.monthRank);
            this.Controls.Add(this.yearComboBox);
            this.Controls.Add(this.monthComboBox);
            this.Controls.Add(this.movieRankGridView);
            this.Name = "MovieRanks";
            this.Text = "MovieRanks";
            ((System.ComponentModel.ISupportInitialize)(this.movieRankGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView movieRankGridView;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.ComboBox yearComboBox;
        private System.Windows.Forms.Label monthRank;
        private System.Windows.Forms.Label yearRank;
        private System.Windows.Forms.Button goButton;
    }
}