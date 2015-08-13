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

        private DatabaseManager databaseManager;

        #endregion

        #region "Constructors"

        public Administration()
        {
            this.series = new List<Serie>();
            this.people = new List<Person>();
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// The Singleton instance for this class
        /// </summary>
        public static Administration Instance { get; set; }

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

        #region "Database"

        /// <summary>
        /// Closes the database connection.
        /// If the connection closed without an error, the return value is true.
        /// If the closing of the connection caused an error, the return value is false.
        /// If there is no connection active, the return value is false
        /// </summary>
        /// <returns>True if the connection was closed, otherwise false</returns>
        public bool CloseDatabaseConnection()
        {
            if (databaseManager == null)
            {
                // There is no connection active, return
                return false;
            }

            // Close the database connection
            return databaseManager.CloseConnection();
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
            if (databaseManager == null)
            {
                // Database connection is nog active, return
                return false;
            }

            // Read the information from the database
            try
            {
                this.CopyInformation(databaseManager.ReadData());
            }
            catch (Exception)
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
            if (databaseManager == null)
            {
                // Database connection is not active, return
                return false;
            }
            return databaseManager.IsConnected();
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
        public bool OpenDatabaseConnection(string server, int port, string database, DatabaseConnectionType type, string username = "", string password = "")
        {
            try
            {
                // Check if the hostname is a valid IP address or hostname
                IPHostEntry ipaddress = Dns.GetHostEntry(server);
                // Convert the ipaddress back to a string
                server = ipaddress.HostName;
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
            if (databaseManager != null)
            {
                // There is already a database connected, return
                return false;
            }

            // Create the connection to the database
            databaseManager = new DatabaseManager(server, port, database, type, username, password);
            if (databaseManager != null)
            {
                // Open the connection to the database
                bool result = databaseManager.OpenConnection();
                if (!result)
                {
                    // Opening the database connection failed
                    return false;
                }
            }
            
            // For the idea of something might going wrong
            return databaseManager.IsConnected();
        }

        public bool CheckDatabaseTables()
        {
            return this.databaseManager.CheckDatabaseTables();
        }

        public bool CreateMissingDatabaseTables()
        {
            return this.databaseManager.CreateMissingDatabaseTables();
        }

        #endregion

        #region "Serie"

        /// <summary>
        /// Create a new serie
        /// </summary>
        /// <param name="name">The name of the new serie to create, must be unique</param>
        /// <param name="codePrefix">The code prefix for generation unique episode codes, must be unique</param>
        /// <returns>True if the creation succeded, otherwise false</returns>
        public bool CreateSerie(string name, string codePrefix)
        {
            return false;
        }

        /// <summary>
        /// Find a serie by the combination of the name and code prefix or one of both and delete it
        /// </summary>
        /// <param name="name">The name of the serie to remove, can be null if the codePrefix is set</param>
        /// <param name="codePrefix">The code prefix of the serie to remove, can be null if the name is set</param>
        /// <returns></returns>
        public bool DeleteSerie(string name = "", string codePrefix = "")
        {
            return false;
        }

        /// <summary>
        /// Create a new season and place it into the specified serie.
        /// The number of the season will be the next avaiable number in the database
        /// </summary>
        /// <param name="story">A summary story about the season</param>
        /// <param name="seriename">The name of the serie to place the season into</param>
        /// <returns>True if the season was added to the serie, otherwise false</returns>
        public bool AddSeason(string story, string seriename)
        {
            return false;
        }

        /// <summary>
        /// Create a new Season and place it into the specified serie.
        /// The number of the season can be specified in the first parameter
        /// </summary>
        /// <param name="nr">The number of the new season, must be unique and above 0</param>
        /// <param name="story">A summary story about the season</param>
        /// <param name="seriename">The bane of the serie to place the season into</param>
        /// <returns>True if the season was added to the serie, otherwise false</returns>
        public bool AddSeason(int nr, string story, string seriename)
        {
            return false;
        }

        /// <summary>
        /// Changes the information of a serie
        /// </summary>
        /// <param name="oldSeriename">The name of the old serie</param>
        /// <param name="newSeriename">The new name for the serie, must be unique</param>
        /// <returns>True if the information was updated, otherwise false</returns>
        public bool EditSerieInformation(string oldSeriename, string newSeriename, string codePrefix)
        {
            return false;
        }

        #endregion

        #region "Season"

        /// <summary>
        /// Checks the datbabase and returns the next avaiable number for a season
        /// </summary>
        /// <returns>The next number for a season</returns>
        public int GetNextSeasonNumber()
        {
            return -1;
        }

        /// <summary>
        /// Changes the information baout a season
        /// </summary>
        /// <param name="oldNr">The number of the season to change</param>
        /// <param name="newNr">A new number for the season, can be the same as the old number</param>
        /// <param name="story">A new summary story for the season, can be the same as the old number
        /// When a null value is found, the old sotry is kept</param>
        /// <returns>True if the information was succesfully updated, otherwise false</returns>
        public bool EditSeasonInformation(int oldNr, int newNr, string story)
        {
            return false;
        }

        /// <summary>
        /// Moves the season from a serie to another serie
        /// </summary>
        /// <param name="seasonNr">The seasonnumber of the season</param>
        /// <param name="newSeriename">The name of the serie to place the season into</param>
        /// <returns></returns>
        public bool MoveSeason(int seasonNr, string newSeriename)
        {
            return false;
        }

        /// <summary>
        /// Creates a new episode and places it into the specified season. 
        /// The number of the season will be the next avaiable number in the database
        /// </summary>
        /// <param name="title">The title of the episode</param>
        /// <param name="type">The type of the episode</param>
        /// <param name="notes">Notes to keep for the episode</param>
        /// <param name="seasonnumber">The number of the season to place the episode into</param>
        /// <returns>True if the episode was created, otherwise false</returns>
        public bool AddEpisode(string title, string type, string notes, int seasonnumber)
        {
            return false;
        }

        /// <summary>
        /// Creates a new episode and place it into the specified season.
        /// The number of the season can be specified in the nr paramter
        /// </summary>
        /// <param name="nr">The number of the episode, mustbe unique and bigger than 0</param>
        /// <param name="title">The title of the episode</param>
        /// <param name="type">The type of the episode</param>
        /// <param name="notes">Notes to keep for the episode</param>
        /// <param name="seasonnumber">The number of the season to place the episode into</param>
        /// <returns>True if the episode was created, otherwise false</returns>
        public bool AddEpisode(int nr, string title, string type, string notes, int seasonnumber)
        {
            return false;
        }

        #endregion

        #region "Episode"

        /// <summary>
        /// Edits the information in the episode
        /// </summary>
        /// <param name="oldEpisodeNr">The old number of the episode</param>
        /// <param name="newEpisodeNr">The new number of the episode, can be the same as the old number</param>
        /// <param name="title">The new title of the episode</param>
        /// <param name="type">The new type of the episode</param>
        /// <param name="notes">The new notes for the episode</param>
        /// <returns>True if the information was updated succesfully, otherwise false</returns>
        public bool EditEpisodeInformation(int oldEpisodeNr, int newEpisodeNr, string title, string type, string notes)
        {
            return false;
        }

        /// <summary>
        /// Checks the database and returns the next avaiable number for an episode
        /// </summary>
        /// <returns>The next avaiable number in the database for an episode</returns>
        public int GetNextEpisodeNumber()
        {
            return -1;
        }

        /// <summary>
        /// Ads a staff member to the episode
        /// </summary>
        /// <param name="accountnaam">The accountname of the person to add</param>
        /// <param name="role">The role this person fullfilled in the episode</param>
        /// <param name="episodeNr">The number of the episode to store the information into</param>
        /// <returns>True if the staff member was added to the episode, otherwise false</returns>
        public bool AddStaffToEpisode(string accountnaam, string role, int episodeNr)
        {
            return false;
        }

        /// <summary>
        /// Adds a new scene to a episode.
        /// The number of the scene will be the next avaiable number in the database
        /// </summary>
        /// <param name="locationid">The ID of the location at wich the scene takes place</param>
        /// <param name="time">The time inside the scene</param>
        /// <param name="mood">The mood of the scene</param>
        /// <param name="description">A short description of the scene</param>
        /// <param name="aproxSceneDuration">The aproximated duration of the scene</param>
        /// <param name="priority">the priority of the scene <??></param>
        /// <returns>True if the scene was created succesfully into the episode, otherwise false</returns>
        public bool AddScene(int locationid, DateTime time, string mood, string description, TimeSpan aproxSceneDuration,
            int priority)
        {
            return false;
        }

        /// <summary>
        /// Adds a new scene to a episode.
        /// The number of the scene can be specified in the first parameter
        /// </summary>
        /// <param name="nr">The number of the scene, must be unique and above 0</param>
        /// <param name="locationid">The ID of the location at wich the scene takes place</param>
        /// <param name="time">The time inside the scene</param>
        /// <param name="mood">The mood of the scene</param>
        /// <param name="description">A short description of the scene</param>
        /// <param name="aproxSceneDuration">The aproximated duration of the scene</param>
        /// <param name="priority">the priority of the scene <??></param>
        /// <returns>True if the scene was created succesfully into the episode, otherwise false</returns>
        public bool AddScene(int nr, int locationid, DateTime time, string mood, string description, TimeSpan aproxSceneDuration,
            int priority)
        {
            return false;
        }

        #endregion

        #region "Scene"

        /// <summary>
        /// Add a attribute to use to the scene 
        /// </summary>
        /// <param name="description">A description of the attribute to use</param>
        /// <param name="scenenr">The id of the scene to add the attribute to</param>
        /// <returns></returns>
        public bool AddAttributeToScene(string description, int scenenr)
        {
            return false;
        }

        /// <summary>
        /// Add staff personal to the scene
        /// </summary>
        /// <param name="accountname">The accountname of the person to add</param>
        /// <param name="role">The role this person has fulfilled</param>
        /// <param name="scenenr">The number of the scene in wich this person was active</param>
        /// <returns></returns>
        public bool AddStaffToScene(int accountname, string role, int scenenr)
        {
            return false;
        }

        /// <summary>
        /// Adds a character to the scene
        /// </summary>
        /// <param name="scenenr">The number of the scene to add the character to</param>
        /// <param name="characternr">The number of the character to add to the scene</param>
        /// <returns>True if the character was added to the scene, otherwise false</returns>
        public bool AddCharactersToScene(int scenenr, int characternr)
        {
            return false;
        }

        #endregion

        #region "Character"

        /// <summary>
        /// Creates a character to be used in a scene
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <param name="description">A description about the character and it's behaviour</param>
        /// <param name="notes">Other notes regarding the character</param>
        /// <param name="accountname">The accountname of the person who portrays this character</param>
        /// <returns></returns>
        public bool CreateCharacter(string name, string description, string notes, string accountname)
        {
            return false;
        }

        #endregion

        #endregion
    }
}
