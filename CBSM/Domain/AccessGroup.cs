using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Creates a group of rights that can be given to a person (user)
    /// </summary>
    class AccessGroup
    {
        #region "Fields"

        private int id;
        private string name;
        private List<Access> access;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the name of the group
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the rights of the group
        /// </summary>
        public List<Access> Access
        {
            get { return new List<Access>(this.access); }
            set { this.access = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
