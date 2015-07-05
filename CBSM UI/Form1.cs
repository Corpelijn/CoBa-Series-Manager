using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CBSM;

namespace CBSM_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*test*/
            Administration ad = new Administration();
            while(!ad.IsDatabaseConnected())
                ad.OpenDatabaseConnection("", 10, CBSM.Database.DatabaseConnectionType.MySQL);
            ad.ImportDatabase();
        }
    }
}
