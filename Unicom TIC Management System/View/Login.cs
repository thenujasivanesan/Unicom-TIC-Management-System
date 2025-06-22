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
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

            string username = txtUsername2.Text.Trim();
            string password = txtPassword2.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and Password cannot be empty.", "Validation Error");
                return;
            }

            LoginControllers loginController = new LoginControllers();
            LoginInfo loginInfo = loginController.Login(username, password);

            if (loginInfo != null)
            {
                MessageBox.Show("Login successful!");

                // Passing userId and role to AdminDashboard
                AdminDashboard dashboard = new AdminDashboard(loginInfo.UserId, loginInfo.Role);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clearing input fields
            txtUsername2.Clear();
            txtPassword2.Clear();

            // Unchecking "Show Password" checkbox
            chkShowPassword2.Checked = false;

            // Reseting password hiding
            txtPassword2.PasswordChar = '*'; 

            //Setting focus back to Username
            txtUsername2.Focus();

        }

        private void chkShowPassword2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword2.Checked)
            {
                txtPassword2.PasswordChar = '\0'; // Showing actual password
            }
            else
            {
                txtPassword2.PasswordChar = '*';  // Masking it 
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
