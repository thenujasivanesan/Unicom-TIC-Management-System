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
    public partial class ExamManagementControl : UserControl
    {
        private int userId;
        private string role;

        public ExamManagementControl(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;
        }

        private void ExamManagementControl_Load(object sender, EventArgs e)
        {
            if (role == "Student" || role == "Lecturer")
            {
                // View-only mode: hide editing controls
                btnAddExam.Visible = false;
                btnUpdateExam.Visible = false;
                btnDeleteExam.Visible = false;
                btnClearExam.Visible = false;

                cmbSubject.Enabled = false;
                txtExamName.ReadOnly = true;
            }

            LoadSubjects();
            LoadExams();
        }


        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT SubjectId, SubjectName FROM Subjects";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var subjectList = new List<KeyValuePair<int, string>>();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["SubjectId"]);
                        string name = reader["SubjectName"].ToString();
                        subjectList.Add(new KeyValuePair<int, string>(id, name));
                    }

                    cmbSubject.DataSource = subjectList;
                    cmbSubject.DisplayMember = "Value";
                    cmbSubject.ValueMember = "Key";
                    cmbSubject.SelectedIndex = -1;
                }
            }
        }
            
        private void LoadExams()
        {
            dgvExams.DataSource = null;
            dgvExams.DataSource = ExamController.GetAllExams()
                .Select(e => new
                {
                    ExamId = e.ExamId,
                    ExamName = e.ExamName,
                    Subject = e.SubjectName
                }).ToList();

            dgvExams.ClearSelection();
        }

        private void btnAddExam_Click(object sender, EventArgs e)
        {
            var exam = new Exam
            {
                ExamName = txtExamName.Text.Trim(),
                SubjectId = ((KeyValuePair<int, string>)cmbSubject.SelectedItem).Key
            };

            ExamController.AddExam(exam);
            LoadExams();
            ClearExamFields();
        }

        private void btnUpdateExam_Click(object sender, EventArgs e)
        {
            if (dgvExams.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvExams.SelectedRows[0].Cells["ExamId"].Value);
                var exam = new Exam
                {
                    ExamId = id,
                    ExamName = txtExamName.Text.Trim(),
                    SubjectId = ((KeyValuePair<int, string>)cmbSubject.SelectedItem).Key
                };

                ExamController.UpdateExam(exam);
                LoadExams();
                ClearExamFields();
            }
        }

        private void btnDeleteExam_Click(object sender, EventArgs e)
        {
            if (dgvExams.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvExams.SelectedRows[0].Cells["ExamId"].Value);
                ExamController.DeleteExam(id);
                LoadExams();
                ClearExamFields();
            }
        }

        private void btnClearExam_Click(object sender, EventArgs e)
        {
            ClearExamFields();
        }


        private void ClearExamFields()
        {
            txtExamName.Text = "";
            cmbSubject.SelectedIndex = -1;
            dgvExams.ClearSelection();
        }

        private void dgvExams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvExams.Rows[e.RowIndex];
                txtExamName.Text = row.Cells["ExamName"].Value.ToString();
                cmbSubject.Text = row.Cells["Subject"].Value.ToString();
            }
        }

        private void lblSubject_Click(object sender, EventArgs e)
        {

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
