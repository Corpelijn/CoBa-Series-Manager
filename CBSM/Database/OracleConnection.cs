using CBSM.Database.Tables;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CBSM.Database
{
    class OracleConnection : DatabaseConnection
    {
        #region "Fields"

        private Oracle.DataAccess.Client.OracleConnection connection;
        private string username;

        #endregion

        #region "Constructors"

        public OracleConnection()
        {
        }

        #endregion

        #region "Properties"
        #endregion

        #region "Methods"

        public override bool OpenConnection(string server, int port, string database, string username, string password)
        {
            this.username = username;

            string connectionString = "";
            if (username != "")
            {
                connectionString += "user id=" + username + ";";
            }
            if (password != "")
            {
                connectionString += "password=" + password + ";";
            }
            connectionString += "data source=//" + server;
            if (port > 0)
            {
                connectionString += ":" + port.ToString();
            }
            connectionString += "/" + database;

            this.connection = new Oracle.DataAccess.Client.OracleConnection(connectionString);
            this.connection.Open();

            return this.connection.State == System.Data.ConnectionState.Open;
        }

        public override bool IsConnected()
        {
            if (this.connection == null)
            {
                return false;
            }

            if (this.connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }

            return false;
        }

        public override bool CloseConnection()
        {
            return false;
        }

        public override DataTable ExecuteQuery(string command)
        {
            OracleCommand oracleCommand = new OracleCommand(command, this.connection);
            OracleDataReader reader = null;
            try
            {
                reader = oracleCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            DataTable table = new DataTable();
            if(reader != null)
                table.Load(reader);

            return table;
        }

        public override bool ExecuteNonQuery(string command)
        {
            OracleCommand oracleCommand = new OracleCommand(command, this.connection);
            try
            {
                oracleCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public override bool DoesTableExist(string table)
        {
            return ExecuteQuery("select table_name from all_tables where owner = \'" + username.ToUpper() + "\' and table_name = \'" + table + "\'").Rows.Count > 0;
        }

        public override DatabaseColumn DoesColumnExist(string table, string column)
        {
            DataTable dt = ExecuteQuery("SELECT table_name, column_name, data_type, data_length, nullable FROM USER_TAB_COLUMNS WHERE table_name=\'" + table + "\' AND column_name=\'" + column + "\'");
            if (dt.Rows.Count == 1)
            {
                //dt.
            }
            return null;
        }

        #endregion
    }
}
