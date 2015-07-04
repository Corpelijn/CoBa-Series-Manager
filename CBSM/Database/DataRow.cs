using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    public class DataRow
    {
        #region "Fields"

        private List<string> indexes;
        private List<object> fields;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new DataRow object
        /// </summary>
        /// <param name="indexes">List of string objects with the names of the columns in order</param>
        /// <param name="fields">List of objects containing the data of the row</param>
        public DataRow(List<string> indexes, List<object> fields)
        {
            this.indexes = indexes;
            this.fields = fields;
        }

        /// <summary>
        /// Creates a new DataRow object
        /// </summary>
        /// <param name="indexes">Array of string objects with the names of the columns in order</param>
        /// <param name="fields">Array of objects containing the data of the row</param>
        public DataRow(string[] indexes, object[] fields)
        {
            this.indexes = new List<string>(indexes);
            this.fields = new List<object>(fields);
        }

        #endregion

        #region "Properties"
        #endregion

        #region "Methods"

        /// <summary>
        /// Get data from one of the columns in the row
        /// </summary>
        /// <param name="column">The column to get data from</param>
        /// <returns>The data from the field</returns>
        public object GetData(string column)
        {
            return null;
        }

        #endregion
    }
}
