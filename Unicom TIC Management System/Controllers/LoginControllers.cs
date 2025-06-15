using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class LoginControllers
    {
        public string Login(string username, string password)
        {
            using (var conn = dbConfig.GetConnection())
            {
                var cmd = new SQLiteCommand("SELECT Role FROM Users WHERE Username=@u AND Password=@p", conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                var result = cmd.ExecuteScalar();

                return result?.ToString(); 
            }
        }



    }
}
    
