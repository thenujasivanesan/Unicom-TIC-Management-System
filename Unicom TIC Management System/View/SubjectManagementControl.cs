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
    public partial class SubjectManagementControl : UserControl
    {
        public SubjectManagementControl()
        {
            InitializeComponent();
        }

        private void SubjectManagementControl_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadSubjects();

        }


        private void LoadCourses()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT CourseId, CourseName FROM Courses";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    cmbCourses.DataSource = dt;
                    cmbCourses.DisplayMember = "CourseName";
                    cmbCourses.ValueMember = "CourseId";
                }
            }
        }

        private void LoadSubjects()
        {
            dgvSubjects.DataSource = null;
            dgvSubjects.DataSource = SubjectController.GetAllSubjects();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var subject = new Subject
            {
                SubjectName = txtSubjectName.Text.Trim(),
                CourseId = Convert.ToInt32(cmbCourses.SelectedValue)
            };

            SubjectController.AddSubject(subject);
            LoadSubjects();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                var subject = new Subject
                {
                    SubjectId = Convert.ToInt32(dgvSubjects.SelectedRows[0].Cells["SubjectId"].Value),
                    SubjectName = txtSubjectName.Text.Trim(),
                    CourseId = Convert.ToInt32(cmbCourses.SelectedValue)
                };

                SubjectController.UpdateSubject(subject);
                LoadSubjects();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSubjects.SelectedRows[0].Cells["SubjectId"].Value);
                SubjectController.DeleteSubject(id);
                LoadSubjects();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtSubjectName.Text = "";
            cmbCourses.SelectedIndex = -1;
        }

        private void dgvSubjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSubjects.Rows[e.RowIndex];
                txtSubjectName.Text = row.Cells["SubjectName"].Value.ToString();
                cmbCourses.SelectedValue = Convert.ToInt32(row.Cells["CourseId"].Value);
            }
        }

        private void cmbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
