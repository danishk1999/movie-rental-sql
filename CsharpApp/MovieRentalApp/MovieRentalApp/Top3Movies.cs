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
    public partial class Top3Movies : Form
    {
        public Top3Movies()
        {
            InitializeComponent();
        }

        private void btnLoadTopQueueMovies_Click(object sender, EventArgs e)
        {
            LoadTopQueuedMovies();
        }

        private void LoadTopQueuedMovies()
        {
            string sql = @"
        SELECT TOP 3
            m.MovieID,
            m.MovieName,
            COUNT(*) AS QueueCount
        FROM dbo.CustomerQueue cq
        JOIN dbo.Movie m ON cq.MovieID = m.MovieID
        GROUP BY m.MovieID, m.MovieName
        ORDER BY QueueCount DESC, m.MovieName;";

            DataTable dt = DatabaseHelper.ExecuteSelect(sql);

            gridTopQueueMovies.DataSource = dt;

            if (gridTopQueueMovies.Columns.Contains("MovieID"))
                gridTopQueueMovies.Columns["MovieID"].Visible = false;

            if (gridTopQueueMovies.Columns.Contains("QueueCount"))
                gridTopQueueMovies.Columns["QueueCount"].HeaderText = "Times in Queue";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
