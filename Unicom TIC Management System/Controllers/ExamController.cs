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
    internal class ExamController
    {
        public static void AddExam(Exam exam)
        {
            if (string.IsNullOrEmpty(exam.ExamName))
            {
                MessageBox.Show("Exam name cannot be empty.", "Validation Error");
                return;
            }

            if (exam.SubjectId <= 0)
            {
                MessageBox.Show("Please select a valid subject.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "INSERT INTO Exams (ExamName, SubjectId) VALUES (@ExamName, @SubjectId)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                        cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding exam: " + ex.Message, "Database Error");
            }
        }

        public static void UpdateExam(Exam exam)
        {
            if (string.IsNullOrEmpty(exam.ExamName))
            {
                MessageBox.Show("Exam name cannot be empty.", "Validation Error");
                return;
            }

            if (exam.SubjectId <= 0)
            {
                MessageBox.Show("Please select a valid subject.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "UPDATE Exams SET ExamName = @ExamName, SubjectId = @SubjectId WHERE ExamId = @ExamId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                        cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                        cmd.Parameters.AddWithValue("@ExamId", exam.ExamId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating exam: " + ex.Message, "Database Error");
            }
        }

        public static void DeleteExam(int id)
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Exams WHERE ExamId = @ExamId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamId", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting exam: " + ex.Message, "Database Error");
            }
        }

        public static List<(int ExamId, string ExamName, string SubjectName)> GetAllExams()
        {
            var list = new List<(int, string, string)>();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"
                    SELECT Exams.ExamId, Exams.ExamName, Subjects.SubjectName 
                    FROM Exams 
                    INNER JOIN Subjects ON Exams.SubjectId = Subjects.SubjectId";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add((
                                Convert.ToInt32(reader["ExamId"]),
                                reader["ExamName"].ToString(),
                                reader["SubjectName"].ToString()
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading exams: " + ex.Message, "Database Error");
            }

            return list;
        }


    }
}
