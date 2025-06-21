using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Unicom_TIC_Management_System.View
{
    public partial class AdminDashboard : Form
    {
        private int userId;
        private string role;


        public AdminDashboard(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;

            ApplyRoleRestrictions();
        }


        private void ApplyRoleRestrictions()
        {
            if (role == "Student")
            {
                // Hide or disable admin-only buttons
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
                // Admin or default role: enable all buttons
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


        private void LoadControlInPanel(UserControl control)
        {
            panelMainContent.Controls.Clear();        // Remove old content
            control.Dock = DockStyle.Fill;            // Make it fill the panel
            panelMainContent.Controls.Add(control);   // Show it
            control.BringToFront();                   // Bring to front
        }


        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadControlInPanel(new AdminHomeControl(userId, role));
        }

        private void LoadUserControl(UserControl control)
        {
            panelMainContent.Controls.Clear();          // Remove old content
            control.Dock = DockStyle.Fill;       // Fill the panel
            panelMainContent.Controls.Add(control);     // Add new content
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UserManagementControl());
        }

        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            LoadUserControl(new CourseManagementControl());
        }

        private void btnManageSubjects_Click(object sender, EventArgs e)
        {
            LoadUserControl(new SubjectManagementControl());
        }

        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            LoadUserControl(new StudentManagementControl());

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
            LoadUserControl(new RoomsManagementControl());
        }

        private void btnManageTimetable_Click(object sender, EventArgs e)
        {
            LoadUserControl(new TimetableManagementControl(userId, role));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Show the Login Form again
            Login loginForm = new Login();
            loginForm.Show();

            // Close the current Dashboard
            this.Close();
        }

        private void btnManageLecturers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new LecturerManagementControl());

        }

        private void btnManageStaff_Click(object sender, EventArgs e)
        {
            LoadUserControl(new StaffManagementControl());

        }
    }
}
