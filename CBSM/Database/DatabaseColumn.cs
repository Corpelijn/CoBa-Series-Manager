using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    public class DatabaseColumn
    {
        #region "Fields"

        private string name;
        private string type;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new DatabaseColumn object
        /// </summary>
        /// <param name="name">The name of the column</param>
        /// <param name="type">The datatype that needs to be stored into the column</param>
        public DatabaseColumn(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the name of the column
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the datatype of the column
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
