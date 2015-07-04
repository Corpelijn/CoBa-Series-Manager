using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    class Seizoen
    {
        #region "Fields"

        private int id;
        private int nr;
        private string verhaal;
        private Serie serie;
        private List<Aflevering> afleveringen;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        public int Nr
        {
            get { return this.nr; }
            set { this.nr = value; }
        }

        public string Verhaal
        {
            get { return this.verhaal; }
            set { this.verhaal = value; }
        }

        public Serie Serie
        {
            get { return this.serie; }
            set { this.serie = value; }
        }

        public List<Aflevering> Afleveringen
        {
            get { return new List<Aflevering>(this.afleveringen); }
            set { this.afleveringen = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
