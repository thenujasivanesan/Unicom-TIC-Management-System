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
    internal class RegisterController
    {
        public bool Register(User user)
        {
            using (var conn = dbConfig.GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)", conn);
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@p", user.Password); 
                cmd.Parameters.AddWithValue("@r", user.Role);

                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
