using CBSM.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    public class Serie : DBMS<Serie>
    {
        private string naam;
        private List<Seizoen> seizoenen;

        public Serie(string naam) : base()
        {
            this.naam = naam;
            this.seizoenen = new List<Seizoen>();
        }

        public Serie() : base()
        {
        }

        public void AddSeizoen(Seizoen seizoen)
        {
            seizoenen.Add(seizoen);
        }
    }


    public class Seizoen : DBMS<Seizoen>
    {
        private int code;
        private Serie serie;

        public Seizoen(int code, Serie serie) : base()
        {
            this.code = code;
            this.serie = serie;
        }

        public Seizoen() : base()
        {
        }
    }
}
