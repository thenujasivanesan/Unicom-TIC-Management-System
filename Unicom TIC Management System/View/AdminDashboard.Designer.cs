namespace Unicom_TIC_Management_System.View
{
    partial class AdminDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageCourses = new System.Windows.Forms.Button();
            this.btnManageStudents = new System.Windows.Forms.Button();
            this.btnManageExams = new System.Windows.Forms.Button();
            this.btnManageTimetable = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnManageTimetable);
            this.panelSidebar.Controls.Add(this.btnManageExams);
            this.panelSidebar.Controls.Add(this.btnManageStudents);
            this.panelSidebar.Controls.Add(this.btnManageCourses);
            this.panelSidebar.Controls.Add(this.btnManageUsers);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(225, 653);
            this.panelSidebar.TabIndex = 1;
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageUsers.ForeColor = System.Drawing.Color.White;
            this.btnManageUsers.Location = new System.Drawing.Point(12, 154);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(198, 41);
            this.btnManageUsers.TabIndex = 0;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            // 
            // btnManageCourses
            // 
            this.btnManageCourses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCourses.ForeColor = System.Drawing.Color.White;
            this.btnManageCourses.Location = new System.Drawing.Point(12, 212);
            this.btnManageCourses.Name = "btnManageCourses";
            this.btnManageCourses.Size = new System.Drawing.Size(198, 41);
            this.btnManageCourses.TabIndex = 0;
            this.btnManageCourses.Text = "Manage Courses";
            this.btnManageCourses.UseVisualStyleBackColor = true;
            // 
            // btnManageStudents
            // 
            this.btnManageStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageStudents.ForeColor = System.Drawing.Color.White;
            this.btnManageStudents.Location = new System.Drawing.Point(12, 269);
            this.btnManageStudents.Name = "btnManageStudents";
            this.btnManageStudents.Size = new System.Drawing.Size(198, 41);
            this.btnManageStudents.TabIndex = 0;
            this.btnManageStudents.Text = "Manage Students";
            this.btnManageStudents.UseVisualStyleBackColor = true;
            // 
            // btnManageExams
            // 
            this.btnManageExams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageExams.ForeColor = System.Drawing.Color.White;
            this.btnManageExams.Location = new System.Drawing.Point(12, 330);
            this.btnManageExams.Name = "btnManageExams";
            this.btnManageExams.Size = new System.Drawing.Size(198, 41);
            this.btnManageExams.TabIndex = 0;
            this.btnManageExams.Text = "Manage Exams";
            this.btnManageExams.UseVisualStyleBackColor = true;
            // 
            // btnManageTimetable
            // 
            this.btnManageTimetable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageTimetable.ForeColor = System.Drawing.Color.White;
            this.btnManageTimetable.Location = new System.Drawing.Point(12, 396);
            this.btnManageTimetable.Name = "btnManageTimetable";
            this.btnManageTimetable.Size = new System.Drawing.Size(198, 41);
            this.btnManageTimetable.TabIndex = 0;
            this.btnManageTimetable.Text = "Manage Timetable";
            this.btnManageTimetable.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(12, 541);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(198, 41);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // panelMainContent
            // 
            this.panelMainContent.Location = new System.Drawing.Point(231, 57);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(839, 584);
            this.panelMainContent.TabIndex = 2;
            this.panelMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContent_Paint);
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminDashboard";
            this.Text = "AdminDashboard";
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            this.panelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnManageTimetable;
        private System.Windows.Forms.Button btnManageExams;
        private System.Windows.Forms.Button btnManageStudents;
        private System.Windows.Forms.Button btnManageCourses;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Panel panelMainContent;
    }
}