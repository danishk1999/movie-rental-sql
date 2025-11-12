using System;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class MoviesForm : Form
    {
        public MoviesForm()
        {
            InitializeComponent();
        }

        private void MoviesForm_Load(object sender, EventArgs e)
        {
            this.Text = "Movies Management";
        }
    }
}