using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database.Tables
{
    class TableInformation
    {
        private static List<DatabaseTable> tables = new List<DatabaseTable>();

        public static DatabaseTable GetTable(string name)
        {
            return null;
        }

        private static void SetUp()
        {
            DatabaseTable dt = null;

            dt = new DatabaseTable("serie");
            dt.AddColumn(new DatabaseColumn("id", "number", 0, true));
            dt.AddColumn(new DatabaseColumn("name", "varchar2", 100));
            tables.Add(dt);

            dt = new DatabaseTable("seizoen");
            dt.AddColumn(new DatabaseColumn("id", "number", 0, true));
            dt.AddColumn(new DatabaseColumn("nr", "number", 0));
            dt.AddColumn(new DatabaseColumn("verhaal", "varchar2", 300));
            tables.Add(dt);

            dt = new DatabaseTable("seizoen_per_serie");
            dt.AddColumn(new DatabaseColumn("id", "number", 0, true));
            dt.AddColumn(new DatabaseColumn("serie_id", "number", 0, new DatabaseColumnForeignKey("fk_sps_serie_id", "serie_id", "serie", "id")));
            dt.AddColumn(new DatabaseColumn("seizoen_id", "number", 0, new DatabaseColumnForeignKey("fk_sps_seizoen_id", "seizoen_id", "seizoen", "id")));
            tables.Add(dt);
        }
    }
}
