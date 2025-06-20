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
    internal class LecturerController
    {
        public static void AddLecturer(Lecturer lecturer)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"INSERT INTO Lecturers 
                         (UserId, FirstName, LastName, Contact, Email, Address) 
                         VALUES (@UserId, @FirstName, @LastName, @Contact, @Email, @Address)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", lecturer.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", lecturer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", lecturer.LastName);
                    cmd.Parameters.AddWithValue("@Contact", lecturer.Contact);
                    cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                    cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void UpdateLecturer(Lecturer lecturer)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"UPDATE Lecturers SET 
                         UserId = @UserId, 
                         FirstName = @FirstName, 
                         LastName = @LastName, 
                         Contact = @Contact, 
                         Email = @Email, 
                         Address = @Address 
                         WHERE LecturerId = @LecturerId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", lecturer.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", lecturer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", lecturer.LastName);
                    cmd.Parameters.AddWithValue("@Contact", lecturer.Contact);
                    cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                    cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                    cmd.Parameters.AddWithValue("@LecturerId", lecturer.LecturerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteLecturer(int lecturerId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "DELETE FROM Lecturers WHERE LecturerId = @LecturerId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Lecturer> GetAllLecturers()
        {
            var list = new List<Lecturer>();
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"SELECT LecturerId, UserId, FirstName, LastName, Contact, Email, Address FROM Lecturers";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Lecturer
                        {
                            LecturerId = Convert.ToInt32(reader["LecturerId"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public static Lecturer GetLecturerByUserId(int userId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT * FROM Lecturers WHERE UserId = @UserId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lecturer
                            {
                                LecturerId = Convert.ToInt32(reader["LecturerId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Contact = reader["Contact"].ToString(),
                                Email = reader["Email"].ToString(),
                                Address = reader["Address"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
