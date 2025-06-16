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
    internal class CourseController
    {
        public static void AddCourse(Course course)
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

        public static void UpdateCourse(Course course)
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

        public static void DeleteCourse(int id)
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

        public static List<Course> GetAllCourses()
        {
            var list = new List<Course>();
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
            return list;
        }
    }
}
