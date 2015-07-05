using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Hold the information for an episode
    /// </summary>
    class Episode
    {
        #region "Fields"

        private int id;
        private int nr;
        private string title;
        private string type;
        private string code;
        private string notes;
        private Season season;
        private List<Scene> scenes;
        private List<Staff> staff;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the number of the episode inside the season
        /// </summary>
        public int Nr
        {
            get { return this.nr; }
            set { this.nr = value; }
        }

        /// <summary>
        /// Gets or sets the title of the episode
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Gets or sets the type of the episode
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Gets or sets the unique code for the episode
        /// </summary>
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// Gets or sets some notes about the episode
        /// </summary>
        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        /// <summary>
        /// Gets or sets the season in wich the episode is apearing
        /// </summary>
        public Season Season
        {
            get { return this.season; }
            set { this.season = value; }
        }

        /// <summary>
        /// Gets or sets the scenes in the episode
        /// </summary>
        public List<Scene> Scenes
        {
            get { return new List<Scene>(this.scenes); }
            set { this.scenes = value; }
        }

        /// <summary>
        /// Gets or sets the staff used to create the episode
        /// </summary>
        public List<Staff> Staff
        {
            get { return new List<Staff>(this.staff); }
            set { this.staff = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
