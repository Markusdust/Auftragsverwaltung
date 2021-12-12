using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Adresse
    {
        public int Id { get; set; }
        public string Strasse { get; set; }
        public string HausNr { get; set; }
        public DateTime GueltigAb { get; set; }
        public DateTime GueltigBis { get; set; }
        public int OrtschaftId { get; set; }
        public Ortschaft Ortschaft { get; set; }
    }
}
