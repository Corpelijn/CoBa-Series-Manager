using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database.Tables
{
    class DatabaseColumnForeignKey
    {
        private string name;
        private string foreignKey;
        private string referenceTable;
        private string referenceColumn;

        public DatabaseColumnForeignKey(string name, string foreignKey, string referenceTable, string referenceColumn)
        {
            this.name = name;
            this.foreignKey = foreignKey;
            this.referenceTable = referenceTable;
            this.referenceColumn = referenceColumn;
        }

        public override string ToString()
        {
            return "CONSTRAINT " + name + " FOREIGN KEY (" + foreignKey + ") REFERENCES " + referenceTable + "(" + referenceColumn + ") ";
        }
    }
}
