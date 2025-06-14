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
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.View
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginControllers controller = new LoginControllers();
            string role = controller.Login(txtUsername2.Text, txtPassword2.Text);

            if (role == null)
            {
                MessageBox.Show("Invalid credentials");
            }
            else
            {
                MessageBox.Show("Login Successful!");

                this.Hide(); // Hide Login Form

                switch (role)
                {
                    case "Admin":
                        new AdminDashboard().Show();
                        break;
                    case "Student":
                        new StudentDashboard().Show();
                        break;
                    case "Staff":
                        new StaffDashboard().Show();
                        break;
                    case "Lecturer":
                        new LecturerDashboard().Show();
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clear input fields
            txtUsername2.Clear();
            txtPassword2.Clear();

            // Uncheck "Show Password" checkbox
            chkShowPassword2.Checked = false;

            // Reset password hiding
            txtPassword2.PasswordChar = '*'; // If using PasswordChar


            // Set focus back to Username
            txtUsername2.Focus();

        }

        private void chkShowPassword2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword2.Checked)
            {
                txtPassword2.PasswordChar = '\0'; // Show actual password
            }
            else
            {
                txtPassword2.PasswordChar = '*';  // Mask it again
            }
        }
    }
}
