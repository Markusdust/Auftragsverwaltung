using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public int ArtikelNr { get; set; }
        public decimal PreisNetto { get; set; }

        private decimal Mwst;
        private decimal PreisBrutto;
        public bool Aktiv { get; set; }

        //public int ArtikelgruppeId { get; set; }
        //public Artikelgruppe Artikelgruppe { get; set; } // Hier Fehlermeldung (Is not virtual)

        // gibt es noch eine Klasse Position ? -- Joel
        // public ICollection<Position> Positionen { get; set; }
        // public Position Position { get; set; }

        public decimal mwst
        {
            get { return Mwst; }
            set { Mwst = 7.7m; }

        }

        public decimal preisBrutto
        {
            get { return PreisBrutto; }
            set { PreisBrutto = PreisNetto * Mwst; }
        }


    }
}
