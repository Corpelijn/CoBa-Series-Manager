using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace CBSM.Database
{
    public interface IDatabaseConnection
    {
        bool OpenConnection(string hostname, string database, string username, string password, int port = -1);

        void CloseConnection();

        bool IsOpen();

        DataTable ExecuteQuery(string query);

        void ExecuteNonQuery(string query);

        string GetDatabasename();


        void CreateTable(object data);

        bool DoesTableExists(Type type);

        void UpdateTable(object data);

        string GetFieldType(FieldInfo info, object data);
    }
}
