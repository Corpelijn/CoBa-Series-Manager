using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    class DatabaseColumn
    {
        #region "Fields"

        private string name;
        private DatabaseColumnType type;
        private bool primaryKey;
        private DatabaseColumnForeignKey foreignKey;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new DatabaseColumn object
        /// </summary>
        /// <param name="name">The name of the column</param>
        /// <param name="type">The datatype that needs to be stored into the column</param>
        public DatabaseColumn(string name, DatabaseColumnType type)
        {
            this.name = name;
            this.type = type;
            this.primaryKey = false;
            this.foreignKey = null;
        }

        public DatabaseColumn(string name, DatabaseColumnType type, bool primaryKey)
        {
            this.name = name;
            this.type = type;
            this.primaryKey = primaryKey;
            this.foreignKey = null;
        }

        public DatabaseColumn(string name, DatabaseColumnType type, DatabaseColumnForeignKey foreignkey)
        {
            this.name = name;
            this.type = type;
            this.foreignKey = foreignkey;
            this.primaryKey = false;
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
        public DatabaseColumnType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        #endregion

        #region "Methods"

        public override string ToString()
        {
            string __type = "";
            if (this.type.ToString().Contains("_"))
            {
                __type = this.type.ToString().Replace('_', '(') + ")";
            }
            else __type = this.type.ToString();

            string key = "";
            if (foreignKey != null)
            {
                key = foreignKey.ToString() + ",";
            }

            return this.name.ToUpper() + " " + __type + " " + (this.primaryKey ? "PRIMARY KEY" : "") + "," + key;
        }

        #endregion
    }
}
