using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MovieRentalApp
{
    public class AddActorForm : Form
    {
        private Label lblActor;
        private ComboBox cboActor;
        private Button btnSave, btnCancel;

        public int SelectedActorID { get; private set; }

        public AddActorForm()
        {
            InitializeComponent();
            LoadActors();
        }

        private void InitializeComponent()
        {
            this.Text = "Select Actor";
            this.Size = new Size(350, 170);
            this.StartPosition = FormStartPosition.CenterParent;

            lblActor = new Label()
            {
                Left = 12,
                Top = 20,
                Width = 80,
                Text = "Actor"
            };

            cboActor = new ComboBox()
            {
                Left = 100,
                Top = 18,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnSave = new Button()
            {
                Left = 60,
                Top = 70,
                Width = 100,
                Text = "Select"
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button()
            {
                Left = 180,
                Top = 70,
                Width = 100,
                Text = "Cancel"
            };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(lblActor);
            this.Controls.Add(cboActor);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void LoadActors()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ActorID, Name FROM Actor ORDER BY Name", conn))
                {
                    conn.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    cboActor.DataSource = dt;
                    cboActor.DisplayMember = "Name";
                    cboActor.ValueMember = "ActorID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading actors: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cboActor.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an actor.");
                return;
            }

            SelectedActorID = Convert.ToInt32(cboActor.SelectedValue);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}


