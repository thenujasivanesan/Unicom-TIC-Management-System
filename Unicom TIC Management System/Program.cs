using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Repositories;
using Unicom_TIC_Management_System.View;

namespace Unicom_TIC_Management_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DatabaseManager.InitializeDatabase();


            // Checking if an Admin is already registered
            bool isAdminExists = DatabaseChecker.IsAdminRegistered();

            if (isAdminExists)
            {
                Application.Run(new Login()); // Showing Login form
            }
            else
            {
                Application.Run(new Registration()); // First time show Admin registration
            }
        }
    
    }
}
