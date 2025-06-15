namespace Unicom_TIC_Management_System.View
{
    partial class AdminHomeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTotalStudents = new System.Windows.Forms.Panel();
            this.lblTotalStudents = new System.Windows.Forms.Label();
            this.panelTotalLecturers = new System.Windows.Forms.Panel();
            this.lblTotalLecturers = new System.Windows.Forms.Label();
            this.panelTotalStaff = new System.Windows.Forms.Panel();
            this.lblTotalStaff = new System.Windows.Forms.Label();
            this.panelTotalStudents.SuspendLayout();
            this.panelTotalLecturers.SuspendLayout();
            this.panelTotalStaff.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTotalStudents
            // 
            this.panelTotalStudents.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelTotalStudents.Controls.Add(this.lblTotalStudents);
            this.panelTotalStudents.Location = new System.Drawing.Point(310, 14);
            this.panelTotalStudents.Name = "panelTotalStudents";
            this.panelTotalStudents.Size = new System.Drawing.Size(290, 113);
            this.panelTotalStudents.TabIndex = 0;
            // 
            // lblTotalStudents
            // 
            this.lblTotalStudents.AutoSize = true;
            this.lblTotalStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStudents.ForeColor = System.Drawing.Color.White;
            this.lblTotalStudents.Location = new System.Drawing.Point(31, 33);
            this.lblTotalStudents.Name = "lblTotalStudents";
            this.lblTotalStudents.Size = new System.Drawing.Size(240, 36);
            this.lblTotalStudents.TabIndex = 0;
            this.lblTotalStudents.Text = "Total Students: 0";
            // 
            // panelTotalLecturers
            // 
            this.panelTotalLecturers.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelTotalLecturers.Controls.Add(this.lblTotalLecturers);
            this.panelTotalLecturers.Location = new System.Drawing.Point(310, 200);
            this.panelTotalLecturers.Name = "panelTotalLecturers";
            this.panelTotalLecturers.Size = new System.Drawing.Size(290, 113);
            this.panelTotalLecturers.TabIndex = 0;
            // 
            // lblTotalLecturers
            // 
            this.lblTotalLecturers.AutoSize = true;
            this.lblTotalLecturers.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLecturers.ForeColor = System.Drawing.Color.White;
            this.lblTotalLecturers.Location = new System.Drawing.Point(31, 37);
            this.lblTotalLecturers.Name = "lblTotalLecturers";
            this.lblTotalLecturers.Size = new System.Drawing.Size(246, 36);
            this.lblTotalLecturers.TabIndex = 0;
            this.lblTotalLecturers.Text = "Total Lecturers: 0";
            // 
            // panelTotalStaff
            // 
            this.panelTotalStaff.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelTotalStaff.Controls.Add(this.lblTotalStaff);
            this.panelTotalStaff.Location = new System.Drawing.Point(310, 390);
            this.panelTotalStaff.Name = "panelTotalStaff";
            this.panelTotalStaff.Size = new System.Drawing.Size(290, 113);
            this.panelTotalStaff.TabIndex = 0;
            // 
            // lblTotalStaff
            // 
            this.lblTotalStaff.AutoSize = true;
            this.lblTotalStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStaff.ForeColor = System.Drawing.Color.White;
            this.lblTotalStaff.Location = new System.Drawing.Point(31, 33);
            this.lblTotalStaff.Name = "lblTotalStaff";
            this.lblTotalStaff.Size = new System.Drawing.Size(182, 36);
            this.lblTotalStaff.TabIndex = 0;
            this.lblTotalStaff.Text = "Total Staff: 0";
            // 
            // AdminHomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTotalStaff);
            this.Controls.Add(this.panelTotalLecturers);
            this.Controls.Add(this.panelTotalStudents);
            this.Name = "AdminHomeControl";
            this.Size = new System.Drawing.Size(857, 625);
            this.Load += new System.EventHandler(this.AdminHomeControl_Load);
            this.panelTotalStudents.ResumeLayout(false);
            this.panelTotalStudents.PerformLayout();
            this.panelTotalLecturers.ResumeLayout(false);
            this.panelTotalLecturers.PerformLayout();
            this.panelTotalStaff.ResumeLayout(false);
            this.panelTotalStaff.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTotalStudents;
        private System.Windows.Forms.Label lblTotalStudents;
        private System.Windows.Forms.Panel panelTotalLecturers;
        private System.Windows.Forms.Label lblTotalLecturers;
        private System.Windows.Forms.Panel panelTotalStaff;
        private System.Windows.Forms.Label lblTotalStaff;
    }
}
