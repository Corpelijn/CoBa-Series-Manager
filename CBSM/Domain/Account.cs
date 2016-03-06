using CBSM.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    public class Account : DBMS<Account>
    {
        private string username;
        private string password;

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public Account()
        {
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
    }
}
