namespace Unicom_TIC_Management_System.View
{
    partial class ExamManagementControl
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
            this.lblExamName = new System.Windows.Forms.Label();
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.btnAddExam = new System.Windows.Forms.Button();
            this.btnUpdateExam = new System.Windows.Forms.Button();
            this.btnDeleteExam = new System.Windows.Forms.Button();
            this.btnClearExam = new System.Windows.Forms.Button();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            this.SuspendLayout();
            // 
            // lblExamName
            // 
            this.lblExamName.AutoSize = true;
            this.lblExamName.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblExamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExamName.Location = new System.Drawing.Point(18, 373);
            this.lblExamName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExamName.Name = "lblExamName";
            this.lblExamName.Size = new System.Drawing.Size(81, 16);
            this.lblExamName.TabIndex = 0;
            this.lblExamName.Text = "Exam Name";
            // 
            // txtExamName
            // 
            this.txtExamName.Location = new System.Drawing.Point(127, 369);
            this.txtExamName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtExamName.Name = "txtExamName";
            this.txtExamName.Size = new System.Drawing.Size(76, 20);
            this.txtExamName.TabIndex = 1;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(18, 422);
            this.lblSubject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(52, 16);
            this.lblSubject.TabIndex = 0;
            this.lblSubject.Text = "Subject";
            this.lblSubject.Click += new System.EventHandler(this.lblSubject_Click);
            // 
            // cmbSubject
            // 
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(127, 417);
            this.cmbSubject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(92, 21);
            this.cmbSubject.TabIndex = 2;
            // 
            // btnAddExam
            // 
            this.btnAddExam.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAddExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddExam.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddExam.Location = new System.Drawing.Point(21, 287);
            this.btnAddExam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddExam.Name = "btnAddExam";
            this.btnAddExam.Size = new System.Drawing.Size(90, 30);
            this.btnAddExam.TabIndex = 3;
            this.btnAddExam.Text = "Add";
            this.btnAddExam.UseVisualStyleBackColor = false;
            this.btnAddExam.Click += new System.EventHandler(this.btnAddExam_Click);
            // 
            // btnUpdateExam
            // 
            this.btnUpdateExam.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnUpdateExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateExam.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUpdateExam.Location = new System.Drawing.Point(169, 287);
            this.btnUpdateExam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdateExam.Name = "btnUpdateExam";
            this.btnUpdateExam.Size = new System.Drawing.Size(90, 30);
            this.btnUpdateExam.TabIndex = 4;
            this.btnUpdateExam.Text = "Update";
            this.btnUpdateExam.UseVisualStyleBackColor = false;
            this.btnUpdateExam.Click += new System.EventHandler(this.btnUpdateExam_Click);
            // 
            // btnDeleteExam
            // 
            this.btnDeleteExam.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDeleteExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteExam.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDeleteExam.Location = new System.Drawing.Point(333, 287);
            this.btnDeleteExam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDeleteExam.Name = "btnDeleteExam";
            this.btnDeleteExam.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteExam.TabIndex = 4;
            this.btnDeleteExam.Text = "Delete Exam";
            this.btnDeleteExam.UseVisualStyleBackColor = false;
            this.btnDeleteExam.Click += new System.EventHandler(this.btnDeleteExam_Click);
            // 
            // btnClearExam
            // 
            this.btnClearExam.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClearExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearExam.ForeColor = System.Drawing.SystemColors.Control;
            this.btnClearExam.Location = new System.Drawing.Point(488, 287);
            this.btnClearExam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClearExam.Name = "btnClearExam";
            this.btnClearExam.Size = new System.Drawing.Size(90, 30);
            this.btnClearExam.TabIndex = 4;
            this.btnClearExam.Text = "Clear";
            this.btnClearExam.UseVisualStyleBackColor = false;
            this.btnClearExam.Click += new System.EventHandler(this.btnClearExam_Click);
            // 
            // dgvExams
            // 
            this.dgvExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(62, 15);
            this.dgvExams.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.ReadOnly = true;
            this.dgvExams.RowHeadersWidth = 51;
            this.dgvExams.RowTemplate.Height = 24;
            this.dgvExams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExams.Size = new System.Drawing.Size(516, 217);
            this.dgvExams.TabIndex = 5;
            this.dgvExams.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExams_CellClick);
            // 
            // ExamManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.btnClearExam);
            this.Controls.Add(this.btnDeleteExam);
            this.Controls.Add(this.btnUpdateExam);
            this.Controls.Add(this.btnAddExam);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.txtExamName);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.lblExamName);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ExamManagementControl";
            this.Size = new System.Drawing.Size(629, 509);
            this.Load += new System.EventHandler(this.ExamManagementControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExamName;
        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Button btnAddExam;
        private System.Windows.Forms.Button btnUpdateExam;
        private System.Windows.Forms.Button btnDeleteExam;
        private System.Windows.Forms.Button btnClearExam;
        private System.Windows.Forms.DataGridView dgvExams;
    }
}
