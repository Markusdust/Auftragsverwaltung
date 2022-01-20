using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Auftrag
    {
        public int Id { get; set; }
        public int AuftragsNr { get; set; }
        public DateTime Datum { get; set; }

        //Foreign Key Kunde
        public int KudenId { get; set; }
        public Kunde Kunde { get; set; }
    }
}
