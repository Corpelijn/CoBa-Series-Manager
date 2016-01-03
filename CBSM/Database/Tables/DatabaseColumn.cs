using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database.Tables
{
    class DatabaseColumn
    {
        #region "Fields"

        private string name;
        private string type;
        private int data_length;
        private bool primaryKey;
        private DatabaseColumnForeignKey foreignKey;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new DatabaseColumn object
        /// </summary>
        /// <param name="name">The name of the column</param>
        /// <param name="type">The datatype that needs to be stored into the column</param>
        public DatabaseColumn(string name, string type, int length)
        {
            this.name = name;
            this.type = type;
            this.data_length = length;
            this.primaryKey = false;
            this.foreignKey = null;
        }

        public DatabaseColumn(string name, string type, int length, bool primaryKey)
        {
            this.name = name;
            this.type = type;
            this.data_length = length;
            this.primaryKey = primaryKey;
            this.foreignKey = null;
        }

        public DatabaseColumn(string name, string type, int length, DatabaseColumnForeignKey foreignkey)
        {
            this.name = name;
            this.type = type;
            this.data_length = length;
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
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        #endregion

        #region "Methods"

        public override string ToString()
        {
            string __type = "";
            if (this.type.Contains("_"))
            {
                __type = this.type + (data_length != 0 ? "(" + data_length + ")" : "");
            }
            else __type = this.type;

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
