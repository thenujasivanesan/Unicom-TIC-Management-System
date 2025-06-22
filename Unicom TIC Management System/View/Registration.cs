using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.View
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

         // Create new Admin user(first run logic)
            var user = new User
            {
                Username = username,
                Password = password,
                Role = "Admin" // First-run register is always Admin
            };

            var controller = new RegisterController();
            bool success = controller.Register(user);

            if (success)
            {
                MessageBox.Show("Registration successful! Please login.");
                this.Hide();
                new Login().Show();    // Redirect to login
            }
            else
            {
                MessageBox.Show("Registration failed. Username might already exist.");
            }
        }

       // password visibility

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0'; // Removing masking
                txtConfirmPassword.PasswordChar = '\0';  // showing confirm password
            }
            else
            {
                txtPassword.PasswordChar = '*';  // Masking again
                txtConfirmPassword.PasswordChar = '*';   // hiding confirm password
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clearing all text fields
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();

            // Unchecking the show password box
            chkShowPassword.Checked = false;

            // Reseting password masking 
            txtPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';

            // setting focus back to username field
            txtUsername.Focus();
        }
    }
}
