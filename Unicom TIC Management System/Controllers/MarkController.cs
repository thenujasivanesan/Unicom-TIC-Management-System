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
    internal class MarkController
    {
        public static void AddMark(Mark mark)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "INSERT INTO Marks (StudentId, ExamId, Score) VALUES (@StudentId, @ExamId, @Score)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                    cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                    cmd.Parameters.AddWithValue("@Score", mark.Score);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateMark(Mark mark)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "UPDATE Marks SET StudentId = @StudentId, ExamId = @ExamId, Score = @Score WHERE MarkId = @MarkId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                    cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                    cmd.Parameters.AddWithValue("@Score", mark.Score);
                    cmd.Parameters.AddWithValue("@MarkId", mark.MarkId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteMark(int markId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "DELETE FROM Marks WHERE MarkId = @MarkId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MarkId", markId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Mark> GetAllMarks()
        {
            var list = new List<Mark>();
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT * FROM Marks";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Mark
                        {
                            MarkId = Convert.ToInt32(reader["MarkId"]),
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            ExamId = Convert.ToInt32(reader["ExamId"]),
                            Score = Convert.ToInt32(reader["Score"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
