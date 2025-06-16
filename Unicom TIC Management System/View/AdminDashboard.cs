using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_TIC_Management_System.View
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
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
            LoadControlInPanel(new AdminHomeControl());
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
            LoadUserControl(new ExamManagementControl());

        }

        private void btnManageMarks_Click(object sender, EventArgs e)
        {
            LoadUserControl(new MarksManagementControl());
        }

        private void btnManageRooms_Click(object sender, EventArgs e)
        {
            LoadUserControl(new RoomsManagementControl());
        }

        private void btnManageTimetable_Click(object sender, EventArgs e)
        {
            LoadUserControl(new TimetableManagementControl());
        }
    }
}
