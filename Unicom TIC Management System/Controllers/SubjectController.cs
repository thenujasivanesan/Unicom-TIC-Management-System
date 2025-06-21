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
    internal class SubjectController
    {
        // ✅ Add new subject
        public static void AddSubject(Subject subject)
        {
            // ✅ Basic validation
            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                MessageBox.Show("Subject name is required.", "Validation Error");
                return;
            }

            if (subject.CourseId == 0)
            {
                MessageBox.Show("Please select a valid course.", "Validation Error");
                return;
            }

            if (subject.LecturerId == 0)
            {
                MessageBox.Show("Please select a lecturer.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "INSERT INTO Subjects (SubjectName, CourseId, LecturerId) VALUES (@SubjectName, @CourseId, @LecturerId)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@CourseId", subject.CourseId);
                        cmd.Parameters.AddWithValue("@LecturerId", subject.LecturerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding subject: " + ex.Message, "Database Error");
            }
        }

        // ✅ Update existing subject
        public static void UpdateSubject(Subject subject)
        {
            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                MessageBox.Show("Subject name is required.", "Validation Error");
                return;
            }

            if (subject.CourseId == 0)
            {
                MessageBox.Show("Please select a valid course.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "UPDATE Subjects SET SubjectName = @SubjectName, CourseId = @CourseId, LecturerId = @LecturerId WHERE SubjectId = @SubjectId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@CourseId", subject.CourseId);
                        cmd.Parameters.AddWithValue("@LecturerId", subject.LecturerId);
                        cmd.Parameters.AddWithValue("@SubjectId", subject.SubjectId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating subject: " + ex.Message, "Database Error");
            }
        }

        // ✅ Delete a subject
        public static void DeleteSubject(int subjectId)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting subject: " + ex.Message, "Database Error");
            }
        }

        // ✅ Get all subjects with their course name (and lecturer if needed)
        public static List<Subject> GetAllSubjects()
        {
            var list = new List<Subject>();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"SELECT s.SubjectId, s.SubjectName, s.CourseId, s.LecturerId, c.CourseName
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
                                LecturerId = reader["LecturerId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LecturerId"]),
                                CourseName = reader["CourseName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message, "Database Error");
            }

            return list;
        }

    }
}
