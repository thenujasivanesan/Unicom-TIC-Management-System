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
    public partial class StudentManagementControl : UserControl
    {
        public StudentManagementControl()
        {
            InitializeComponent();
        }

        private void StudentManagementControl_Load(object sender, EventArgs e)
        {
            cmbGender.Items.AddRange(new string[] { "Male", "Female", "Other" });
            LoadCourses();
            LoadStudents();
            LoadUsers();
        }


        private void LoadUsers(int? selectedUserId = null)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT UserId, Username FROM Users WHERE Role = 'Student'";

                if (selectedUserId.HasValue)
                {
                    query += $" AND (UserId NOT IN (SELECT UserId FROM Students) OR UserId = {selectedUserId.Value})";
                }
                else
                {
                    query += " AND UserId NOT IN (SELECT UserId FROM Students)";
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


        private void LoadCourses()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT CourseId, CourseName FROM Courses";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    cmbCourse.DataSource = dt;
                    cmbCourse.DisplayMember = "CourseName";
                    cmbCourse.ValueMember = "CourseId";
                    cmbCourse.SelectedIndex = -1;
                }
            }
        }

        private void LoadStudents()
        {
            dgvStudents.DataSource = null;
            dgvStudents.DataSource = StudentController.GetAllStudents();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a User for this student.");
                return;
            }

            var student = new Student
            {
                UserId = Convert.ToInt32(cmbUsers.SelectedValue),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Gender = cmbGender.Text,
                DateOfBirth = dtpDOB.Value.Date,
                Contact = txtContact.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                CourseId = Convert.ToInt32(cmbCourse.SelectedValue)
            };

            StudentController.AddStudent(student);
            LoadStudents();
            LoadUsers();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var student = new Student
                {
                    StudentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentId"].Value),
                    UserId = Convert.ToInt32(cmbUsers.SelectedValue),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Gender = cmbGender.Text,
                    DateOfBirth = dtpDOB.Value.Date,
                    Contact = txtContact.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    CourseId = Convert.ToInt32(cmbCourse.SelectedValue)
                };

                StudentController.UpdateStudent(student);
                LoadStudents();
                LoadUsers();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentId"].Value);
                StudentController.DeleteStudent(id);
                LoadStudents();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cmbGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Today;
            txtContact.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            cmbCourse.SelectedIndex = -1;
            cmbUsers.SelectedIndex = -1;

            LoadUsers();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                int selectedUserId = Convert.ToInt32(row.Cells["UserId"].Value);
                LoadUsers(selectedUserId);  // Reload users to include this one for editing



                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                cmbUsers.SelectedValue = Convert.ToInt32(row.Cells["UserId"].Value);
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                cmbGender.Text = row.Cells["Gender"].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                txtContact.Text = row.Cells["Contact"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                cmbCourse.SelectedValue = Convert.ToInt32(row.Cells["CourseId"].Value);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblGender_Click(object sender, EventArgs e)
        {

        }
    }
}
