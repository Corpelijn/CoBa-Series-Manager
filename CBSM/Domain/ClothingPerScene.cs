using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Holds the clothes that are used per scene per character
    /// </summary>
    class ClothingPerScene
    {
        #region "Fields"

        private Clothing clothing;
        private Scene scene;
        private Character character;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the clothing object for the character in the specified scene
        /// </summary>
        public Clothing Clothing
        {
            get { return this.clothing; }
            set { this.clothing = value; }
        }

        /// <summary>
        /// Gets or sets the scene in wich the character apears in the specified cloths
        /// </summary>
        public Scene Scene
        {
            get { return this.scene; }
            set { this.scene = value; }
        }

        /// <summary>
        /// Gets or sets the character wearing the specified cloth in the specified scene
        /// </summary>
        public Character Character
        {
            get { return this.character; }
            set { this.character = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
