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
    internal class MarkController
    {
        public static void AddMark(Mark mark)
        {
            // Basic validation
            if (mark.StudentId <= 0)
            {
                MessageBox.Show("Please select a valid student.", "Validation Error");
                return;
            }

            if (mark.ExamId <= 0)
            {
                MessageBox.Show("Please select a valid exam.", "Validation Error");
                return;
            }

            if (mark.Score < 0 || mark.Score > 100)
            {
                MessageBox.Show("Score must be between 0 and 100.", "Validation Error");
                return;
            }

            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error adding mark: " + ex.Message, "Database Error");
            }
        }

        public static void UpdateMark(Mark mark)
        {
            // Same validation as AddMark
            if (mark.StudentId <= 0)
            {
                MessageBox.Show("Please select a valid student.", "Validation Error");
                return;
            }

            if (mark.ExamId <= 0)
            {
                MessageBox.Show("Please select a valid exam.", "Validation Error");
                return;
            }

            if (mark.Score < 0 || mark.Score > 100)
            {
                MessageBox.Show("Score must be between 0 and 100.", "Validation Error");
                return;
            }

            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error updating mark: " + ex.Message, "Database Error");
            }
        }

        public static void DeleteMark(int markId)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting mark: " + ex.Message, "Database Error");
            }
        }

        public static List<Mark> GetAllMarks()
        {
            var list = new List<Mark>();

            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading marks: " + ex.Message, "Database Error");
            }

            return list;
        }
    }
}
