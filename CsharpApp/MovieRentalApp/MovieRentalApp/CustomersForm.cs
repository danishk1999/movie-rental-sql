using System;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            this.Text = "Customers Management";
        }
    }
}