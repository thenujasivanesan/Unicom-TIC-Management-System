using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class TimetableController
    {
        public static void AddTimetable(Timetable timetable)
        {
            if (timetable.SubjectID == 0)
            {
                MessageBox.Show("Please select a subject.", "Validation Error");
                return;
            }

            if (timetable.RoomID == 0)
            {
                MessageBox.Show("Please select a room.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(timetable.TimeSlot))
            {
                MessageBox.Show("Please select a time slot.", "Validation Error");
                return;
            }

            if (timetable.Date == default)
            {
                MessageBox.Show("Please select a valid date.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"INSERT INTO Timetables (SubjectID, RoomID, TimeSlot, Date) 
                                 VALUES (@SubjectID, @RoomID, @TimeSlot, @Date)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                        cmd.Parameters.AddWithValue("@RoomID", timetable.RoomID);
                        cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                        cmd.Parameters.AddWithValue("@Date", timetable.Date.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding timetable: " + ex.Message, "Database Error");
            }
        }

        public static void UpdateTimetable(Timetable timetable)
        {
            if (timetable.TimetableID == 0)
            {
                MessageBox.Show("Invalid timetable selected.", "Validation Error");
                return;
            }

            if (timetable.SubjectID == 0 || timetable.RoomID == 0 || string.IsNullOrWhiteSpace(timetable.TimeSlot) || timetable.Date == default)
            {
                MessageBox.Show("Please complete all timetable details.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"UPDATE Timetables 
                                 SET SubjectID = @SubjectID, RoomID = @RoomID, TimeSlot = @TimeSlot, Date = @Date 
                                 WHERE TimetableID = @TimetableID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                        cmd.Parameters.AddWithValue("@RoomID", timetable.RoomID);
                        cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                        cmd.Parameters.AddWithValue("@Date", timetable.Date.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@TimetableID", timetable.TimetableID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating timetable: " + ex.Message, "Database Error");
            }
        }

        public static void DeleteTimetable(int timetableId)
        {
            if (timetableId == 0)
            {
                MessageBox.Show("Invalid timetable ID.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Timetables WHERE TimetableID = @TimetableID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TimetableID", timetableId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting timetable: " + ex.Message, "Database Error");
            }
        }

        // get all timetable entries with JOIN info 
        public static DataTable GetAllTimetables()
        {
            var dt = new DataTable();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = @"
                SELECT 
                    t.TimetableID,
                    s.SubjectName,
                    r.RoomName,
                    r.RoomType,
                    t.TimeSlot,
                    t.Date
                FROM Timetables t
                JOIN Subjects s ON t.SubjectID = s.SubjectID
                JOIN Rooms r ON t.RoomID = r.RoomID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading timetables: " + ex.Message, "Database Error");
            }

            return dt;
        }


    }
        
}
