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
    internal class LecturerController
    {
        public static void AddLecturer(Lecturer lecturer)
        {
            // Validate all important fields
            if (lecturer.UserId <= 0)
            {
                MessageBox.Show("Please select a valid user.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(lecturer.FirstName) || string.IsNullOrWhiteSpace(lecturer.LastName))
            {
                MessageBox.Show("First Name and Last Name are required.", "Validation Error");
                return;
            }

            if (!string.IsNullOrEmpty(lecturer.Contact) && !lecturer.Contact.All(char.IsDigit))
            {
                MessageBox.Show("Contact number must contain only digits.", "Validation Error");
                return;
            }

            if (!string.IsNullOrEmpty(lecturer.Email) && !IsValidEmail(lecturer.Email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"INSERT INTO Lecturers 
                         (UserId, FirstName, LastName, Contact, Email, Address) 
                         VALUES (@UserId, @FirstName, @LastName, @Contact, @Email, @Address)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", lecturer.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", lecturer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", lecturer.LastName);
                        cmd.Parameters.AddWithValue("@Contact", lecturer.Contact);
                        cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                        cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding lecturer: " + ex.Message, "Database Error");
            }
        }

        public static void UpdateLecturer(Lecturer lecturer)
        {
            if (string.IsNullOrEmpty(lecturer.FirstName) || string.IsNullOrEmpty(lecturer.LastName))
            {
                MessageBox.Show("First Name and Last Name are required.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"UPDATE Lecturers SET 
                         UserId = @UserId, 
                         FirstName = @FirstName, 
                         LastName = @LastName, 
                         Contact = @Contact, 
                         Email = @Email, 
                         Address = @Address 
                         WHERE LecturerId = @LecturerId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", lecturer.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", lecturer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", lecturer.LastName);
                        cmd.Parameters.AddWithValue("@Contact", lecturer.Contact);
                        cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                        cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                        cmd.Parameters.AddWithValue("@LecturerId", lecturer.LecturerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating lecturer: " + ex.Message, "Database Error");
            }
        }

        public static void DeleteLecturer(int lecturerId)
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Lecturers WHERE LecturerId = @LecturerId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting lecturer: " + ex.Message, "Database Error");
            }
        }

        public static List<Lecturer> GetAllLecturers()
        {
            var list = new List<Lecturer>();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"SELECT LecturerId, UserId, FirstName, LastName, Contact, Email, Address FROM Lecturers";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Lecturer
                            {
                                LecturerId = Convert.ToInt32(reader["LecturerId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Contact = reader["Contact"].ToString(),
                                Email = reader["Email"].ToString(),
                                Address = reader["Address"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lecturers: " + ex.Message, "Database Error");
            }

            return list;
        }

        public static Lecturer GetLecturerByUserId(int userId)
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "SELECT * FROM Lecturers WHERE UserId = @UserId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Lecturer
                                {
                                    LecturerId = Convert.ToInt32(reader["LecturerId"]),
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Contact = reader["Contact"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Address = reader["Address"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching lecturer: " + ex.Message, "Database Error");
            }

            return null;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
        
}
