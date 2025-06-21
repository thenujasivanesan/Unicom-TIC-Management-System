using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class StudentController
    {
        // ✅ Helper method to check if email format is valid
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // ✅ Add new student to the database
        public static void AddStudent(Student student)
        {
            // Basic validations
            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                MessageBox.Show("First name is required.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                MessageBox.Show("Last name is required.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.Gender))
            {
                MessageBox.Show("Gender is required.", "Validation Error");
                return;
            }

            if (student.DateOfBirth == default)
            {
                MessageBox.Show("Date of birth is required.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.Contact) || student.Contact.Length < 7)
            {
                MessageBox.Show("Enter a valid contact number.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.Email) || !IsValidEmail(student.Email))
            {
                MessageBox.Show("Enter a valid email address.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"INSERT INTO Students 
                (UserId, FirstName, LastName, Gender, DateOfBirth, Contact, Email, Address, CourseId) 
                VALUES 
                (@UserId, @FirstName, @LastName, @Gender, @DateOfBirth, @Contact, @Email, @Address, @CourseId)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", student.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", student.LastName);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Contact", student.Contact);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@Address", student.Address);
                        cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message, "Database Error");
            }
        }

        // ✅ Update existing student record
        public static void UpdateStudent(Student student)
        {
            // Same validations as Add
            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                MessageBox.Show("First name is required.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                MessageBox.Show("Last name is required.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.Gender))
            {
                MessageBox.Show("Gender is required.", "Validation Error");
                return;
            }

            if (student.DateOfBirth == default)
            {
                MessageBox.Show("Date of birth is required.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.Contact) || student.Contact.Length < 7)
            {
                MessageBox.Show("Enter a valid contact number.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(student.Email) || !IsValidEmail(student.Email))
            {
                MessageBox.Show("Enter a valid email address.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"UPDATE Students SET 
                FirstName = @FirstName,
                UserId = @UserId,
                LastName = @LastName,
                Gender = @Gender,
                DateOfBirth = @DateOfBirth,
                Contact = @Contact,
                Email = @Email,
                Address = @Address,
                CourseId = @CourseId
                WHERE StudentId = @StudentId";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("@UserId", student.UserId);
                        cmd.Parameters.AddWithValue("@LastName", student.LastName);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Contact", student.Contact);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@Address", student.Address);
                        cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                        cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message, "Database Error");
            }
        }

        // ✅ Delete student by ID
        public static void DeleteStudent(int id)
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Students WHERE StudentId = @id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message, "Database Error");
            }
        }

        // ✅ Get all students and their course names
        public static List<Student> GetAllStudents()
        {
            var list = new List<Student>();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"SELECT s.*, c.CourseName 
                                 FROM Students s 
                                 JOIN Courses c ON s.CourseId = c.CourseId";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Student
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Contact = reader["Contact"].ToString(),
                                Email = reader["Email"].ToString(),
                                Address = reader["Address"].ToString(),
                                CourseId = Convert.ToInt32(reader["CourseId"]),
                                CourseName = reader["CourseName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message, "Database Error");
            }

            return list;
        }


    }
}
