using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    class OracleConnection : DatabaseConnection
    {
        #region "Fields"
        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"
        #endregion

        #region "Methods"

        public override bool OpenConnection(string server, int port)
        {
            return false;
        }

        public override bool IsConnected()
        {
            return false;
        }

        public override void CloseConnection()
        {
            return;
        }

        public override DataTable ExecuteSelect(string command)
        {
            return null;
        }

        public override bool ExecuteOther(string command)
        {
            return false;
        }

        #endregion
    }
}
