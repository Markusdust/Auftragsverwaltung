using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Kunde
    {
        public int Id { get; set; }
        public string KundenNr { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }
        public string Website { get; set; }
        public DateTime GueltigAb { get; set; }
        public DateTime GueltigBis { get; set; }
    }
}
