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
    internal class RoomController
    {
        // Add Room
        public static void AddRoom(Room room)
        {
            if (string.IsNullOrWhiteSpace(room.RoomName))
            {
                MessageBox.Show("Room name cannot be empty.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(room.RoomType))
            {
                MessageBox.Show("Room type cannot be empty.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "INSERT INTO Rooms (RoomName, RoomType) VALUES (@RoomName, @RoomType)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                        cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding room: " + ex.Message, "Database Error");
            }
        }

        // Update Room
        public static void UpdateRoom(Room room)
        {
            if (string.IsNullOrWhiteSpace(room.RoomName))
            {
                MessageBox.Show("Room name cannot be empty.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(room.RoomType))
            {
                MessageBox.Show("Room type cannot be empty.", "Validation Error");
                return;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "UPDATE Rooms SET RoomName = @RoomName, RoomType = @RoomType WHERE RoomID = @RoomID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                        cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                        cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating room: " + ex.Message, "Database Error");
            }
        }

        // Delete Room
        public static void DeleteRoom(int roomId)
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "DELETE FROM Rooms WHERE RoomID = @RoomID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomID", roomId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting room: " + ex.Message, "Database Error");
            }
        }

        // Get All Rooms
        public static List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "SELECT * FROM Rooms";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                RoomID = Convert.ToInt32(reader["RoomID"]),
                                RoomName = reader["RoomName"].ToString(),
                                RoomType = reader["RoomType"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rooms: " + ex.Message, "Database Error");
            }

            return rooms;
        }

    }
}
