using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.View
{
    public partial class LecturerManagementControl : UserControl
    {
        private int userId;
        private string role;

        public LecturerManagementControl(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;
        }

        private void LecturerManagementControl_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadLecturers();
        }


        private void LoadUsers(int? selectedUserId = null)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"
            SELECT UserId, Username 
            FROM Users 
            WHERE Role = 'Lecturer' 
            AND UserId NOT IN (SELECT UserId FROM Lecturers)";

                // Allow selected lecturer’s current user to remain in list when editing
                if (selectedUserId.HasValue)
                {
                    query += $" OR UserId = {selectedUserId.Value}";
                }

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);

                    cmbUsers.DataSource = dt;
                    cmbUsers.DisplayMember = "Username";
                    cmbUsers.ValueMember = "UserId";
                    cmbUsers.SelectedIndex = -1;
                }
            }
        }

        private void LoadLecturers()
        {
            dgvLecturers.DataSource = null;
            dgvLecturers.DataSource = LecturerController.GetAllLecturers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a user account.");
                return;
            }

            var lecturer = new Lecturer
            {
                UserId = Convert.ToInt32(cmbUsers.SelectedValue),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };

            LecturerController.AddLecturer(lecturer);
            LoadLecturers();
            ClearFields();
            LoadUsers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLecturers.SelectedRows.Count > 0)
            {
                int lecturerId = Convert.ToInt32(dgvLecturers.SelectedRows[0].Cells["LecturerId"].Value);

                var lecturer = new Lecturer
                {
                    LecturerId = lecturerId,
                    UserId = Convert.ToInt32(cmbUsers.SelectedValue),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Contact = txtContact.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                LecturerController.UpdateLecturer(lecturer);
                LoadLecturers();
                ClearFields();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Please select a lecturer to update.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLecturers.SelectedRows.Count > 0)
            {
                int lecturerId = Convert.ToInt32(dgvLecturers.SelectedRows[0].Cells["LecturerId"].Value);

                var confirm = MessageBox.Show("Are you sure you want to delete this lecturer?",
                                              "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    LecturerController.DeleteLecturer(lecturerId);
                    LoadLecturers();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Please select a lecturer to delete.");
            }

        }

        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            cmbUsers.SelectedIndex = -1;

            LoadUsers();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvLecturers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLecturers.Rows[e.RowIndex];
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                LoadUsers(userId); // Reload with current UserId included

                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtContact.Text = row.Cells["Contact"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                cmbUsers.SelectedValue = Convert.ToInt32(row.Cells["UserId"].Value);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var parentForm = this.FindForm() as AdminDashboard;
            if (parentForm != null)
            {
                var homeControl = new AdminHomeControl(userId, role); // pass the same userId & role
                parentForm.LoadControlInPanel(homeControl);
            }
        }
    }
}
