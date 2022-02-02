using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public int ArtikelgruppeId { get; set; }
        public Artikelgruppe Artikelgruppe { get; set; }


        // public Artikel(string bezeichnung, int artikelnummer, decimal preisnetto, bool aktiv)
        // {
        //     this.Bezeichnung = bezeichnung; this.ArtikelNr = artikelnummer; this.PreisNetto = preisnetto; this.Aktiv = aktiv;
        //
        // }

        // public decimal mwst
        // {
        //     get { return Mwst; }
        //     set { Mwst = 7.7m; }
        //
        // }
        //
        // public decimal preisBrutto
        // {
        //     get { return PreisBrutto; }
        //     set { PreisBrutto = PreisNetto * Mwst; }
        // }


    }
}