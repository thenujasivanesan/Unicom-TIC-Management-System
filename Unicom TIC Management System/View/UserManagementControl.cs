using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.View
{
    public partial class UserManagementControl : UserControl
    {
        public UserManagementControl()
        {
            InitializeComponent();
        }

        private void UserManagementControl_Load(object sender, EventArgs e)
        {
            cmbRole.Items.AddRange(new string[] { "Admin", "Staff", "Lecturer", "Student" });
            LoadUsers();
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = UserController.GetAllUsers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                Role = cmbRole.Text
            };

            UserController.AddUser(user);
            LoadUsers();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var user = new User
                {
                    UserId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserId"].Value),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    Role = cmbRole.Text
                };

                UserController.UpdateUser(user);
                LoadUsers();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserId"].Value);
                UserController.DeleteUser(id);
                LoadUsers();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = -1;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure the row index is valid
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                cmbRole.Text = row.Cells["Role"].Value.ToString();
            }
        }
    }
}
