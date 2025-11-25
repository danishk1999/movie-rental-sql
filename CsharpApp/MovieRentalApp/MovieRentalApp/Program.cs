using System;
using System.Windows.Forms;

namespace MovieRentalApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //  Run the MonthlyEarnings form JENNY TO REMOVE
            Application.Run(new  MainForm());
            //Application.Run(new LoginForm());
        }
    }
}