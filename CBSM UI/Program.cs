using CBSM;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CBSM_UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Administration.Instance = new Administration();

            Console.Title = "CoBa Series Manager";

            while (!Administration.Instance.IsDatabaseConnected())
            {
                Console.WriteLine("Trying to connect to the database . . .");
                if (!Administration.Instance.OpenDatabaseConnection("192.168.94.5", 1521, "xe", CBSM.Database.DatabaseConnectionType.Oracle, "bmt", "bmt"))
                {
                    Console.WriteLine("Connecting to the database failed!");
                }
            }
            Console.WriteLine("The connections to the database is open");

            Console.WriteLine("Check if the database tables exist . . .");
            while (!Administration.Instance.CheckDatabaseTables())
            {
                Console.WriteLine("Creating the required tables . . .");
            }

            Console.WriteLine("The required tables exist");

            Console.ReadKey();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
