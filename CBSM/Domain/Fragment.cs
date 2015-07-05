using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Hold a reference to a data file containing one or more scens
    /// </summary>
    class Fragment
    {
        #region "Fields"

        private int id;
        private FragmentStatus status;

        #endregion

        #region "Constructors"
        #endregion 

        #region "Properties"

        /// <summary>
        /// Gets or sets the status of a fragment
        /// </summary>
        public FragmentStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
