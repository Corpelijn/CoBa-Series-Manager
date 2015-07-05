using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Describes an attribute that can be used in a scene
    /// </summary>
    class Attribute
    {
        #region "Fields"

        private int id;
        private string description;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the description of the object (attribute)
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
