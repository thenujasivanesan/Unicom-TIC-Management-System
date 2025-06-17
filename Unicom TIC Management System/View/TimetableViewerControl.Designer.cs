namespace Unicom_TIC_Management_System.View
{
    partial class TimetableViewerControl
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
            this.dgvTimetable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetable)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTimetable
            // 
            this.dgvTimetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimetable.Location = new System.Drawing.Point(112, 189);
            this.dgvTimetable.Name = "dgvTimetable";
            this.dgvTimetable.RowHeadersWidth = 51;
            this.dgvTimetable.RowTemplate.Height = 24;
            this.dgvTimetable.Size = new System.Drawing.Size(557, 209);
            this.dgvTimetable.TabIndex = 0;
            // 
            // TimetableViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTimetable);
            this.Name = "TimetableViewerControl";
            this.Size = new System.Drawing.Size(839, 626);
            this.Load += new System.EventHandler(this.TimetableViewerControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTimetable;
    }
}
