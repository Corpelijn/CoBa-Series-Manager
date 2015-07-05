using CBSM.Database;
using CBSM.Domain;
using CBSM.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CBSM
{
    /// <summary>
    /// This class is the ONLY communication between the UI, database and the other classes. 
    /// This class forms a abstraction layer for error in the UI.
    /// </summary>
    public class Administration
    {
        #region "Fields"

        private List<Serie> series;
        private List<Person> people;

        private DatabaseManager database;

        #endregion

        #region "Constructors"

        public Administration()
        {
            this.series = new List<Serie>();
            this.people = new List<Person>();
        }

        #endregion

        #region "Properties"
        #endregion

        #region Private Methods"

        private void CopyInformation(Administration administration)
        {
            // Copy the data from the given administration class into the current administation class
            this.series = administration.series;
            this.people = administration.people;
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// Closes the database connection.
        /// If the connection closed without an error, the return value is true.
        /// If the closing of the connection caused an error, the return value is false.
        /// If there is no connection active, the return value is false
        /// </summary>
        /// <returns>True if the connection was closed, otherwise false</returns>
        public bool CloseDatabaseConnection()
        {
            if (database == null)
            {
                // There is no connection active, return
                return false;
            }

            // Close the database connection
            return database.CloseConnection();
        }

        /// <summary>
        /// Reads the database information into the administration class
        /// Returns true if the database information was read
        /// Returns false if the database connection is not active
        /// Return false if something went wrong during the reading
        /// </summary>
        /// <returns>True if the reading was successfull, false if reading the database failed</returns>
        public bool ImportDatabase()
        {
            // Check if the database connection is active
            if (database == null)
            {
                // Database connection is nog active, return
                return false;
            }

            // Read the information from the database
            try
            {
                this.CopyInformation(database.ReadData());
            }
            catch (Exception ex)
            {
                // Something went wrong during the reading of the data
                return false;
            }

            // Everything went okay, return
            return true;
        }

        /// <summary>
        /// Checks if the database is connected and ready to write/read
        /// </summary>
        /// <returns>True if the database is connected and ready, otherwise false</returns>
        public bool IsDatabaseConnected()
        {
            // Check if the database connection if active
            if (database == null)
            {
                // Database connection is not active, return
                return false;
            }
            return database.IsConnected();
        }

        /// <summary>
        /// Opens the connection to a database 
        /// Return true if the connection is set up
        /// Return false if the connection failed
        /// Return false if there is already a connection active
        /// </summary>
        /// <param name="ipaddress">IP address of the server of the database</param>
        /// <param name="port">Portnumber of the serverport that is hosting the database</param>
        /// <param name="databaseType">The type op database running on the server</param>
        /// <returns>True if the connection to the database was opened, otherwise false</returns>
        public bool OpenDatabaseConnection(string hostname, int port, Database.DatabaseConnectionType databaseType)
        {
            try
            {
                // Check if the hostname is a valid IP address or hostname
                IPHostEntry ipaddress = Dns.GetHostEntry(hostname);
                // Convert the ipaddress back to a string
                hostname = ipaddress.ToString();
            }
            catch (Exception)
            {
                throw new IllegalArgumentException("The system was not able to resolve the hostname");
            }

            // Check if the portnumber is bigger than 0
            if (port < 0)
            {
                throw new IllegalArgumentException("The portnumber is not valid");
            }

            // Check if there is an active database connection
            if (database != null)
            {
                // There is already a database connected, return
                return false;
            }

            // Create the connection to the database
            database = new DatabaseManager(hostname, port, databaseType);
            if (database != null)
            {
                // Open the connection to the database
                bool result = database.OpenConnection();
                if (!result)
                {
                    // Opening the database connection failed
                    return false;
                }
            }
            
            // For the idea of something might going wrong
            return database.IsConnected();
        }

        #endregion
    }
}
