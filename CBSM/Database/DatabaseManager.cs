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
        private string database;
        private string username;
        private string password;
        private DatabaseConnection connection;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new DatabaseManager object
        /// </summary>
        /// <param name="ip">The ip address of the server to connect to. Cannot be null</param>
        /// <param name="port">The port on the database server where the database is located. Must be bigger than 0</param>
        /// <param name="database">The type of database that is running on the server. Cannot be null</param>
        public DatabaseManager(string ip, int port, string database, DatabaseConnectionType type, string username, string password)
        {
            this.serverip = ip;
            this.serverport = port;
            this.databaseType = type;
            this.database = database;
            this.username = username;
            this.password = password;

            SetupDatabaseConnection();
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
            set
            {
                this.serverip = value;
                this.CloseConnection();
            }
        }

        /// <summary>
        /// Returns the port of the server connected to the database
        /// If the value is set, the connection will be closed and you need to reopen it again
        /// </summary>
        public int ServerPort
        {
            get { return this.serverport; }
            set
            {
                this.serverport = value;
                this.CloseConnection();
            }
        }

        /// <summary>
        /// Returns the type op database hosted on the server
        /// If the value is set, the connection will be closed and you need to reopen it again
        /// </summary>
        public DatabaseConnectionType DatabaseType
        {
            get { return this.databaseType; }
            set
            {
                this.databaseType = value;
                this.CloseConnection();
            }
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Opens the connection to the server and the database hosted on it
        /// </summary>
        /// <returns>True when the connection if opened, false if the connection failed</returns>
        public bool OpenConnection()
        {
            // Check if the connection object is created
            if (connection == null)
            {
                // The object is not created, create it
                SetupDatabaseConnection();
            }

            // Check if the current connection is active
            if (connection.IsConnected())
            {
                // The current connection is active, return
                return false;
            }

            // There is no connction active and the object to connect to is created
            return connection.OpenConnection(this.serverip, this.serverport, this.database, this.username, this.password);
        }

        /// <summary>
        /// Checks if the connection to the server is opened
        /// </summary>
        /// <returns>True when the connection is open, false if the connection is closed</returns>
        public bool IsConnected()
        {
            // Check if there is a connection object
            if (connection == null)
            {
                // There is no connection object, return
                return false;
            }

            return connection.IsConnected();
        }

        /// <summary>
        /// Closes the connection to the server and database
        /// </summary>
        /// <returns>True if the connection closed, false if the closing failed</returns>
        public bool CloseConnection()
        {
            // Check if the connection exists
            if (connection == null)
            {
                // The connection does not exists, return
                return false;
            }

            // Check if the connection is stil active
            if (!connection.IsConnected())
            {
                // The connection is already closed, return
                return false;
            }

            // Close the connection
            return connection.CloseConnection();
        }

        /// <summary>
        /// Creates a new table inside the database
        /// </summary>
        /// <param name="name">The name of the new table. Cannot be null</param>
        /// <param name="columns">The columns that need to be created inside the new table. Cannot be empty or null</param>
        /// <returns>True if the table was created, false if creating failed</returns>
        public bool CreateTable(string name, DatabaseColumn[] columns)
        {
            // Check if there is a connection to a database
            if (connection == null)
            {
                // There is no connection, return
                return false;
            }

            // Check if the connection is active
            if (!connection.IsConnected())
            {
                // The connection is not active, return
                return false;
            }

            // Put the input commands into a command
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE ").Append(name.ToUpper());
            sb.Append(" ( ");
            foreach (DatabaseColumn dc in columns)
            {
                //sb.Append(dc.Name).Append(" ").Append(dc.Type).Append(", ");
                sb.Append(dc.ToString());
            }
            sb = sb.Remove(sb.Length - 2, 2);
            sb.Append(")");

            // Convert the command into the syntax of the sepcified database
            string com = SyntaxConverter.Convert(sb.ToString(), this.databaseType);

            // Run the command agaist the database
            return connection.ExecuteNonQuery(com);
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
        public DataTable1 ExecuteQuery(string command)
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

        private void SetupDatabaseConnection()
        {
            // Check if there is a connection active
            if (connection != null)
            {
                // There is a connection active, return
                return;
            }

            // Check the type of database on the server and connect using the specified ip address and port
            if (databaseType == DatabaseConnectionType.MySQL)
            {
                connection = new MySQLConnection();
            }
            else
            {
                connection = new OracleConnection();
            }
        }

        private bool DoesTableExists(string table)
        {
            return this.connection.DoesTableExists(table.ToUpper());
        }

        public bool CheckDatabaseTables()
        {
            bool result = true;
            if (!this.DoesTableExists("serie")) result = false;
            if (!this.DoesTableExists("seizoen")) result = false;
            if (!this.DoesTableExists("seizoen_per_serie")) result = false;
            return result;
        }

        public bool CreateMissingDatabaseTables()
        {
            if (!this.DoesTableExists("SERIE"))
            {
                if (!this.CreateTable("SERIE", new DatabaseColumn[] {
                        new DatabaseColumn("ID", DatabaseColumnType.NUMBER, true),
                        new DatabaseColumn("NAME", DatabaseColumnType.VARCHAR2_100)
                })) return false;
                Console.WriteLine("SERIE TABLE CREATED");
            }

            if (!this.DoesTableExists("SEIZOEN"))
            {
                if (!this.CreateTable("SEIZOEN", new DatabaseColumn[] {
                        new DatabaseColumn("ID", DatabaseColumnType.NUMBER, true),
                        new DatabaseColumn("NR", DatabaseColumnType.NUMBER),
                        new DatabaseColumn("VERHAAL", DatabaseColumnType.VARCHAR2_500)
                })) return false;
                Console.WriteLine("SEIZOEN TABLE CREATED");
            }

            if(!this.DoesTableExists("SEIZOEN_PER_SERIE"))
            {
                if(!this.CreateTable("SEIZOEN_PER_SERIE", new DatabaseColumn[] {
                        new DatabaseColumn("ID", DatabaseColumnType.NUMBER, true),
                        new DatabaseColumn("SERIE_ID", DatabaseColumnType.NUMBER, new DatabaseColumnForeignKey("FK_SPS_SERIE_ID", "SERIE_ID", "SERIE", "ID")),
                        new DatabaseColumn("SEIZOEN_ID", DatabaseColumnType.NUMBER, new DatabaseColumnForeignKey("FK_SPS_SEIZOEN_ID", "SEIZOEN_ID", "SEIZOEN", "ID"))
                })) return false;
                Console.WriteLine("SEIZOEN_PER_SERIE TABLE CREATED");
            }

            return true;
        }

        #endregion
    }
}
