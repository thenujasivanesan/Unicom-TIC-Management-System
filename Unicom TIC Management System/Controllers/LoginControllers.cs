﻿using System;
using System.Collections;
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
    internal class LoginControllers
    {
        public LoginInfo Login(string username, string password)
        {
            // Checking if username or password is empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    // SQL query to match username and password
                    var cmd = new SQLiteCommand("SELECT UserId, Username, Role FROM Users WHERE Username=@u AND Password=@p", conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // if matching user found this reads and returns login infos
                        if (reader.Read())
                        {
                            return new LoginInfo
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Role = reader.GetString(2)
                            };
                        }
                    }
                }
                // if no match found returns null
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login: " + ex.Message, "Database Error");
                return null;
            }
        }

    }
}
    
