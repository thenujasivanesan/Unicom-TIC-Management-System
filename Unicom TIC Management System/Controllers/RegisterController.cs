using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class RegisterController
    {
        public bool Register(User user)
        {
            // basic validations
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error");
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error");
                return false;
            }

            if (user.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Weak Password");
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.Role))
            {
                MessageBox.Show("Role must be selected.", "Validation Error");
                return false;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    var cmd = new SQLiteCommand("INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)", conn);
                    cmd.Parameters.AddWithValue("@u", user.Username);
                    cmd.Parameters.AddWithValue("@p", user.Password);
                    cmd.Parameters.AddWithValue("@r", user.Role);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message, "Database Error");
                return false;
            }
        }
    }
}
