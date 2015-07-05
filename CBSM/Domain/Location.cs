using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Describes a location to use for recording a scene
    /// </summary>
    class Location
    {
        #region "Field"

        private int id;
        private string description;
        private string physicalLocation;
        private Person contactperson;
        private List<Availability> availability;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the description for the location
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the description of the location inside the real world
        /// </summary>
        public string PhysicalLocation
        {
            get { return this.physicalLocation; }
            set { this.physicalLocation = value; }
        }

        /// <summary>
        /// Gets or sets the information about the person to inform when wanting to record. Is the "owner" of the location
        /// </summary>
        public Person Contactperson
        {
            get { return this.contactperson; }
            set { this.contactperson = value; }
        }

        /// <summary>
        /// Gets or sets the available times and dates to record at this location
        /// </summary>
        public List<Availability> Availability
        {
            get { return this.availability; }
            set { this.availability = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
