using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    public class FK
    {
        private string table;
        private string column;
        private string internalcolumn;

        public FK(string internalcolumn, string destinationtable, string destinationcolumn)
        {
            this.internalcolumn = internalcolumn;
            this.table = destinationtable;
            this.column = destinationcolumn;
        }

        public string Table
        {
            get { return table; }
            set { table = value; }
        }

        public string Column
        {
            get { return column; }
            set { column = value; }
        }

        public string InternalColumn
        {
            get { return internalcolumn; }
            set { internalcolumn = value; }
        }
    }
}
