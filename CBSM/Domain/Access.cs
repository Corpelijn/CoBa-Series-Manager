using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Gives a person (user) access to specified series, episodes, scenes, etc.
    /// </summary>
    class Access
    {
        #region "Fields"

        private int id;
        private string description;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the description of the object access is given to
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
