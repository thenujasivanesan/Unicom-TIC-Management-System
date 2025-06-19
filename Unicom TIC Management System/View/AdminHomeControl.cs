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
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.View
{
    public partial class AdminHomeControl : UserControl
    {
        private int userId;
        private string role;

        public AdminHomeControl(int userId, string role)
        {
            InitializeComponent();
            this.userId = userId;
            this.role = role;
        }

        private void AdminHomeControl_Load(object sender, EventArgs e)
        {
            lblTotalStudents.Text = "Total Students: 150";
            lblTotalLecturers.Text = "Total Lecturers: 12";
            lblTotalStaff.Text = "Total Staff: 5";

            if (role == "Admin")
            {
                ShowAdminSummary();
            }
            else if (role == "Student")
            {
                ShowStudentInfo();
            }
            else if (role == "Lecturer")
            {
                ShowAdminSummary();
            }
            else if (role == "Staff")
            {
                ShowAdminSummary();
            }
        }

        private void ShowAdminSummary()
        {
            using (var conn = dbConfig.GetConnection())
            {
                

                var cmd1 = new SQLiteCommand("SELECT COUNT(*) FROM Students", conn);
                var totalStudents = Convert.ToInt32(cmd1.ExecuteScalar());

                var cmd2 = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Lecturer'", conn);
                var totalLecturers = Convert.ToInt32(cmd2.ExecuteScalar());

                var cmd3 = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Staff'", conn);
                var totalStaff = Convert.ToInt32(cmd3.ExecuteScalar());

                lblWelcome.Text = $"Welcome, Admin!";
                lblSummary.Text = $"📚 Total Students: {totalStudents}\n👨‍🏫 Lecturers: {totalLecturers}\n👩‍💼 Staff: {totalStaff}";
            }
        }

        private void ShowStudentInfo()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"SELECT FirstName || ' ' || LastName AS FullName, Gender, DateOfBirth
                         FROM Students WHERE UserId = @userId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["FullName"].ToString();
                            string gender = reader["Gender"].ToString();
                            string dob = reader["DateOfBirth"].ToString();
                            

                            lblWelcome.Text = $"Welcome, {name}!";
                            lblSummary.Text = $"Gender: {gender}\nDOB: {dob}";
                        }
                    }
                }
            }
        }


    }
}
