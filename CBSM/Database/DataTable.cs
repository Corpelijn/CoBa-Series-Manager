using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Database
{
    public class DataTable
    {
        #region "Fields"

        private List<string> indexes;
        private List<DataRow> data;

        #endregion

        #region "Constructors"

        #endregion

        #region "Properties"
        #endregion

        #region "Methods"

        /// <summary>
        /// Get data from a row in the DataTable
        /// </summary>
        /// <param name="row">The number of the row to fetch</param>
        /// <param name="columns">The order of the columns to be returned</param>
        /// <returns>The columns with data in the specified order</returns>
        public DataRow GetData(int row, string[] columns = null)
        {
            columns = columns ?? new string[] { "[ALL]" };
            return null;
        }

        /// <summary>
        /// Returns all the rows in the DataTable
        /// </summary>
        /// <param name="columns">The order of the columns to be returned</param>
        /// <returns>All the rows of the DataTable in the order specified</returns>
        public List<DataRow> GetAllData(string[] columns = null)
        {
            columns = columns ?? new string[] {"[ALL]"};
            return null;
        }

        #endregion
    }
}
