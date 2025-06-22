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
    internal class UserController
    {
        // this returns true if successful and false otherwise
        public static bool AddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error");
                return false;
            }
            // validating password is not empty and at least 6 characters long
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation Error");
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
                    //  Check if username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", user.Username);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another.", "Validation Error");
                            return false;
                        }
                    }

                    
                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Database Error");
                return false;
            }
        }

        public static bool UpdateUser(User user)
        {
            if (user.UserId <= 0)
            {
                MessageBox.Show("Invalid user ID.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation Error");
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
                    string query = "UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE UserId = @Id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@Id", user.UserId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Database Error");
                return false;
            }
        }

        public static bool DeleteUser(int id)
        {
            if (id <= 0)
            {
                MessageBox.Show("Invalid user ID.", "Validation Error");
                return false;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Users WHERE UserId = @Id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Database Error");
                return false;
            }
        }
        public static List<User> GetAllUsers()
        {
            var list = new List<User>();
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "SELECT * FROM Users";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = reader["Role"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving users: {ex.Message}", "Database Error");
            }
            return list;
        }
    }
}
