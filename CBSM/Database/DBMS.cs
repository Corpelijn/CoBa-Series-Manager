using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CBSM.Database
{
    public class DBMS
    {
        #region "Attributes"

        /// <summary>
        /// The primary key of the table
        /// </summary>
        protected int id;

        #endregion

        #region "Contructor"

        /// <summary>
        /// Creates a new instance of the DBMS class. This class is needed for creating a table in the database
        /// </summary>
        public DBMS()
        {
            this.id = -1;
            DatabaseManager.GetInstance().AddTable(this);
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets the name of the table that will be placed in the database
        /// </summary>
        /// <returns>Returns the name of the table in the database</returns>
        public string GetTableName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Writes the object to the corresponding table in the database
        /// </summary>
        public void WriteToDatabase()
        {
            DatabaseManager.GetInstance().WriteObject(this);
        }

        /// <summary>
        /// Gets the name of the primary key of this class.
        /// </summary>
        /// <returns>Returns the primary key of this table</returns>
        public string GetPrimaryKey()
        {
            return "id";
        }

        /// <summary>
        /// Gets the values that the current primary key is holding
        /// </summary>
        /// <returns>Returns the value of the current primary key</returns>
        public object GetPrimaryKeyValue()
        {
            return this.GetType().GetField(GetPrimaryKey(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        }

        public void SetInaccessible()
        {
            this.id = this.id == -1 ? -2 : this.id;
        }

        public void SetAccessible()
        {
            this.id = this.id == -2 ? -1 : this.id;
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Writes the class and it's information to a single string
        /// </summary>
        /// <returns>Return the class and information in a single string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(this.GetType().Name + " : ");

            Type[] t = this.GetType().GetNestedTypes(BindingFlags.Public);

            foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // Add an exception for the 'pk' attribute
                if (fi.Name == "pk")
                {
                    // Continue with the next value
                    continue;
                }

                // Append the name of the next field
                sb.Append(fi.Name);
                // Append an equal to sign
                sb.Append("=");
                // Append the value in the current instance of the class
                sb.Append(fi.GetValue(this));
                // Append a comma sign
                sb.Append(", ");
            }

            sb = sb.Remove(sb.Length - 2,2);

            sb.Append("}");
            return sb.ToString();
        }

        #endregion
    }

    public class DBMS<T> : DBMS where T : new()
    {
        /// <summary>
        /// Gets an object from the database from the corresponding table. The record that is wanted to be read is defined by the constraints
        /// </summary>
        /// <param name="constraints">The constraints where the record has to correspond to</param>
        /// <returns>Returns the corresponding data from the table</returns>
        public static T GetFromDatabase(params string[] constraints)
        {
            // Instantiate a new value of type T
            T value = new T();

            // Create a select query from the constraints
            string table = value.GetType().Name;
            string query = "select * from " + table + " where ";
            StringBuilder where_clause = new StringBuilder();

            foreach (string constraint in constraints)
            {
                where_clause.Append(constraint + " and ");
            }

            // Remove the last ' and ' from the instruction
            where_clause = where_clause.Remove(where_clause.Length - 5, 5);
            // Execute the command to the database
            DataTable dt = DatabaseManager.GetInstance().ExecuteQuery(query + where_clause);

            // Check if the database has returned a single record
            if (dt.GetRowCount() == 1)
            {
                // Get the record from the database
                DataRow row = dt.GetDataRow(0);

                // Loop through the columns returned by the database
                for (int i = 0; i < row.GetFieldCount(); i++)
                {
                    // Get the type of the class (typeof(T))
                    Type type = value.GetType();

                    // Get the information from a field. The name of the field should be the same as the name of the column
                    FieldInfo fi = type.GetField(row.GetField(i), BindingFlags.NonPublic | BindingFlags.Instance);
                    
                    // Check if the attribute of the class equals to a DBMS type. If so the record is a foreign key
                    if (fi.FieldType.IsSubclassOf(typeof(DBMS)))
                    {
                        // The field is a foreign key

                        // Get the data of the single object from the database by calling the 'GetFromDatabase' method
                        MethodInfo database = fi.FieldType.BaseType.GetMethod("GetFromDatabase", BindingFlags.Public | BindingFlags.Static);

                        // Define a constraint to find a single record
                        string constraint = DatabaseManager.GetInstance().GetPrimaryKey(value.GetType().Name) + "=" + row.GetObject(row.GetField(i));
                        
                        // Get the data object from the database
                        object fk = database.Invoke(null, new object[] { new string[] { constraint } });

                        // Set the received value to the correct field
                        fi.SetValue(value, fk);
                    }
                    else
                    {
                        // The field is a normal value, set the data to the object
                        fi.SetValue(value, row.GetObject(row.GetField(i)));
                    }
                }
            }
            // Triggers when the database return more than one recor
            else if (dt.GetRowCount() > 0)
            {
                query = "";
                foreach (string constraint in constraints)
                {
                    query += constraint + " and ";
                }
                query = query.Remove(query.Length - 5);
                throw new Exception("To many results found with the specified constraints\nGet: " + table + "-object where " + query);
            }
            // Triggers when the database return no records
            else
            {
                query = "";
                foreach (string constraint in constraints)
                {
                    query += constraint + " and ";
                }
                query = query.Remove(query.Length - 5);
                throw new Exception("There is no record found with the specified constraints\nGet: " + table + "-object where " + query);
            }

            // Return the new value with the correct values
            return value;
        }

        public static T[] GetMutipleFromDatabase(params string[] constraints)
        {
            // Instantiate a new value of type T
            List<T> ret_val = new List<T>();

            // Create a select query from the constraints
            string table = ret_val.GetType().GetGenericArguments()[0].Name;
            string query = "select * from " + table + " where ";
            StringBuilder where_clause = new StringBuilder();

            foreach (string constraint in constraints)
            {
                where_clause.Append(constraint + " and ");
            }

            // Remove the last ' and ' from the instruction
            where_clause = where_clause.Remove(where_clause.Length - 5, 5);
            // Execute the command to the database
            DataTable dt = DatabaseManager.GetInstance().ExecuteQuery(query + where_clause);

            // Check if the database has returned a single record
            if (dt.GetRowCount() > 0)
            {
                // Get the record from the database
                foreach (DataRow row in dt)
                {
                    T value = new T();
                    // Loop through the columns returned by the database
                    for (int i = 0; i < row.GetFieldCount(); i++)
                    {
                        // Get the type of the class (typeof(T))
                        Type type = value.GetType();

                        // Get the information from a field. The name of the field should be the same as the name of the column
                        FieldInfo fi = type.GetField(row.GetField(i), BindingFlags.NonPublic | BindingFlags.Instance);

                        // Check if the attribute of the class equals to a DBMS type. If so the record is a foreign key
                        if (fi.FieldType.IsSubclassOf(typeof(DBMS)))
                        {
                            // The field is a foreign key

                            // Get the data of the single object from the database by calling the 'GetFromDatabase' method
                            MethodInfo database = fi.FieldType.BaseType.GetMethod("GetFromDatabase", BindingFlags.Public | BindingFlags.Static);

                            // Define a constraint to find a single record
                            string constraint = DatabaseManager.GetInstance().GetPrimaryKey(value.GetType().Name) + "=" + row.GetObject(row.GetField(i));

                            // Get the data object from the database
                            object fk = database.Invoke(null, new object[] { new string[] { constraint } });

                            // Set the received value to the correct field
                            fi.SetValue(value, fk);
                        }
                        else
                        {
                            // The field is a normal value, set the data to the object
                            fi.SetValue(value, row.GetObject(row.GetField(i)));
                        }
                    }
                    ret_val.Add(value);
                }
            }
            //// Triggers when the database return no records
            //else
            //{
            //    query = "";
            //    foreach (string constraint in constraints)
            //    {
            //        query += constraint + " and ";
            //    }
            //    query = query.Remove(query.Length - 5);
            //    throw new Exception("There is no record found with the specified constraints\nGet: " + table + "-object where " + query);
            //}

            // Return the new value with the correct values
            return ret_val.ToArray();
        }
    }
}
