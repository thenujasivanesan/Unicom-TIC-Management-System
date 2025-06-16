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
    internal class SubjectController
    {
        public static void AddSubject(Subject subject)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "INSERT INTO Subjects (SubjectName, CourseId) VALUES (@SubjectName, @CourseId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@CourseId", subject.CourseId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateSubject(Subject subject)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "UPDATE Subjects SET SubjectName = @SubjectName, CourseId = @CourseId WHERE SubjectId = @SubjectId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@CourseId", subject.CourseId);
                    cmd.Parameters.AddWithValue("@SubjectId", subject.SubjectId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteSubject(int subjectId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "DELETE FROM Subjects WHERE SubjectId = @SubjectId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Subject> GetAllSubjects()
        {
            var list = new List<Subject>();

            using (var conn = dbConfig.GetConnection())
            {
                string query = @"SELECT s.SubjectId, s.SubjectName, s.CourseId, c.CourseName
                             FROM Subjects s
                             INNER JOIN Courses c ON s.CourseId = c.CourseId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Subject
                        {
                            SubjectId = Convert.ToInt32(reader["SubjectId"]),
                            SubjectName = reader["SubjectName"].ToString(),
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
