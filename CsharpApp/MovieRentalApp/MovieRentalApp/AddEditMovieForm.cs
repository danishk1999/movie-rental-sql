using System;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class AddEditMovieForm : Form
    {
        public AddEditMovieForm()
        {
            InitializeComponent();
        }

        private void btnSearchMovie_Click(object sender, EventArgs e)
        {
            using (var form = new SearchMovieForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MovieName = form.SelectedMovieName;
                }
            }
        }

        // Public properties used by MoviesForm to read values
        public string MovieName
        {
            get => txtMovieName.Text.Trim();
            set => txtMovieName.Text = value;
        }


        public string MovieType
        {
            get => cmbMovieType.SelectedItem?.ToString() ?? "";
            set
            {
                if (!string.IsNullOrEmpty(value) && cmbMovieType.Items.Contains(value))
                    cmbMovieType.SelectedItem = value;
                else
                    cmbMovieType.SelectedIndex = -1;
            }
        }

        public decimal Fee
        {
            get => numericFee.Value;
            set
            {
                if (value >= numericFee.Minimum && value <= numericFee.Maximum)
                    numericFee.Value = value;
            }
        }

        public int NumOfCopies
        {
            get => (int)numericCopies.Value;
            set
            {
                if (value >= numericCopies.Minimum && value <= numericCopies.Maximum)
                    numericCopies.Value = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // validation
            if (string.IsNullOrWhiteSpace(MovieName))
            {
                MessageBox.Show("Please enter a movie name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMovieName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(MovieType))
            {
                MessageBox.Show("Please select a movie type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMovieType.Focus();
                return;
            }
            if (Fee < 0)
            {
                MessageBox.Show("Fee cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericFee.Focus();
                return;
            }
            if (NumOfCopies < 0)
            {
                MessageBox.Show("Number of copies cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericCopies.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


