using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class KundenAdresse
    {
        public int Id { get; set; }

        //evtl nicht notwendig KundenNr
        public int KundenNr { get; set; }
        public DateTime GueltigAb { get; set; }
        public DateTime GueltigBis { get; set; }

        //Foreign Key Kunde
        public int KudenId { get; set; }
        public Kunde Kunde { get; set; }

        //Foreign Key Adresse
        public int AdresseId { get; set; }
        public Adresse Adresse { get; set; }

    }
}
