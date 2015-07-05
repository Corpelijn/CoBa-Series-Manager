using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Describes an in movie/film character and hold the person playing it
    /// </summary>
    class Character
    {
        #region "Fields"

        private int id;
        private string name;
        private string description;
        private Person person;
        private string notes;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the name of the character
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets a description for the character
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the person playering the character
        /// </summary>
        public Person Person
        {
            get { return this.person; }
            set { this.person = value; }
        }

        /// <summary>
        /// Gets or sets some notes about the other given information
        /// </summary>
        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
