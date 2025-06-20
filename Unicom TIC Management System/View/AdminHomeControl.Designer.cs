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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Cambria", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblWelcome.Location = new System.Drawing.Point(12, 39);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(36, 43);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "c";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Cambria", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSummary.Location = new System.Drawing.Point(20, 162);
            this.lblSummary.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(30, 43);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "l";
            this.lblSummary.Click += new System.EventHandler(this.lblSummary_Click);
            // 
            // AdminHomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblWelcome);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminHomeControl";
            this.Size = new System.Drawing.Size(629, 509);
            this.Load += new System.EventHandler(this.AdminHomeControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSummary;
    }
}
