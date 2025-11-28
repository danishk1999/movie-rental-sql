using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MovieRentalApp
{
    partial class AddEditMovieForm
    {
        private IContainer components = null;

    private Label lblName;
        private Label lblType;
        private Label lblFee;
        private Label lblCopies;

        private TextBox txtMovieName;
        private Button btnSearchMovie;
        private ComboBox cmbMovieType;
        private NumericUpDown numericFee;
        private NumericUpDown numericCopies;

        private Button btnOK;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            lblName = new Label();
            lblType = new Label();
            lblFee = new Label();
            lblCopies = new Label();

            txtMovieName = new TextBox();
            btnSearchMovie = new Button();
            cmbMovieType = new ComboBox();
            numericFee = new NumericUpDown();
            numericCopies = new NumericUpDown();

            btnOK = new Button();
            btnCancel = new Button();

            ((ISupportInitialize)(numericFee)).BeginInit();
            ((ISupportInitialize)(numericCopies)).BeginInit();

            // Labels
            lblName.Text = "Movie Name:";
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(20, 18);

            lblType.Text = "Movie Type:";
            lblType.AutoSize = true;
            lblType.Location = new System.Drawing.Point(20, 58);

            lblFee.Text = "Fee:";
            lblFee.AutoSize = true;
            lblFee.Location = new System.Drawing.Point(20, 98);

            lblCopies.Text = "Number of Copies:";
            lblCopies.AutoSize = true;
            lblCopies.Location = new System.Drawing.Point(20, 138);

            // Movie Name TextBox
            txtMovieName.Location = new System.Drawing.Point(160, 15);
            txtMovieName.Width = 200;

            // Search Button
            btnSearchMovie.Text = "Search";
            btnSearchMovie.Width = 80;
            btnSearchMovie.Location = new System.Drawing.Point(370, 12);
            btnSearchMovie.Click += new EventHandler(btnSearchMovie_Click);

            // Movie Type ComboBox
            cmbMovieType.Location = new System.Drawing.Point(160, 55);
            cmbMovieType.Width = 160;
            cmbMovieType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMovieType.Items.AddRange(new object[] { "Comedy", "Drama", "Action", "Foreign" });

            // Fee
            numericFee.Location = new System.Drawing.Point(160, 95);
            numericFee.Width = 120;
            numericFee.Maximum = 100000;
            numericFee.DecimalPlaces = 2;
            numericFee.Minimum = 0;

            // Number of Copies
            numericCopies.Location = new System.Drawing.Point(160, 135);
            numericCopies.Width = 120;
            numericCopies.Maximum = 100000;
            numericCopies.Minimum = 0;

            // OK Button
            btnOK.Text = "OK";
            btnOK.Width = 100;
            btnOK.Location = new System.Drawing.Point(120, 185);
            btnOK.Click += new EventHandler(btnOK_Click);

            // Cancel Button
            btnCancel.Text = "Cancel";
            btnCancel.Width = 100;
            btnCancel.Location = new System.Drawing.Point(240, 185);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(460, 230);
            this.Controls.Add(lblName);
            this.Controls.Add(txtMovieName);
            this.Controls.Add(btnSearchMovie);
            this.Controls.Add(lblType);
            this.Controls.Add(cmbMovieType);
            this.Controls.Add(lblFee);
            this.Controls.Add(numericFee);
            this.Controls.Add(lblCopies);
            this.Controls.Add(numericCopies);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
            this.Text = "Add / Edit Movie";

            ((ISupportInitialize)(numericFee)).EndInit();
            ((ISupportInitialize)(numericCopies)).EndInit();
        }
    }

}


