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
    public partial class CourseManagementControl : UserControl
    {
        private int userId;
        private string role;

        public CourseManagementControl(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;

        }
        //  Loads ourses from the database into DataGridView
        private void CourseControl_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            dgvCourses.DataSource = null;
            dgvCourses.DataSource = CourseController.GetAllCourses();
        }

        private void ClearFields()
        {
            txtCourseName.Text = "";
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var course = new Course
            {
                CourseName = txtCourseName.Text.Trim(),
                
            };

            CourseController.AddCourse(course);  // sav3e to DB
            LoadCourses();                  // refreshes table
            ClearFields();                  // reset input
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                var course = new Course
                {
                    CourseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CourseId"].Value),
                    CourseName = txtCourseName.Text.Trim(),
                    
                };

                CourseController.UpdateCourse(course);
                LoadCourses();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CourseId"].Value);
                CourseController.DeleteCourse(id);
                LoadCourses();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        // Populatesform fields when a row is clicked
        private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCourses.Rows[e.RowIndex];
                txtCourseName.Text = row.Cells["CourseName"].Value.ToString();
                
            }

        }

        // Back button returns to AdminHomeControl
        private void btnBack_Click(object sender, EventArgs e)
        {
            var parentForm = this.FindForm() as AdminDashboard;
            if (parentForm != null)
            {
                var homeControl = new AdminHomeControl(userId, role); 
                parentForm.LoadControlInPanel(homeControl);
            }

        }
    }
}
