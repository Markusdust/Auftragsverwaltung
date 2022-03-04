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

        public decimal Mwst { get; set; }
        public decimal PreisBrutto { get; set; }
        public bool Aktiv { get; set; }



        // Foreign Key
        public int ArtikelgruppeId { get; set; }
        public Artikelgruppe Artikelgruppe { get; set; }
        



    }
}