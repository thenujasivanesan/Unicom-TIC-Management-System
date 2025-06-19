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
    internal class UserController
    {
        public static void AddUser(User user)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateUser(User user)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE UserId = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@Id", user.UserId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteUser(int id)
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "DELETE FROM Users WHERE UserId = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<User> GetAllUsers()
        {
            var list = new List<User>();
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT * FROM Users";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
