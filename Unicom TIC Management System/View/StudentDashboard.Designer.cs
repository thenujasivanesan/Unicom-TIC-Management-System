namespace Unicom_TIC_Management_System.View
{
    partial class StudentDashboard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnViewTimetable = new System.Windows.Forms.Button();
            this.btnViewMarks = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnViewTimetable);
            this.panel1.Controls.Add(this.btnViewMarks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 654);
            this.panel1.TabIndex = 0;
            // 
            // btnViewTimetable
            // 
            this.btnViewTimetable.Location = new System.Drawing.Point(34, 300);
            this.btnViewTimetable.Name = "btnViewTimetable";
            this.btnViewTimetable.Size = new System.Drawing.Size(123, 23);
            this.btnViewTimetable.TabIndex = 0;
            this.btnViewTimetable.Text = "View Timetable";
            this.btnViewTimetable.UseVisualStyleBackColor = true;
            // 
            // btnViewMarks
            // 
            this.btnViewMarks.Location = new System.Drawing.Point(34, 184);
            this.btnViewMarks.Name = "btnViewMarks";
            this.btnViewMarks.Size = new System.Drawing.Size(125, 23);
            this.btnViewMarks.TabIndex = 0;
            this.btnViewMarks.Text = "View Marks";
            this.btnViewMarks.UseVisualStyleBackColor = true;
            this.btnViewMarks.Click += new System.EventHandler(this.btnViewMarks_Click);
            // 
            // panelContent
            // 
            this.panelContent.Location = new System.Drawing.Point(215, 16);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(839, 626);
            this.panelContent.TabIndex = 1;
            // 
            // StudentDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 654);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StudentDashboard";
            this.Text = "StudentDashboard";
            this.Load += new System.EventHandler(this.StudentDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnViewTimetable;
        private System.Windows.Forms.Button btnViewMarks;
    }
}