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

        private void custTotRentals_Click(object sender, EventArgs e)
        {
            this.Hide();
            TopCustomersForm topForm = new TopCustomersForm();
            topForm.ShowDialog();
            this.Show();
        }

        private void movieRanks_Click(object sender, EventArgs e)
        {
            
            MovieRanks rankingsForm = new MovieRanks();
            rankingsForm.ShowDialog();
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Top3Employees top3EmployeesForm = new Top3Employees();
            top3EmployeesForm.ShowDialog();
        }
    }
}
