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
        private int userId;
        private string role;

        private int lecturerId = -1;

        // Constructor with role-based subject filter event
        public MarksManagementControl(int userId , string role)
        {
            InitializeComponent();
            cmbSubject.SelectedIndexChanged += cmbSubject_SelectedIndexChanged;

            this.userId = userId;
            this.role = role;
        }

        private void MarksManagementControl_Load(object sender, EventArgs e)
        {

            if (role == "Student")
            {
                // If Student hide all controls and show only their marks
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnClear.Visible = false;


                lblStudent.Visible = false;
                cmbStudent.Visible = false;
                lblExam.Visible = false;
                cmbExam.Visible = false;
                lblSubject.Visible = false;
                cmbSubject.Visible = false;
                lblScore.Visible = false;
                txtScore.Visible = false;

                LoadMarksForStudent(userId);
            }

            else if (role == "Lecturer")
            {
                // If Lecturer, loads nly their assigned data

                Lecturer lecturer = LecturerController.GetLecturerByUserId(userId);
                if (lecturer != null)
                {
                    lecturerId = lecturer.LecturerId;
                    LoadSubjects(lecturerId);
                    LoadStudents(lecturerId);
                    LoadMarksForLecturer(lecturerId);
                }
            }


            else
            {
                // If Admin or Staff, loads verything
                LoadStudents();
                LoadExams();
                LoadSubjects();
                LoadMarks();
            }
            
        }

        // Load marks for the currently logged-in student
        private void LoadMarksForStudent(int userId)
        {
            using (var conn = dbConfig.GetConnection())
            {   
                string getStudentIdQuery = "SELECT StudentId FROM Students WHERE UserId = @userId";
                using (var cmd = new SQLiteCommand(getStudentIdQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int studentId = Convert.ToInt32(result);

                        //Get marks for that StudentId
                        string getMarksQuery = @"
                    SELECT m.Score, e.ExamName, s.SubjectName
                    FROM Marks m
                    JOIN Exams e ON m.ExamId = e.ExamId
                    JOIN Subjects s ON e.SubjectId = s.SubjectId
                    WHERE m.StudentId = @studentId";

                        using (var marksCmd = new SQLiteCommand(getMarksQuery, conn))
                        {
                            marksCmd.Parameters.AddWithValue("@studentId", studentId);
                            using (var reader = marksCmd.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                dgvMarks.DataSource = dt;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No student record found for this user.");
                    }
                }
            }
        }
        private void LoadSubjects()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT SubjectId, SubjectName FROM Subjects";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);

                    cmbSubject.DataSource = dt;
                    cmbSubject.DisplayMember = "SubjectName";
                    cmbSubject.ValueMember = "SubjectId";
                }
            }
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
        // Load exams filtered by SubjectId 
        private void LoadExams(int subjectId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT ExamId, ExamName FROM Exams WHERE SubjectId = @SubjectId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
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
        }

        // Loads all marks (Admin/Staff)
        private void LoadMarks()
        {
            dgvMarks.DataSource = null;
            dgvMarks.DataSource = MarkController.GetAllMarks();
        }

        // Loads subjects assigned to the logged-in lecturer
        private void LoadSubjects(int lecturerId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT SubjectId, SubjectName FROM Subjects WHERE LecturerId = @LecturerId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        cmbSubject.DataSource = dt;
                        cmbSubject.DisplayMember = "SubjectName";
                        cmbSubject.ValueMember = "SubjectId";
                    }
                }
            }
        }
        // Loads students based on the lecturer's assigned subjects and courses
        private void LoadStudents(int lecturerId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"
        SELECT DISTINCT s.StudentId, s.FirstName || ' ' || s.LastName AS FullName
        FROM Students s
        JOIN Courses c ON s.CourseId = c.CourseId
        JOIN Subjects subj ON subj.CourseId = c.CourseId
        WHERE subj.LecturerId = @LecturerId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        cmbStudent.DataSource = dt;
                        cmbStudent.DisplayMember = "FullName";
                        cmbStudent.ValueMember = "StudentId";
                    }
                }
            }
        }
        // Load only the marks associated with the lecturer’s subjects
        private void LoadMarksForLecturer(int lecturerId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"
        SELECT m.MarkId, m.Score, m.StudentId, m.ExamId, e.ExamName, s.SubjectName
        FROM Marks m
        JOIN Exams e ON m.ExamId = e.ExamId
        JOIN Subjects s ON e.SubjectId = s.SubjectId
        WHERE s.LecturerId = @LecturerId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvMarks.DataSource = dt;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedIndex == -1 || cmbExam.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtScore.Text))
            {
                MessageBox.Show("Please select student, exam, and enter score.");
                return;
            }

            var mark = new Mark
            {
                StudentId = Convert.ToInt32(cmbStudent.SelectedValue),
                ExamId = Convert.ToInt32(cmbExam.SelectedValue),
                Score = Convert.ToInt32(txtScore.Text.Trim())
            };

            MarkController.AddMark(mark);

            if (role == "Lecturer")
                LoadMarksForLecturer(lecturerId);
            else
                LoadMarks();

            ClearFields();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a mark to update.");
                return;
            }

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
                if (role == "Lecturer")
                    LoadMarksForLecturer(lecturerId);
                else
                    LoadMarks();

                ClearFields();
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count > 0)
            {
                if (dgvMarks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a mark to delete.");
                    return;
                }

                int id = Convert.ToInt32(dgvMarks.SelectedRows[0].Cells["MarkId"].Value);
                MarkController.DeleteMark(id);
                if (role == "Lecturer")
                    LoadMarksForLecturer(lecturerId);
                else
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
            cmbSubject.SelectedIndex = -1;
        }

        private void dgvMarks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && role != "Student")
            {
                DataGridViewRow row = dgvMarks.Rows[e.RowIndex];

                cmbStudent.SelectedValue = Convert.ToInt32(row.Cells["StudentId"].Value);
                cmbExam.SelectedValue = Convert.ToInt32(row.Cells["ExamId"].Value);
                txtScore.Text = row.Cells["Score"].Value.ToString();
            }
        }

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedValue != null && int.TryParse(cmbSubject.SelectedValue.ToString(), out int subjectId))
            {
                LoadExams(subjectId);
               
            }
        }

        private void dgvMarks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbExam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

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
