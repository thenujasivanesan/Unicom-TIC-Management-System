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
            var student = new Student
            {
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
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var student = new Student
                {
                    StudentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentId"].Value),
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
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
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
    }
}
