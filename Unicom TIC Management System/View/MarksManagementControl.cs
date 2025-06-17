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
    public partial class MarksManagementControl : UserControl
    {
        public MarksManagementControl()
        {
            InitializeComponent();
        }

        private void MarksManagementControl_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadExams();
            LoadMarks();
        }

        private void LoadStudents()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT StudentId, FirstName || ' ' || LastName AS FullName FROM Students";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);

                    cmbStudent.DataSource = dt;
                    cmbStudent.DisplayMember = "FullName";
                    cmbStudent.ValueMember = "StudentId";
                }
            }
        }

        private void LoadExams()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT ExamId, ExamName FROM Exams";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);

                    cmbExam.DataSource = dt;
                    cmbExam.DisplayMember = "ExamName";
                    cmbExam.ValueMember = "ExamId";
                }
            }
        }


        private void LoadMarks()
        {
            dgvMarks.DataSource = null;
            dgvMarks.DataSource = MarkController.GetAllMarks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var mark = new Mark
            {
                StudentId = Convert.ToInt32(cmbStudent.SelectedValue),
                ExamId = Convert.ToInt32(cmbExam.SelectedValue),
                Score = Convert.ToInt32(txtScore.Text.Trim())
            };

            MarkController.AddMark(mark);
            LoadMarks();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count > 0)
            {
                var mark = new Mark
                {
                    MarkId = Convert.ToInt32(dgvMarks.SelectedRows[0].Cells["MarkId"].Value),
                    StudentId = Convert.ToInt32(cmbStudent.SelectedValue),
                    ExamId = Convert.ToInt32(cmbExam.SelectedValue),
                    Score = Convert.ToInt32(txtScore.Text.Trim())
                };

                MarkController.UpdateMark(mark);
                LoadMarks();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvMarks.SelectedRows[0].Cells["MarkId"].Value);
                MarkController.DeleteMark(id);
                LoadMarks();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        private void ClearFields()
        {
            cmbStudent.SelectedIndex = -1;
            cmbExam.SelectedIndex = -1;
            txtScore.Text = "";
        }

        private void dgvMarks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMarks.Rows[e.RowIndex];

                cmbStudent.SelectedValue = Convert.ToInt32(row.Cells["StudentId"].Value);
                cmbExam.SelectedValue = Convert.ToInt32(row.Cells["ExamId"].Value);
                txtScore.Text = row.Cells["Score"].Value.ToString();
            }
        }
    }
}
