using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CBSM.Database
{
    public class DataTable
    {
        private List<string> fields;
        private List<object[]> data;

        public DataTable(DbDataReader reader)
        {
            this.fields = new List<string>();
            this.data = new List<object[]>();


            for (int i = 0; i < reader.FieldCount; i++)
            {
                fields.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                for (int i = 0; i < fields.Count; i++)
                {
                    values[i] = reader.GetValue(reader.GetOrdinal(fields[i]));
                }
                data.Add(values);
            }
        }

        public int GetRowCount()
        {
            return this.data.Count;
        }

        public object[] GetRow(int row)
        {
            return data[row];
        }

        public DataRow GetDataRow(int row)
        {
            return new DataRow(fields, data[row]);
        }

        public object[] GetDataFromRow(string[] columns, int row)
        {
            object[] values = new object[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                int index = fields.IndexOf(columns[i]);
                values[i] = data[row][index];
            }

            return values;
        }

        public object GetObjectFromRow(string column, int row)
        {
            return data[row][fields.IndexOf(column)];
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            for (int i = 0; i < data.Count; i++)
            {
                yield return new DataRow(fields, data[i]);
            }
        }
    }

    public class DataRow
    {
        private List<string> fields;
        private object[] data;

        public DataRow(List<string> fields, object[] data)
        {
            this.fields = fields;
            this.data = data;
        }

        public object GetObject(string column)
        {
            return data[fields.IndexOf(column)];
        }

        public string GetField(int i)
        {
            return fields[i];
        }

        public int GetFieldCount()
        {
            return fields.Count;
        }
    }
}
