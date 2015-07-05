using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Describes a person from the real world that plays a character or fills up any other role
    /// </summary>
    class Person
    {
        #region "Fields"

        private int id;
        private string name;
        private string account;
        private string password;
        private List<Access> access;
        private List<AccessGroup> groups;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the name of the person
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the unique account name for the person
        /// </summary>
        public string Account
        {
            get { return this.account; }
            set { this.account = value; }
        }

        /// <summary>
        /// Gets or sets the password for the account
        /// </summary>
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        /// <summary>
        /// Gets or sets the access rights for this person (account)
        /// </summary>
        public List<Access> Access
        {
            get { return new List<Access>(this.access); }
            set { this.access = value; }
        }

        /// <summary>
        /// Gets or sets the accessgroups for this person (account)
        /// </summary>
        public List<AccessGroup> Groups
        {
            get { return new List<AccessGroup>(this.groups); }
            set { this.groups = value; }
        }

        #endregion

        #region "Methods"
        #endregion

    }
}
