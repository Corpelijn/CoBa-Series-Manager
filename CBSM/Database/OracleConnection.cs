﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    class OracleConnection : DatabaseConnection
    {
        #region "Fields"
        #endregion

        #region "Constructors"

        public OracleConnection()
        {
        }

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

        public override bool CloseConnection()
        {
            return false; ;
        }

        public override DataTable ExecuteQuery(string command)
        {
            return null;
        }

        public override bool ExecuteNonQuery(string command)
        {
            return false;
        }

        #endregion
    }
}
