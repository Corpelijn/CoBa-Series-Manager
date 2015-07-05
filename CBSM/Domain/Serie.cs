﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Hold the information of a serie
    /// </summary>
    class Serie
    {
        #region "Fields"

        private int id;
        private string name;
        private List<Season> seasons;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the name of the serie
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the seasons that exist in this serie
        /// </summary>
        public List<Season> Seasons
        {
            get { return new List<Season>(this.seasons); }
            set { this.seasons = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
