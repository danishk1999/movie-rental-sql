using System;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            CustomersForm customersForm = new CustomersForm();
            customersForm.StartPosition = FormStartPosition.CenterScreen;
            customersForm.ShowDialog();
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            MoviesForm moviesForm = new MoviesForm();
            moviesForm.StartPosition = FormStartPosition.CenterScreen;
            moviesForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new RentalForm())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            using (var form = new Reports())
            {   
                this.Hide();
                form.ShowDialog();
                this.Show();
            }

        }
    }
}