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
            this.monthlyEarn.Location = new System.Drawing.Point(109, 22);
            this.monthlyEarn.Name = "monthlyEarn";
            this.monthlyEarn.Size = new System.Drawing.Size(305, 67);
            this.monthlyEarn.TabIndex = 0;
            this.monthlyEarn.Text = "Monthly Earnings";
            this.monthlyEarn.UseVisualStyleBackColor = true;
            this.monthlyEarn.Click += new System.EventHandler(this.monthlyEarn_Click);
            // 
            // movieRanks
            // 
            this.movieRanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.movieRanks.Location = new System.Drawing.Point(109, 95);
            this.movieRanks.Name = "movieRanks";
            this.movieRanks.Size = new System.Drawing.Size(305, 67);
            this.movieRanks.TabIndex = 1;
            this.movieRanks.Text = "Movie Ranks";
            this.movieRanks.UseVisualStyleBackColor = true;
            // 
            // custTotRentals
            // 
            this.custTotRentals.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custTotRentals.Location = new System.Drawing.Point(109, 168);
            this.custTotRentals.Name = "custTotRentals";
            this.custTotRentals.Size = new System.Drawing.Size(305, 67);
            this.custTotRentals.TabIndex = 2;
            this.custTotRentals.Text = "Customers and Their Total Rentals";
            this.custTotRentals.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(109, 241);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(305, 67);
            this.button4.TabIndex = 3;
            this.button4.Text = "Movies in Queue";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(109, 314);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(305, 67);
            this.button5.TabIndex = 4;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 414);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.custTotRentals);
            this.Controls.Add(this.movieRanks);
            this.Controls.Add(this.monthlyEarn);
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