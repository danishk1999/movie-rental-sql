using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public partial class SearchMovieForm : Form
    {
        public SearchMovieForm()
        {
            InitializeComponent();
        }

        public string SelectedMovieName { get; private set; }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"SELECT MovieName FROM Movie WHERE MovieName LIKE @search + '%'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", searchText);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvMovies.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No searches found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching movies: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgvMovies.CurrentRow == null)
            {
                MessageBox.Show("Please select a movie from the list.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedMovieName = dgvMovies.CurrentRow.Cells["MovieName"].Value.ToString();
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



