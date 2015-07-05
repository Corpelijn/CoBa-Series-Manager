using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    class DatabaseManager
    {
        #region "Fields"

        private string serverip;
        private int serverport;
        private DatabaseConnectionType databaseType;
        private DatabaseConnection connection;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new DatabaseManager object
        /// </summary>
        /// <param name="ip">The ip address of the server to connect to. Cannot be null</param>
        /// <param name="port">The port on the database server where the database is located. Must be bigger than 0</param>
        /// <param name="database">The type of database that is running on the server. Cannot be null</param>
        public DatabaseManager(string ip, int port, DatabaseConnectionType database)
        {
            this.serverip = ip;
            this.serverport = port;
            this.databaseType = database;
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Returns the IP address of the server
        /// If the value is set, the connection will be closed and you need to reopen it again
        /// </summary>
        public string ServerIP
        {
            get { return this.serverip; }
            set { this.serverip = value; }
        }

        /// <summary>
        /// Returns the port of the server connected to the database
        /// If the value is set, the connection will be closed and you need to reopen it again
        /// </summary>
        public int ServerPort
        {
            get { return this.serverport; }
            set { this.serverport = value; }
        }

        /// <summary>
        /// Returns the type op database hosted on the server
        /// If the value is set, the connection will be closed and you need to reopen it again
        /// </summary>
        public DatabaseConnectionType DatabaseType
        {
            get { return this.databaseType; }
            set { this.databaseType = value; }
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Opens the connection to the server and the database hosted on it
        /// </summary>
        /// <returns>True when the connection if opened, false if the connection failed</returns>
        public bool OpenConnection()
        {
            return false;
        }

        /// <summary>
        /// Checks if the connection to the server is opened
        /// </summary>
        /// <returns>True when the connection is open, false if the connection is closed</returns>
        public bool IsConnected()
        {
            return false;
        }

        /// <summary>
        /// Closes the connection to the server and database
        /// </summary>
        /// <returns>True if the connection closed, false if the closing failed</returns>
        public bool CloseConnection()
        {
            return false;
        }

        /// <summary>
        /// Creates a new table inside the database
        /// </summary>
        /// <param name="name">The name of the new table. Cannot be null</param>
        /// <param name="columns">The columns that need to be created inside the new table. Cannot be empty or null</param>
        /// <returns>True if the table was created, false if creating failed</returns>
        public bool CreateTable(string name, DatabaseColumn[] columns)
        {
            return false;
        }

        /// <summary>
        /// Deletes the specified table from the database
        /// </summary>
        /// <param name="name">The name of the table to delete. Cannot be null or empty</param>
        /// <returns>True if the table is deleted, false if the operation failed</returns>
        public bool DeleteTable(string name)
        {
            return false;
        }

        /// <summary>
        /// Execute a query command towards the database
        /// </summary>
        /// <param name="command">The query command to execute. Cannot be empty or null</param>
        /// <returns>A DataTable object containing the requested information</returns>
        public DataTable ExecuteQuery(string command)
        {
            return null;
        }

        /// <summary>
        /// Execute a command towards the database
        /// </summary>
        /// <param name="command">The command to execute. Cannot be empty or null</param>
        /// <returns>True if the command executed succesfully, false if it failed</returns>
        public bool ExecuteNonQuery(string command)
        {
            return false;
        }

        /// <summary>
        /// Reads the data from the database and returns it into a Administration class 
        /// </summary>
        /// <returns>An Administration class object containing the data from the database</returns>
        public Administration ReadData()
        {
            return null;
        }

        #endregion
    }
}
