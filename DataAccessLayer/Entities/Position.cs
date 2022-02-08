using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public int PositionNr { get; set; }
        public int Menge { get; set; }

        //Foreign Key Auftrag
        public int AuftragId { get; set; }
        //public Auftrag Auftrag { get; set; }

        //Foreign Key Artikel
        public int ArtikelId { get; set; }
        //public Artikel Artikel { get; set; }
    }
}
