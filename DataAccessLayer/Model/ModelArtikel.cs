using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
    public class ModelArtikel

    {
        public bool artikelspeichern(Artikel artikel)
        {
            using (var context = new AuftragContext())
            {
                context.Artikel.Add(artikel);
                context.SaveChanges();
                return true;
            }
        }

        public bool artikelgruppespeichern(Artikelgruppe artikelgruppe)
        {
            using (var context = new AuftragContext())
            {
                context.Artikelgruppe.Add(artikelgruppe);
                context.SaveChanges();
                return true;
            }
        }
    }
}
