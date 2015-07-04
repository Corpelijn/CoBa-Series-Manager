using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    class Aflevering
    {
        #region "Fields"

        private int id;
        private int nr;
        private string titel;
        private string type;
        private string code;
        private string opmerking;
        private Seizoen seizoen;
        private List<Scene> scenes;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        public int Nr
        {
            get { return this.nr; }
            set { this.nr = value; }
        }

        public string Titel
        {
            get { return this.titel; }
            set { this.titel = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Opmerking
        {
            get { return this.opmerking; }
            set { this.opmerking = value; }
        }

        public Seizoen Seizoen
        {
            get { return this.seizoen; }
            set { this.seizoen = value; }
        }

        public List<Scene> Scenes
        {
            get { return new List<Scene>(this.scenes); }
            set { this.scenes = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
