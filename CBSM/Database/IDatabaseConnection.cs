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

        void InitializeConnection();

        DataTable ExecuteQuery(string query);

        void ExecuteNonQuery(string query);

        int ExecuteInsert(string query);

        string GetDatabasename();

        string CreateLinkTable(string table1, string table2, string columnname);

        void CreateTable(Type type);

        bool DoesTableExists(Type type);

        bool DoesTableExists(string tablename);

        void UpdateTable(object data);

        string GetFieldType(FieldInfo info);
    }
}
