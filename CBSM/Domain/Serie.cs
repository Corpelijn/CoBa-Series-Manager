using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    class Serie
    {
        #region "Fields"

        private int id;
        private string naam;
        private List<Seizoen> seizoenen;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        public string Name
        {
            get { return this.naam; }
            set { this.naam = value; }
        }

        public List<Seizoen> Seizoenen
        {
            get { return new List<Seizoen>(this.seizoenen); }
            set { this.seizoenen = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
