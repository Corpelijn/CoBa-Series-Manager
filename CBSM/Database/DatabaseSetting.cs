using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    class DatabaseSetting : DBMS<DatabaseSetting>
    {
        private string name;
        private string data;

        public DatabaseSetting(DBSettings name, string data)
        {
            this.name = Enum.GetName(typeof(DBSettings), name);
            this.data = data;
        }

        public DatabaseSetting(string name, string data)
        {
            this.name = name;
            this.data = data;
        }

        public DatabaseSetting()
        {
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Data
        {
            get { return data; }
            set { data = value; }
        }
    }

    class DatabaseSettings
    {
        private List<DatabaseSetting> settings;

        public DatabaseSettings()
        {
            this.settings = new List<DatabaseSetting>();

            // Write the default values
            this.settings.Add(new DatabaseSetting(DBSettings.NextLinkNumber, "0"));

            if (DatabaseManager.GetInstance().DoesTableExist(settings[0].GetType()))
            {
                // Check for newer values in the database
                UpdateFromDatabase();
                FetchTableInformation();
            }
        }

        private static DatabaseSettings instance;

        public static DatabaseSettings GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseSettings();
            }
            return instance;
        }

        public static void SetValue(DBSettings setting, string value)
        {
            DatabaseSetting ds = GetInstance().FindSetting(setting);
            ds.Data = value;
            GetInstance().UpdateToDatabase();
        }

        public static string GetValue(DBSettings setting)
        {
            DatabaseSetting ds = GetInstance().FindSetting(setting);
            return ds.Data;
        }

        private DatabaseSetting FindSetting(DBSettings name)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                if (settings[i].Name == Enum.GetName(typeof(DBSettings), name))
                {
                    return settings[i];
                }
            }
            return null;
        }

        private DatabaseSetting FindSetting(string name)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                if (settings[i].Name == name)
                {
                    return settings[i];
                }
            }
            return null;
        }

        public static void SetTable(string table1, string table2, string column, string linkTable)
        {
            DatabaseSettings ds = GetInstance();
            ds.settings.Add(new DatabaseSetting(table1 + "->" + table2 + "|" + column, linkTable));
            ds.UpdateToDatabase();
        }

        public static string GetTable(string table1, string table2, string column)
        {
            DatabaseSettings ds = GetInstance();
            DatabaseSetting setting = ds.FindSetting(table1 + "->" + table2 + "|" + column);
            if (setting == null)
                return null;
            return setting.Data;
        }

        private void UpdateToDatabase()
        {
            for (int i = 0; i < settings.Count; i++)
            {
                settings[i].WriteToDatabase();
            }
        }

        private void UpdateFromDatabase()
        {
            for (int i = 0; i < settings.Count; i++)
            {
                settings[i] = DatabaseSetting.GetFromDatabase(new string[] { "name=\'" + settings[i].Name + "\'" });
            }
        }

        private void FetchTableInformation()
        {
            DatabaseSetting[] sets = DatabaseSetting.GetMutipleFromDatabase(new string[] {"name like \'%->%|%\'"});
            settings.AddRange(sets);
        }
    }

    enum DBSettings
    {
        NextLinkNumber = 0
    }
}
