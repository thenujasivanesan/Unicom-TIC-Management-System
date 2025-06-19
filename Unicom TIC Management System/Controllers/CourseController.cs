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
    internal class CourseController
    {
        public static void AddCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                MessageBox.Show("Course name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                using (var conn = dbConfig.GetConnection())
                {
                    string query = "INSERT INTO Courses (CourseName) VALUES (@CourseName)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding course: " + ex.Message);

            }
        }
        

        public static void UpdateCourse(Course course)
        {
            if (string.IsNullOrEmpty(course.CourseName))
            {
                MessageBox.Show("Course name cannot be empty.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "UPDATE Courses SET CourseName = @CourseName WHERE CourseId = @CourseId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);

                        cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating course: " + ex.Message);
            }


        }

        public static void DeleteCourse(int id)
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Courses WHERE CourseId = @CourseId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseId", id);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting course: " + ex.Message);
            }

        }

        public static List<Course> GetAllCourses()
        {
            var list = new List<Course>();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "SELECT * FROM Courses";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Course
                            {
                                CourseId = Convert.ToInt32(reader["CourseId"]),
                                CourseName = reader["CourseName"].ToString(),

                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message);
            }

            return list;
        }
    }
}
