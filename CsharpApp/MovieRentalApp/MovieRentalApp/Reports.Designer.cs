namespace MovieRentalApp
{
    partial class Reports
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
            this.monthlyEarn = new System.Windows.Forms.Button();
            this.movieRanks = new System.Windows.Forms.Button();
            this.custTotRentals = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monthlyEarn
            // 
            this.monthlyEarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthlyEarn.Location = new System.Drawing.Point(82, 18);
            this.monthlyEarn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.monthlyEarn.Name = "monthlyEarn";
            this.monthlyEarn.Size = new System.Drawing.Size(229, 54);
            this.monthlyEarn.TabIndex = 0;
            this.monthlyEarn.Text = "Monthly Earnings";
            this.monthlyEarn.UseVisualStyleBackColor = true;
            this.monthlyEarn.Click += new System.EventHandler(this.monthlyEarn_Click);
            // 
            // movieRanks
            // 
            this.movieRanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.movieRanks.Location = new System.Drawing.Point(82, 77);
            this.movieRanks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.movieRanks.Name = "movieRanks";
            this.movieRanks.Size = new System.Drawing.Size(229, 54);
            this.movieRanks.TabIndex = 1;
            this.movieRanks.Text = "Top 3 Movie Ranks";
            this.movieRanks.UseVisualStyleBackColor = true;
            this.movieRanks.Click += new System.EventHandler(this.movieRanks_Click);
            // 
            // custTotRentals
            // 
            this.custTotRentals.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custTotRentals.Location = new System.Drawing.Point(82, 136);
            this.custTotRentals.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.custTotRentals.Name = "custTotRentals";
            this.custTotRentals.Size = new System.Drawing.Size(229, 54);
            this.custTotRentals.TabIndex = 2;
            this.custTotRentals.Text = "Top  5 Customers in Renting";
            this.custTotRentals.UseVisualStyleBackColor = true;
            this.custTotRentals.Click += new System.EventHandler(this.custTotRentals_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(82, 196);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(229, 54);
            this.button4.TabIndex = 3;
            this.button4.Text = "Top 3 Movies in Queue";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(82, 255);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(229, 54);
            this.button5.TabIndex = 4;
            this.button5.Text = "Top 3 Employees";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 336);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.custTotRentals);
            this.Controls.Add(this.movieRanks);
            this.Controls.Add(this.monthlyEarn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Reports";
            this.Text = "Reports";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button monthlyEarn;
        private System.Windows.Forms.Button movieRanks;
        private System.Windows.Forms.Button custTotRentals;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}