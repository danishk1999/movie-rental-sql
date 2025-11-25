using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void monthlyEarn_Click(object sender, EventArgs e)
        {
            MonthlyEarnings monthlyEarningsForm = new MonthlyEarnings();
            monthlyEarningsForm.ShowDialog();

        }
    }
}
