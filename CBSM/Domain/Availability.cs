using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Stores the availability of a (film)location
    /// </summary>
    class Availability
    {
        #region "Fields"

        private int id;
        private DateTime from;
        private DateTime to;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the start date and time of the locations availability
        /// </summary>
        public DateTime From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        /// <summary>
        /// Gets or sets the end date and time of the locations availabitlity
        /// </summary>
        public DateTime To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
