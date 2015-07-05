using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Holds the information about a season of a serie
    /// </summary>
    class Season
    {
        #region "Fields"

        private int id;
        private int nr;
        private string story;
        private Serie serie;
        private List<Episode> episodes;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the number of the season inside the serie
        /// </summary>
        public int Nr
        {
            get { return this.nr; }
            set { this.nr = value; }
        }

        /// <summary>
        /// Gets or sets the (short) story describing the season
        /// </summary>
        public string Story
        {
            get { return this.story; }
            set { this.story = value; }
        }

        /// <summary>
        /// Gets or sets the serie in wich this season apears
        /// </summary>
        public Serie Serie
        {
            get { return this.serie; }
            set { this.serie = value; }
        }

        /// <summary>
        /// Gets or sets the episodes that apear in this season
        /// </summary>
        public List<Episode> Episodes
        {
            get { return new List<Episode>(this.episodes); }
            set { this.episodes = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
