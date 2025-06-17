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
    internal class ExamController
    {
        public static void AddExam(Exam exam)
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

        public static void UpdateExam(Exam exam)
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

        public static void DeleteExam(int id)
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

        public static List<(int ExamId, string ExamName, string SubjectName)> GetAllExams()
        {
            var list = new List<(int, string, string)>();
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
            return list;
        }
    }
}
