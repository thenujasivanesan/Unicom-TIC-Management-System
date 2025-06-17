using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class TimetableController
    {
        // ✅ Add Timetable
        public static void AddTimetable(Timetable timetable)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"INSERT INTO Timetables (SubjectID, RoomID, TimeSlot) 
                             VALUES (@SubjectID, @RoomID, @TimeSlot)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                    cmd.Parameters.AddWithValue("@RoomID", timetable.RoomID);
                    cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Update Timetable
        public static void UpdateTimetable(Timetable timetable)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"UPDATE Timetables 
                             SET SubjectID = @SubjectID, RoomID = @RoomID, TimeSlot = @TimeSlot 
                             WHERE TimetableID = @TimetableID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                    cmd.Parameters.AddWithValue("@RoomID", timetable.RoomID);
                    cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                    cmd.Parameters.AddWithValue("@TimetableID", timetable.TimetableID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Delete Timetable
        public static void DeleteTimetable(int timetableId)
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

        // ✅ Get All Timetables (with JOIN)
        public static DataTable GetAllTimetables()
        {
            var dt = new DataTable();
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"
                SELECT 
                    t.TimetableID,
                    s.SubjectName,
                    r.RoomName,
                    r.RoomType,
                    t.TimeSlot
                FROM Timetables t
                JOIN Subjects s ON t.SubjectID = s.SubjectID
                JOIN Rooms r ON t.RoomID = r.RoomID";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var adapter = new SQLiteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
        
}
