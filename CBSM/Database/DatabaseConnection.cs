using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    abstract class DatabaseConnection
    {
        #region "Fields"
        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"
        #endregion

        #region "Methods"

        /// <summary>
        /// Opens the connection to the database
        /// </summary>
        /// <param name="server">The ip address of the server to connect to. Cannot be null</param>
        /// <param name="port">The port of the server to connect to. Must be bigger than 0</param>
        /// <returns>True if the connection is opened, false if it failed</returns>
        public abstract bool OpenConnection(string server, int port);

        /// <summary>
        /// Checks if the connection is still open and active
        /// </summary>
        /// <returns>True if the connecion is open, otherwise false</returns>
        public abstract bool IsConnected();

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public abstract void CloseConnection();

        /// <summary>
        /// Executes a query command to the database
        /// </summary>
        /// <param name="command">The command to execute. Cannot be null or empty</param>
        /// <returns>A DataTable object containing the requested data</returns>
        public abstract DataTable ExecuteQuery(string command);

        /// <summary>
        /// Executes a command to the database
        /// </summary>
        /// <param name="command">The command to execute. Cannot be null or empty</param>
        /// <returns>True if the command was successully executed, false otherwise</returns>
        public abstract bool ExecuteNonQuery(string command);

        #endregion
    }
}
