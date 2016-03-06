using CBSM;
using CBSM.Database;
using CBSM.Domain;
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
            //DatabaseManager.GetInstance();

            Serie serie = new Serie("Frank & Dale");
            Seizoen seizoen = new Seizoen(1, serie);
            Seizoen seizoen2 = new Seizoen(2, serie);

            serie.AddSeizoen(seizoen);
            serie.AddSeizoen(seizoen2);

            serie.WriteToDatabase();
            seizoen.WriteToDatabase();
            seizoen2.WriteToDatabase();

            //try
            {
                //Account test = Account.GetFromDatabase("username=\'corpelijn\'");
                //Customer c = Customer.GetFromDatabase("id=1");

                //Console.WriteLine(c);
                //Console.WriteLine(test);
            }
            //catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            Console.ReadKey();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
