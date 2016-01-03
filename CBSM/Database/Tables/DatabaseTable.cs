using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database.Tables
{
    class DatabaseTable
    {
        private string name;
        private List<DatabaseColumn> columns;

        public DatabaseTable(string name)
        {
            this.name = name;
            this.columns = new List<DatabaseColumn>();
        }

        public void AddColumn(DatabaseColumn column)
        {
            this.columns.Add(column);
        }
    }
}
