using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class StudentController
    {
        public static void AddStudent(Student student)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"INSERT INTO Students 
                (UserId, FirstName, LastName, Gender, DateOfBirth, Contact, Email, Address, CourseId) 
                VALUES 
                (@UserId, @FirstName, @LastName, @Gender, @DateOfBirth, @Contact, @Email, @Address, @CourseId)";

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
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateStudent(Student student)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"UPDATE Students SET 
                FirstName = @FirstName,
                UserId = @UserId
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

        public static void DeleteStudent(int id)
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

        public static List<Student> GetAllStudents()
        {
            var list = new List<Student>();
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
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Contact = reader["Contact"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            CourseName = reader["CourseName"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
