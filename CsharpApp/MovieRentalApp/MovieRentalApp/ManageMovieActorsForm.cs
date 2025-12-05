using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public class ManageMovieActorsForm : Form
    {
        private int _movieID;
        private string _movieName;

        private Label lblMovie;
        private ComboBox cboAllActors;
        private Button btnAddActorToMovie, btnAddActorFromForm;
        private DataGridView dgvMovieActors;
        private Button btnRemoveActor;

        public ManageMovieActorsForm(int movieID, string movieName)
        {
            _movieID = movieID;
            _movieName = movieName;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Actors";
            this.Size = new Size(650, 450);
            this.StartPosition = FormStartPosition.CenterParent;

            lblMovie = new Label() { Left = 12, Top = 12, Width = 600, Text = $"Movie: {_movieName} (ID {_movieID})" };

            cboAllActors = new ComboBox() { Left = 12, Top = 40, Width = 360, DropDownStyle = ComboBoxStyle.DropDownList };
            btnAddActorToMovie = new Button() { Left = 384, Top = 38, Width = 120, Text = "Add Selected" };
            btnAddActorToMovie.Click += BtnAddActorToMovie_Click;

            btnAddActorFromForm = new Button() { Left = 512, Top = 38, Width = 120, Text = "Add Actor..." };
            btnAddActorFromForm.Click += BtnAddActorFromForm_Click;

            dgvMovieActors = new DataGridView()
            {
                Left = 12,
                Top = 80,
                Width = 620,
                Height = 300,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false
            };

            btnRemoveActor = new Button() { Left = 12, Top = 390, Width = 140, Text = "Remove Selected" };
            btnRemoveActor.Click += BtnRemoveActor_Click;

            this.Controls.Add(lblMovie);
            this.Controls.Add(cboAllActors);
            this.Controls.Add(btnAddActorToMovie);
            this.Controls.Add(btnAddActorFromForm);
            this.Controls.Add(dgvMovieActors);
            this.Controls.Add(btnRemoveActor);

            this.Load += ManageMovieActorsForm_Load;
        }

        private void ManageMovieActorsForm_Load(object sender, EventArgs e)
        {
            LoadAllActors();
            LoadMovieActors();
        }

        private void LoadAllActors()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT ActorID, Name FROM Actor ORDER BY Name";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboAllActors.DisplayMember = "Name";
                    cboAllActors.ValueMember = "ActorID";
                    cboAllActors.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading actors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMovieActors()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                        SELECT A.ActorID, A.Name, A.Gender, 
                               DATEDIFF(year, A.DateOfBrith, GETDATE()) AS Age
                        FROM ActorAppear AA
                        JOIN Actor A ON AA.ActorID = A.ActorID
                        WHERE AA.MovieID = @movieID
                        ORDER BY A.Name";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@movieID", _movieID);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMovieActors.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading movie actors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddActorToMovie_Click(object sender, EventArgs e)
        {
            if (cboAllActors.SelectedValue == null) return;

            int actorID = Convert.ToInt32(cboAllActors.SelectedValue);

            // Check if actor already in movie
            foreach (DataGridViewRow row in dgvMovieActors.Rows)
            {
                if ((int)row.Cells["ActorID"].Value == actorID)
                {
                    MessageBox.Show("Actor is already assigned to this movie.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ActorAppear (MovieID, ActorID) VALUES (@m, @a)", conn))
                {
                    cmd.Parameters.AddWithValue("@m", _movieID);
                    cmd.Parameters.AddWithValue("@a", actorID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadMovieActors();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding actor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddActorFromForm_Click(object sender, EventArgs e)
        {
            using (var form = new AddActorForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int actorID = form.SelectedActorID;

                    // Check if actor already in movie
                    foreach (DataGridViewRow row in dgvMovieActors.Rows)
                    {
                        
                        if (row.IsNewRow) continue;

                        var cellValue = row.Cells["ActorID"].Value;
                        if (cellValue == null || cellValue == DBNull.Value) continue;

                        int existingActorID = Convert.ToInt32(cellValue);
                        if (existingActorID == actorID)
                        {
                            MessageBox.Show("Actor is already assigned to this movie.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }



                    try
                    {
                        using (SqlConnection conn = DatabaseHelper.GetConnection())
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO ActorAppear (MovieID, ActorID) VALUES (@m, @a)", conn))
                        {
                            cmd.Parameters.AddWithValue("@m", _movieID);
                            cmd.Parameters.AddWithValue("@a", actorID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }

                        LoadMovieActors();
                        LoadAllActors(); // refresh the dropdown
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding actor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnRemoveActor_Click(object sender, EventArgs e)
        {
            if (dgvMovieActors.CurrentRow == null) return;

            int actorID = Convert.ToInt32(dgvMovieActors.CurrentRow.Cells["ActorID"].Value);

            var ans = MessageBox.Show("Remove selected actor from movie?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ActorAppear WHERE MovieID=@m AND ActorID=@a", conn))
                {
                    cmd.Parameters.AddWithValue("@m", _movieID);
                    cmd.Parameters.AddWithValue("@a", actorID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadMovieActors();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing actor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
