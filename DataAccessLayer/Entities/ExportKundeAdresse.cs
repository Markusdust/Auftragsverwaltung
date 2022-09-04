using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ExportKundeAdresse
    {
        //Kunde
        public int KundenId { get; set; }
        public string KundenNr { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }
        public string Website { get; set; }
        public DateTime GueltigAb { get; set; }
        public DateTime GueltigBis { get; set; }

        
        //Adresse
        public int AdressId { get; set; }
        public string Strasse { get; set; }
        public string HausNr { get; set; }
        public DateTime AdressGueltigAb { get; set; }
        public DateTime AdressGueltigBis { get; set; }

        //public int OrtschaftId { get; set; }
        //public Ortschaft Ortschaft { get; set; }

        //Ortschaft
        public int OrtschaftId { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
        //public bool Aktiv { get; set; }


    }
}
