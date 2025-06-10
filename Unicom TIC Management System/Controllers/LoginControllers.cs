using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class LoginControllers
    {
        public bool ValidateUser(string username, string password, out string role)
        {
            role = string.Empty;
            string connectionString = "Data Source=unicomtic.db;Version=3;";
            string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
