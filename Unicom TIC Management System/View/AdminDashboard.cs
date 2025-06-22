using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Unicom_TIC_Management_System.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Unicom_TIC_Management_System.View
{
    public partial class AdminDashboard : Form
    {
        private int userId;      // Stores current logged-in user ID
        private string role;       // Stores current logged in user role


        public AdminDashboard(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;

            ApplyRoleRestrictions();
        }

        // this method hides or shows buttons based on the user's role
        private void ApplyRoleRestrictions()
        {
            if (role == "Student")
            {
                // Students can view marks and timetable
                btnManageUsers.Visible = false;
                btnManageCourses.Visible = false;
                btnManageSubjects.Visible = false;
                btnManageStudents.Visible = false;
                btnManageRooms.Visible = false;
                btnManageExams.Visible = false;
                btnManageMarks.Visible = true;  // View only
                btnManageTimetable.Visible = true;    // View only
                btnManageLecturers.Visible = false;
                btnManageStaff.Visible = false;
            }

            
            else if (role == "Lecturer")
            {
                // lect5urers can manage their subject marks and  view timetable
                btnManageUsers.Visible = false;
                btnManageCourses.Visible = false;
                btnManageSubjects.Visible = false;
                btnManageStudents.Visible = false;
                btnManageRooms.Visible = false;
                btnManageExams.Visible = false;
                btnManageMarks.Visible = true;  
                btnManageTimetable.Visible = true;
                btnManageLecturers.Visible = false;
                btnManageStaff.Visible = false;
            }

            else if (role == "Staff")
            {
               // staff can manage exams, marks and timetable
                btnManageUsers.Visible = false;
                btnManageCourses.Visible = false;
                btnManageSubjects.Visible = false;
                btnManageStudents.Visible = false;
                btnManageRooms.Visible = false;
                btnManageExams.Visible = true;
                btnManageMarks.Visible = true;
                btnManageTimetable.Visible = true;
                btnManageLecturers.Visible=false;
                btnManageStaff.Visible=false;
            }
            else
            {
                // Admin can manage everything
                btnManageUsers.Visible = true;
                btnManageCourses.Visible = true;
                btnManageSubjects.Visible = true;
                btnManageStudents.Visible = true;
                btnManageRooms.Visible = true;
                btnManageExams.Visible = true;
                btnManageMarks.Visible = true;
                btnManageTimetable.Visible = true;
                btnManageLecturers.Visible = true;
                btnManageStaff.Visible = true;
            }

        }

        private void panelMainContent_Paint(object sender, PaintEventArgs e)
        {

        }


        public void LoadControlInPanel(UserControl control)
        {
            panelMainContent.Controls.Clear();        // Removes old content
            control.Dock = DockStyle.Fill;            // Makes it fill the panel
            panelMainContent.Controls.Add(control);   // Shows it
            control.BringToFront();                   // Brings to front
        }


        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadControlInPanel(new AdminHomeControl(userId, role));
        }

        private void LoadUserControl(UserControl control)
        {
            panelMainContent.Controls.Clear();          // Removes old content
            control.Dock = DockStyle.Fill;       // Fills the panel
            panelMainContent.Controls.Add(control);     // Adds new content
        }

        // button click handl3ers to load appropriate management controls
        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UserManagementControl(userId, role));
        }

        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            LoadUserControl(new CourseManagementControl(userId, role));
        }

        private void btnManageSubjects_Click(object sender, EventArgs e)
        {
            LoadUserControl(new SubjectManagementControl(userId, role));
        }

        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            LoadUserControl(new StudentManagementControl(userId, role));

        }

        private void btnManageExams_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ExamManagementControl(userId, role));

        }

        private void btnManageMarks_Click(object sender, EventArgs e)
        {
            LoadUserControl(new MarksManagementControl(userId, role));
        }

        private void btnManageRooms_Click(object sender, EventArgs e)
        {
            LoadUserControl(new RoomsManagementControl(userId, role));
        }

        private void btnManageTimetable_Click(object sender, EventArgs e)
        {
            LoadUserControl(new TimetableManagementControl(userId, role));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Shows the Login Form again
            Login loginForm = new Login();
            loginForm.Show();

            this.Close();
        }

        private void btnManageLecturers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new LecturerManagementControl(userId, role));

        }

        private void btnManageStaff_Click(object sender, EventArgs e)
        {
            LoadUserControl(new StaffManagementControl(userId, role));

        }
    }
}
