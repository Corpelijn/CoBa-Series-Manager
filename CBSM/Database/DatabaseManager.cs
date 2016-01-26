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

        public void WriteObject(object data)
        {
            // Check if the table exists
            if (!this.connection.DoesTableExists(data.GetType()))
            {
                // Create the table of the object
                this.connection.CreateTable(data);
            }

            // Check if the columns need to be updated
            this.connection.UpdateTable(data);

            // Insert or update the data in the table
            //DataTable table = this.connection.ExecuteQuery("select * from " + data.GetType().Name + " where id=" + data.GetType().GetField(((DBMS)data).GetPrimaryKey(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data));
            //if (table.GetRowCount() > 0)
            if ((int)data.GetType().GetField(((DBMS)data).GetPrimaryKey(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data) != -1)
            {
                // Update the row
                string set = " set ";
                string where = " where id=" + data.GetType().GetField(((DBMS)data).GetPrimaryKey(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data);

                foreach (FieldInfo fi in data.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    // Ignore 'pk' field
                    if (fi.Name == "pk")
                        continue;
                    set += fi.Name + "=" + (fi.GetValue(data).GetType() == typeof(string) ? "\'" + fi.GetValue(data) + "\'" : fi.GetValue(data)) + ",";
                }
                set = set.Remove(set.Length - 1);

                this.connection.ExecuteNonQuery("update " + data.GetType().Name + set + where);

                Console.WriteLine("update 1 row");
            }
            else
            {
                // Insert the row
                string columns = "";
                string values = "";
                foreach (FieldInfo info in data.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    // Ignore 'pk' field
                    if (info.Name == ((DBMS)data).GetPrimaryKey() || info.Name == "pk")
                        continue;

                    columns += info.Name + ",";
                    object d = info.GetValue(data);
                    if (d.GetType().IsSubclassOf(typeof(DBMS)))
                    {
                        // TODO: Create the other table and push the data to it
                        d = ((DBMS)d).GetPrimaryKeyValue();
                    }
                    else if (d is IList && d.GetType().IsGenericType)
                    {
                        Type GenericType = d.GetType().GetGenericArguments()[0];
                        if (!this.connection.DoesTableExists(GenericType))
                        {
                            this.connection.CreateTable(d);
                        }
                        d = "";
                    }
                    values += (d.GetType() == typeof(string) ? "\'" + d + "\'" : d) + ",";
                }

                columns = columns.Remove(columns.Length - 1);
                values = values.Remove(values.Length - 1);

                this.connection.ExecuteNonQuery("insert into " + data.GetType().Name + " (" + columns + ") values (" + values + ")");

                Console.WriteLine("insert 1 row");
            }
        }
    }
}
