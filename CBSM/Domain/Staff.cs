using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Describes a person that work on the background of a scene or episode, like camera operator.
    /// </summary>
    class Staff
    {
        #region "Fields"

        private Person person;
        private string role;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the person who portrayed this staff member
        /// </summary>
        public Person Person
        {
            get { return this.person; }
            set { this.person = value; }
        }

        /// <summary>
        /// Gets or sets the role of this staff member
        /// </summary>
        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
