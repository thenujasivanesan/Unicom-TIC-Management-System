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
            // Displays the summary infos based on user role

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
                ShowLecturerInfo();
            }
            else if (role == "Staff")
            {
                ShowStaffInfo();
            }
        }

        // ADMIN: Shows counts of total students, lecturers, and staff
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
        // LECTURER: Shows lecturer personal infos from Lecturers table
        private void ShowLecturerInfo()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"SELECT FirstName || ' ' || LastName AS FullName, 
                                Contact, Email, Address
                         FROM Lecturers WHERE UserId = @userId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["FullName"].ToString();
                            string contact = reader["Contact"].ToString();
                            string email = reader["Email"].ToString();
                            string address = reader["Address"].ToString();

                            lblWelcome.Text = $"Welcome, {name}!";
                            lblSummary.Text = $"📞 Contact: {contact}\n📧 Email: {email}\n🏠 Address: {address}";
                        }
                        else
                        {
                            lblWelcome.Text = "Welcome, Lecturer!";
                            lblSummary.Text = "Your profile is not set up yet. \nPlease contact the admin.";
                        }
                    }
                }
            }
        }

        // STAFF: Shows staff personal info from Staff table
        private void ShowStaffInfo()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"
            SELECT FirstName || ' ' || LastName AS FullName,
                   Gender,
                   DateOfBirth,
                   Contact,
                   Email,
                   Address
            FROM Staff
            WHERE UserId = @userId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["FullName"].ToString();
                            string gender = reader["Gender"].ToString();
                            string dob = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                            string contact = reader["Contact"].ToString();
                            string email = reader["Email"].ToString();
                            string address = reader["Address"].ToString();

                            lblWelcome.Text = $"Welcome, {name}!";
                            lblSummary.Text =
                                $"👥 Gender: {gender}\n" +
                                $"🗓️ DOB: {dob}\n" +
                                $"📞 Contact: {contact}\n" +
                                $"📧 Email: {email}\n" +
                                $"🏡 Address: {address}";
                        }
                        else
                        {
                            lblWelcome.Text = "Welcome, Staff!";
                            lblSummary.Text = "Your profile is not set up yet. \nPlease contact the admin.";
                        }
                    }
                }
            }
        }

        //  STUDENT: Shows student personal info and course details
        private void ShowStudentInfo()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"
            SELECT s.FirstName || ' ' || s.LastName AS FullName,
                   s.Gender,
                   s.DateOfBirth,
                   s.Contact,
                   s.Email,
                   s.Address,
                   c.CourseName
            FROM Students s
            JOIN Courses c ON s.CourseId = c.CourseId
            WHERE s.UserId = @userId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["FullName"].ToString();
                            string gender = reader["Gender"].ToString();
                            string dob = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                            string contact = reader["Contact"].ToString();
                            string email = reader["Email"].ToString();
                            string address = reader["Address"].ToString();
                            string course = reader["CourseName"].ToString();

                            lblWelcome.Text = $"Welcome, {name}!";
                            lblSummary.Text =
                                $"👥 Gender: {gender}\n" +
                                $"🗓️ DOB: {dob}\n" +
                                $"📞 Contact: {contact}\n" +
                                $"📧 Email: {email}\n" +
                                $"🏡 Address: {address}\n" +
                                $"📖 Course: {course}";
                        }
                        else
                        {
                            lblWelcome.Text = "Welcome, Student!";
                            lblSummary.Text = "Your profile is not set up yet. \nPlease contact the admin.";
                        }
                    }
                }
            }
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void lblSummary_Click(object sender, EventArgs e)
        {

        }
    }
}
