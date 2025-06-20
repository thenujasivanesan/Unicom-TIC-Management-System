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
    internal class StaffController
    {
        // Add staff
        public static void AddStaff(Staff staff)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"INSERT INTO Staff 
                (UserId, FirstName, LastName, Gender, DateOfBirth, Contact, Email, Address)
                VALUES
                (@UserId, @FirstName, @LastName, @Gender, @DateOfBirth, @Contact, @Email, @Address)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", staff.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", staff.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", staff.LastName);
                    cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", staff.DateOfBirth.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Contact", staff.Contact);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@Address", staff.Address);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update staff
        public static void UpdateStaff(Staff staff)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = @"UPDATE Staff SET 
                FirstName = @FirstName,
                LastName = @LastName,
                Gender = @Gender,
                DateOfBirth = @DateOfBirth,
                Contact = @Contact,
                Email = @Email,
                Address = @Address,
                UserId = @UserId
                WHERE StaffId = @StaffId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", staff.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", staff.LastName);
                    cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", staff.DateOfBirth.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Contact", staff.Contact);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@Address", staff.Address);
                    cmd.Parameters.AddWithValue("@UserId", staff.UserId);
                    cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete staff
        public static void DeleteStaff(int staffId)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "DELETE FROM Staff WHERE StaffId = @StaffId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", staffId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get all staff
        public static List<Staff> GetAllStaff()
        {
            var list = new List<Staff>();

            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT * FROM Staff";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Staff
                        {
                            StaffId = Convert.ToInt32(reader["StaffId"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Contact = reader["Contact"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}
