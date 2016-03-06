using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CBSM.Database
{
    class MySQLConnection : IDatabaseConnection
    {
        #region "Attributes"

        private MySqlConnection connection;
        private string databasename;

        #endregion

        #region "Constructor"

        public MySQLConnection(string hostname, string database, string username, string password, int port = -1)
        {
            this.databasename = database;
            OpenConnection(hostname, database, username, password);
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Returns the name of the database
        /// </summary>
        /// <returns>The name of the database</returns>
        public string GetDatabasename()
        {
            return this.databasename;
        }

        /// <summary>
        /// Opens a new connection to the database
        /// </summary>
        /// <param name="hostname">The servername or ip address of the server</param>
        /// <param name="database">The name of the database to connect to</param>
        /// <param name="username">The username of to connect with</param>
        /// <param name="password">The password of the username to connect with</param>
        /// <param name="port">The port of the database</param>
        /// <returns>Return true if the databaseconnection is open; otherwise false</returns>
        public bool OpenConnection(string hostname, string database, string username, string password, int port = -1)
        {
            if (connection != null)
                this.CloseConnection();

            string s = "Server=" + hostname + (port == -1 ? "" : ";Port=" + port) + ";User=" + username + ";Database=" + database + ";Password=" + password;
            try
            {
                connection = new MySqlConnection(s);
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return this.IsOpen();
        }

        public void InitializeConnection()
        {
        }

        /// <summary>
        /// Close the connection to the database
        /// </summary>
        public void CloseConnection()
        {
            this.connection.Close();
        }

        /// <summary>
        /// Check if the connection to the database is open
        /// </summary>
        /// <returns>Return true if the database is open; otherwise false</returns>
        public bool IsOpen()
        {
            if (this.connection == null)
                return false;
            else
                return this.connection.State == System.Data.ConnectionState.Open;
        }

        /// <summary>
        /// Executes a query command on the database
        /// </summary>
        /// <param name="query">The query to run on the database</param>
        /// <returns>Return the data found with the query</returns>
        public DataTable ExecuteQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable(reader);
            reader.Dispose();
            command.Dispose();
            return dt;
        }

        /// <summary>
        /// Executes a non query instruction on the database
        /// </summary>
        /// <param name="query">The instruction to run</param>
        public void ExecuteNonQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public int ExecuteInsert(string query)
        {
            MySqlCommand command = new MySqlCommand(query + "; select last_insert_id();", connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void CreateTable(Type type)
        {
            StringBuilder command = new StringBuilder();
            command.Append("create table ").Append(type.Name).Append(" (\n");
            int instructionLength = command.Length;

            foreach (FieldInfo fi in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fi.Name == "id")
                {
                    command = command.Insert(instructionLength, "id\tint(11)\tauto_increment primary key,\n");
                }
                else if (typeof(IList).IsAssignableFrom(fi.FieldType))
                {
                    // Do not place the column into the table. It is a reference to a link table

                    //CreateLinkTable(new string[] { type.Name, fi.FieldType.Name });
                }
                else
                {
                    command.Append(fi.Name).Append(" \t").Append(GetFieldType(fi)).Append(",\n");
                    if (fi.FieldType.IsSubclassOf(typeof(DBMS)))
                    {
                        command.Append("constraint table_").Append(type.Name).Append("\tforeign key (").Append(fi.Name).Append(")\treferences ").Append(fi.FieldType.Name).Append("(id),\n");
                    }
                }
            }

            command.Remove(command.Length - 2, 2);
            command.Append(")");

            Console.WriteLine(command.ToString());
            this.ExecuteNonQuery(command.ToString());
        }

        public string CreateLinkTable(string table1, string table2, string columnname)
        {
            StringBuilder command = new StringBuilder();
            command.Append("create table ");

            int nextLinkNum = Convert.ToInt32(DatabaseSettings.GetValue(DBSettings.NextLinkNumber));
            string fk_name_1 = (nextLinkNum + 1).ToString("000000");
            string fk_name_2 = (nextLinkNum + 2).ToString("000000");
            string tablename = "link_" + nextLinkNum.ToString("000000");

            command.Append(tablename);

            command.Append(" (\n");
            command.Append("id\tint(11)\tauto_increment primary key,\n");

            command.Append(table1.ToLower()).Append("_id\tint(11),\n");
            command.Append("constraint fk_").Append(fk_name_1).Append("\t FOREIGN KEY (" + table1 + "_id)\tREFERENCES " + table1 + "(id),\n");
            command.Append(table2.ToLower()).Append("_id\tint(11),\n");
            command.Append("constraint fk_").Append(fk_name_2).Append("\t FOREIGN KEY (" + table2 + "_id)\tREFERENCES " + table2 + "(id),\n");

            command = command.Remove(command.Length - 2, 1).Append(")");

            Console.WriteLine(command.ToString());
            this.ExecuteNonQuery(command.ToString());

            DatabaseSettings.SetValue(DBSettings.NextLinkNumber, (nextLinkNum + 3).ToString());
            DatabaseSettings.SetTable(table1, table2, columnname, tablename);

            return tablename;
        }

        /// <summary>
        /// Creates a new table in the database
        /// </summary>
        /// <param name="data">A reference to an object the create the table with</param>
        //public void CreateTable(object data)
        //{
        //    string command = "create table " + data.GetType().Name + " (\n";
        //    bool hasPK = false;
        //    foreach (FieldInfo fi in data.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
        //    {
        //        // Ignore 'pk' field
        //        if (fi.Name == "pk")
        //            continue;

        //        command += fi.Name + "\t";
        //        command += GetFieldType(fi, fi.GetValue(data));

        //        if (fi.Name == ((DBMS)data).GetPrimaryKey())
        //        {
        //            command += "\t" + (fi.FieldType.Name.StartsWith("Int") || fi.FieldType.Name == "Float" || fi.FieldType.Name == "Double" ? "auto_increment " : "") + " primary key";
        //            hasPK = true;
        //        }

        //        command += ",\n";
        //    }
        //    command = command.Remove(command.Length - 2);
        //    command += ")";

        //    if (!hasPK)
        //    {
        //        throw new Exception("The table has no primary key. Declare an attribute \'id\' or define a custom primary key");
        //    }

        //    Console.WriteLine(command);
        //    this.ExecuteNonQuery(command);
        //}

        public bool DoesTableExists(Type type)
        {
            DataTable dt = this.ExecuteQuery("select table_name from information_schema.tables where table_schema=\'" + this.GetDatabasename() + "\' and table_name=\'" + type.Name + "\';");

            return dt.GetRowCount() > 0;
        }

        public bool DoesTableExists(string tablename)
        {
            DataTable dt = this.ExecuteQuery("select table_name from information_schema.tables where table_schema=\'" + this.GetDatabasename() + "\' and table_name=\'" + tablename + "\';");

            return dt.GetRowCount() > 0;
        }

        public void UpdateTable(object data)
        {
            DataTable table = this.ExecuteQuery("SHOW COLUMNS FROM " + data.GetType().Name);
            foreach (DataRow row in table)
            {
                string field = row.GetObject("Field").ToString();
                string type = row.GetObject("Type").ToString();
                string key = row.GetObject("Key").ToString();
                string extra = row.GetObject("Extra").ToString();

                if (key == "PRI" && field != ((DBMS)data).GetPrimaryKey())
                {
                    throw new Exception("You cannot just simply change the primary key of a table. Learn something about databases you f*ck!");
                }

                foreach (FieldInfo fi in data.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    // Ignore 'pk' field
                    if (fi.Name == "pk")
                        continue;

                    if (fi.Name == field)
                    {
                        if (type.StartsWith("varchar("))
                        {
                            type = type.Remove(0, 8);
                            int t = Convert.ToInt32(type.Remove(type.Length - 1));
                            string val = (string)fi.GetValue(data);
                            if (t < val.Length)
                            {
                                // table needs to be altered
                                this.ExecuteNonQuery("ALTER TABLE " + data.GetType().Name + " MODIFY COLUMN " + fi.Name + " varchar(" + val.Length + ")");
                                Console.WriteLine("alter " + field + " (" + t + " -> " + val.Length + ")");
                            }
                        }
                    }
                }
            }
        }

        //public string GetFieldType(System.Reflection.FieldInfo info, object data)
        //{
        //    switch (info.FieldType.Name)
        //    {
        //        case "String":
        //            return "varchar(1)";
        //        case "Int16":
        //        case "Int32":
        //            return "int(11)";
        //        case "Int64":
        //            return "bigint(22)";
        //        case "DateTime":
        //            return "date";
        //        case "Double":
        //        case "Float":
        //            return "decimal(9, 10)";
        //        default:
        //            // Check for a foreign key
        //            if (info.FieldType.IsSubclassOf(typeof(DBMS)))
        //            {
        //                // This is a foreign key. Return the type of the PK of the class
        //                DBMS col = (DBMS)data;
        //                return GetFieldType(col.GetType().GetField(col.GetPrimaryKey(), BindingFlags.NonPublic | BindingFlags.Instance), null);
        //            }
        //            return "int(11)";
        //    }
        //}

        public string GetFieldType(FieldInfo info)
        {
            switch (info.FieldType.Name)
            {
                case "String":
                    return "varchar(1)";
                case "Int16":
                case "Int32":
                    return "int(11)";
                case "Int64":
                    return "bigint(22)";
                case "DateTime":
                    return "date";
                case "Double":
                case "Float":
                    return "decimal(9, 10)";
                default:
                    // Check for a foreign key
                    if (info.FieldType.IsSubclassOf(typeof(DBMS)))
                    {
                        // This is a foreign key. Return the type of the PK of the class
                        return "int(11)";
                    }
                    return "blob";
            }
        }

        #endregion
    }
}
