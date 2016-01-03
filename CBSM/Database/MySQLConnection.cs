using CBSM.Database.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CBSM.Database
{
    class MySQLConnection : DatabaseConnection
    {
        #region "Fields"
        #endregion

        #region "Constructors

        public MySQLConnection()
        {

        }

        #endregion

        #region "Properties"
        #endregion

        #region "Methods"

        public override bool OpenConnection(string server, int port, string database, string username, string password)
        {
            return false;
        }

        public override bool IsConnected()
        {
            return false;
        }

        public override bool CloseConnection()
        {
            return false;
        }

        public override DataTable ExecuteQuery(string command)
        {
            return null;
        }

        public override bool ExecuteNonQuery(string command)
        {
            return false;
        }

        public override bool DoesTableExist(string table)
        {
            throw new NotImplementedException();
        }

        public override DatabaseColumn DoesColumnExist(string table, string column)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
