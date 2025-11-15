using System;
using System.Data;
using System.Data.SqlClient;
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
            LoadMovies();
        }

        private void LoadMovies()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"SELECT MovieID, MovieName, MovieType, Fee, NumOfCopy FROM Movie ORDER BY MovieName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvMovies.DataSource = dt;

                    // Cosmetic: ensure columns exist before formatting
                    if (dgvMovies.Columns.Contains("MovieID"))
                        dgvMovies.Columns["MovieID"].ReadOnly = true;
                    if (dgvMovies.Columns.Contains("Fee"))
                        dgvMovies.Columns["Fee"].DefaultCellStyle.Format = "N2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading movies: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new AddEditMovieForm())
            {
                form.Text = "Add Movie";
                // default values can remain
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = DatabaseHelper.GetConnection())
                        {
                            const string insertQuery = @"
                                INSERT INTO Movie (MovieName, MovieType, Fee, NumOfCopy)
                                VALUES (@name, @type, @fee, @copies)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", form.MovieName);
                                cmd.Parameters.AddWithValue("@type", form.MovieType);
                                cmd.Parameters.AddWithValue("@fee", form.Fee);
                                cmd.Parameters.AddWithValue("@copies", form.NumOfCopies);

                                conn.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                        LoadMovies();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding movie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a movie to edit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvMovies.SelectedRows[0];
            int movieID = Convert.ToInt32(row.Cells["MovieID"].Value);
            string currentName = row.Cells["MovieName"].Value?.ToString() ?? "";
            string currentType = row.Cells["MovieType"].Value?.ToString() ?? "";
            decimal currentFee = row.Cells["Fee"].Value == DBNull.Value ? 0m : Convert.ToDecimal(row.Cells["Fee"].Value);
            int currentCopies = row.Cells["NumOfCopy"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["NumOfCopy"].Value);

            using (var form = new AddEditMovieForm())
            {
                form.Text = "Edit Movie";
                form.MovieName = currentName;
                form.MovieType = currentType;
                form.Fee = currentFee;
                form.NumOfCopies = currentCopies;

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = DatabaseHelper.GetConnection())
                        {
                            const string updateQuery = @"
                                UPDATE Movie
                                SET MovieName = @name,
                                    MovieType = @type,
                                    Fee = @fee,
                                    NumOfCopy = @copies
                                WHERE MovieID = @id";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", form.MovieName);
                                cmd.Parameters.AddWithValue("@type", form.MovieType);
                                cmd.Parameters.AddWithValue("@fee", form.Fee);
                                cmd.Parameters.AddWithValue("@copies", form.NumOfCopies);
                                cmd.Parameters.AddWithValue("@id", movieID);

                                conn.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                        LoadMovies();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating movie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMovies.CurrentRow == null)
            {
                MessageBox.Show("Select a movie to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int movieID = Convert.ToInt32(dgvMovies.CurrentRow.Cells["MovieID"].Value);
            string movieName = dgvMovies.CurrentRow.Cells["MovieName"].Value?.ToString() ?? "";

            var answer = MessageBox.Show($"Delete '{movieName}' (ID {movieID})? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    const string deleteQuery = @"DELETE FROM Movie WHERE MovieID = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", movieID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadMovies();
            }
            catch (SqlException sqlex)
            {
                // common cause: FK constraints (ActorAppear, CustomerQueue, RentalRecord)
                MessageBox.Show("Could not delete movie. There may be related records (rentals, actor appearances, queues).\n\n" + sqlex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting movie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


