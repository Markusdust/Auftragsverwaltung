using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Artikelgruppe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public int? UebergeordneteGruppeId { get; set; }

        public ICollection<Artikel> Artikels { get; set; }
       
        ////https://www.entityframeworktutorial.net/code-first/foreignkey-dataannotations-attribute-in-code-first.aspx
    }
}