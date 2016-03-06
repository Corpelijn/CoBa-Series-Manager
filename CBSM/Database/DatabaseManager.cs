using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CBSM.Database
{
    public class DatabaseManager
    {
        private IDatabaseConnection connection;
        private static DatabaseManager manager;

        private List<DBMS> tables;

        private DatabaseManager()
        {
            //this.connection = new MySQLConnection("192.168.94.5", "test", "bmt", "bmt");
            this.connection = new MySQLConnection("127.0.0.1", "bmt", "root", "", 8080);
            this.tables = new List<DBMS>();
        }

        public static DatabaseManager GetInstance()
        {
            if (manager == null)
            {
                manager = new DatabaseManager();
                manager.connection.InitializeConnection();
            }

            return manager;
        }

        public void AddTable(DBMS table)
        {
            tables.Add(table);
        }

        public DataTable ExecuteQuery(string query)
        {
            return this.connection.ExecuteQuery(query);
        }

        public void ExecuteNonQuery(string query)
        {
            this.connection.ExecuteNonQuery(query);
        }

        public string GetPrimaryKey(string table)
        {
            foreach (DBMS t in tables)
            {
                if (t.GetTableName() == table)
                {
                    return t.GetPrimaryKey();
                }
            }
            return null;
        }

        public bool DoesTableExist(Type table)
        {
            return this.connection.DoesTableExists(table);
        }

        public void WriteObject(object data)
        {
            // Check if the table exists
            if (!this.connection.DoesTableExists(data.GetType()))
            {
                // Create the table of the object
                this.connection.CreateTable(data.GetType());
            }

            // Check if the columns need to be updated
            this.connection.UpdateTable(data);

            // Insert or update the data in the table
            if ((int)((DBMS)data).GetPrimaryKeyValue() != -1)
            {
                // Check if we actually may touch the object
                if ((int)((DBMS)data).GetPrimaryKeyValue() == -2)
                {
                    // DO NOT TOUCH !!!
                    return;
                }

                // Update the row
                string set = " set ";
                string where = " where id=" + ((DBMS)data).GetPrimaryKeyValue();

                foreach (FieldInfo fi in data.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    // Ignore id field
                    if (fi.Name == "id")
                        continue;

                    if (typeof(IList).IsAssignableFrom(fi.FieldType))
                        continue;
                    if (fi.FieldType.IsSubclassOf(typeof(DBMS)))
                        set += fi.Name + "=" + ((DBMS)fi.GetValue(data)).GetPrimaryKeyValue() + ",";
                    else
                        set += fi.Name + "=" + (fi.GetValue(data).GetType() == typeof(string) ? "\'" + fi.GetValue(data) + "\'" : fi.GetValue(data)) + ",";
                }
                set = set.Remove(set.Length - 1);

                this.connection.ExecuteNonQuery("update " + data.GetType().Name + set + where);

                Console.WriteLine("update 1 row");
            }
            else
            {
                List<IList> list_data = new List<IList>();
                List<string> link_table_names = new List<string>();

                // Insert the row
                string columns = "";
                string values = "";
                foreach (FieldInfo info in data.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    // Ignore 'pk' field
                    if (info.Name == ((DBMS)data).GetPrimaryKey() || info.Name == "pk")
                        continue;


                    object d = info.GetValue(data);
                    if (d.GetType().IsSubclassOf(typeof(DBMS)))
                    {
                        // Create the other table and push the data to it
                        this.WriteObject(d);
                        // Get the primary key
                        d = ((DBMS)d).GetPrimaryKeyValue();
                    }
                    else if (d is IList && d.GetType().IsGenericType)
                    {
                        // The variable is a List<>. Create a link table for the data
                        Type GenericType = d.GetType().GetGenericArguments()[0];
                        if (!this.connection.DoesTableExists(GenericType) && !GenericType.IsPrimitive)
                        {
                            this.connection.CreateTable(GenericType);
                        }

                        if (!GenericType.IsPrimitive)
                        {
                            // Take some precautions
                            ((DBMS)data).SetInaccessible();

                            // Write all the objects to the corresponding table
                            foreach (object value in (IList)d)
                            {
                                this.WriteObject(value);
                            }

                            ((DBMS)data).SetAccessible();

                            // Setup data for the references in the link table
                            string table = DatabaseSettings.GetTable(data.GetType().Name, GenericType.Name, info.Name);
                            if (table == null)
                            {
                                // The table does not exist, create it
                                table = this.connection.CreateLinkTable(data.GetType().Name, GenericType.Name, info.Name);
                            }
                            // Setup the data to fill the table with
                            list_data.Add((IList)d);
                            link_table_names.Add(table);
                            continue;
                        }
                    }

                    if (d.GetType() == typeof(Int32) && (int)d == -2)
                    {
                        
                    }
                    else
                    {
                        columns += info.Name + ",";
                        values += (d.GetType() == typeof(string) ? "\'" + d + "\'" : d) + ",";
                    }
                }

                columns = columns.Remove(columns.Length - 1);
                values = values.Remove(values.Length - 1);

                int new_id = this.connection.ExecuteInsert("insert into " + data.GetType().Name + " (" + columns + ") values (" + values + ")");

                Console.WriteLine("insert 1 row");

                FieldInfo id_field = data.GetType().GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
                id_field.SetValue(data, new_id);

                // Create inserts for the link table
                for (int i = 0; i < list_data.Count; i++)
                {
                    for (int j = 0; j < list_data[i].Count; j++)
                    {
                        this.connection.ExecuteInsert("insert into " + link_table_names[i] + " values (NULL, " + new_id + ", " + ((DBMS)list_data[i][j]).GetPrimaryKeyValue() + ")");
                    }
                }
            }
        }
    }
}
