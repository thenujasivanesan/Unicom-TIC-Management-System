using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.View
{
    public partial class StaffManagementControl : UserControl
    {
        private int userId;
        private string role;

        public StaffManagementControl(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;
        }

        private void StaffManagementControl_Load(object sender, EventArgs e)
        {
            cmbGender.Items.AddRange(new string[] { "Male", "Female", "Other" });
            LoadUsers();
            LoadStaff();

        }

        private void LoadUsers(int? selectedUserId = null)
        {
            using (var conn = dbConfig.GetConnection())
            {
                // Base query: Select all staff users NOT already assigned to Staff table
                string query = @"
            SELECT UserId, Username FROM Users 
            WHERE Role = 'Staff' 
            AND UserId NOT IN (SELECT UserId FROM Staff)";

                // If we have a selectedUserId (editing), add it back to the list using UNION
                if (selectedUserId.HasValue)
                {
                    query = $@"
                SELECT UserId, Username FROM Users 
                WHERE Role = 'Staff' 
                AND UserId NOT IN (SELECT UserId FROM Staff WHERE UserId != {selectedUserId.Value})
                UNION
                SELECT UserId, Username FROM Users WHERE UserId = {selectedUserId.Value}";
                }

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);

                    cmbUsers.DataSource = dt;
                    cmbUsers.DisplayMember = "Username";
                    cmbUsers.ValueMember = "UserId";

                    // If editing, set selected user, else no selection
                    if (selectedUserId.HasValue)
                    {
                        cmbUsers.SelectedValue = selectedUserId.Value;
                    }
                    else
                    {
                        cmbUsers.SelectedIndex = -1;
                    }
                }
            }
        }
        private void LoadStaff()
        {
            dgvStaff.DataSource = null;
            dgvStaff.DataSource = StaffController.GetAllStaff();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            var staff = new Staff
            {
                UserId = Convert.ToInt32(cmbUsers.SelectedValue),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Gender = cmbGender.Text,
                DateOfBirth = dtpDOB.Value.Date,
                Contact = txtContact.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };

            StaffController.AddStaff(staff);
            LoadUsers();
            LoadStaff();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                var staff = new Staff
                {
                    StaffId = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["StaffId"].Value),
                    UserId = Convert.ToInt32(cmbUsers.SelectedValue),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Gender = cmbGender.Text,
                    DateOfBirth = dtpDOB.Value.Date,
                    Contact = txtContact.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                StaffController.UpdateStaff(staff);
                LoadUsers(staff.UserId);
                
                LoadStaff();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["StaffId"].Value);
                StaffController.DeleteStaff(id);
                LoadUsers();
                LoadStaff();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStaff.Rows[e.RowIndex];

                int selectedUserId = Convert.ToInt32(row.Cells["UserId"].Value);
                LoadUsers(selectedUserId);  // includes the current user so it doesn't disappear
                cmbUsers.SelectedValue = selectedUserId;
                cmbUsers.SelectedValue = Convert.ToInt32(row.Cells["UserId"].Value);
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                cmbGender.Text = row.Cells["Gender"].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                txtContact.Text = row.Cells["Contact"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            cmbUsers.SelectedIndex = -1;
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cmbGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Today;
            txtContact.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
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
