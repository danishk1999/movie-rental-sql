namespace MovieRentalApp
{
    partial class Top3Movies
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
            this.gridTopQueueMovies = new System.Windows.Forms.DataGridView();
            this.btnLoadTopQueueMovies = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopQueueMovies)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(115, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Top 3 Movies In The Queue";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // gridTopQueueMovies
            // 
            this.gridTopQueueMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopQueueMovies.Location = new System.Drawing.Point(149, 113);
            this.gridTopQueueMovies.Name = "gridTopQueueMovies";
            this.gridTopQueueMovies.Size = new System.Drawing.Size(246, 135);
            this.gridTopQueueMovies.TabIndex = 1;
            // 
            // btnLoadTopQueueMovies
            // 
            this.btnLoadTopQueueMovies.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLoadTopQueueMovies.Location = new System.Drawing.Point(177, 285);
            this.btnLoadTopQueueMovies.Name = "btnLoadTopQueueMovies";
            this.btnLoadTopQueueMovies.Size = new System.Drawing.Size(184, 31);
            this.btnLoadTopQueueMovies.TabIndex = 2;
            this.btnLoadTopQueueMovies.Text = "Load Top Movies In Queue";
            this.btnLoadTopQueueMovies.UseVisualStyleBackColor = true;
            this.btnLoadTopQueueMovies.Click += new System.EventHandler(this.btnLoadTopQueueMovies_Click);
            // 
            // Top3Movies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 371);
            this.Controls.Add(this.btnLoadTopQueueMovies);
            this.Controls.Add(this.gridTopQueueMovies);
            this.Controls.Add(this.label1);
            this.Name = "Top3Movies";
            this.Text = "Top3Movies";
            ((System.ComponentModel.ISupportInitialize)(this.gridTopQueueMovies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridTopQueueMovies;
        private System.Windows.Forms.Button btnLoadTopQueueMovies;
    }
}