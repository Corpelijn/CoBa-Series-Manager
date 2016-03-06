using CBSM.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    public class Customer : DBMS<Customer>
    {
        private Account account;    // FK
        private string naam;

        public Customer(string naam)
            : base()
        {
            this.naam = naam;
        }

        public Customer()
            : base()
        {
        }

        public Account Account
        {
            get { return this.account; }
            set { this.account = value; }
        }
    }
}
