using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    internal class DatabaseChecker
    {
        public static bool IsAdminRegistered()
        {
            try
            {
                using (var conn = dbConfig.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin'";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking admin registration: " + ex.Message, "Database Error");
                return false;
            }
        }
    }
}
