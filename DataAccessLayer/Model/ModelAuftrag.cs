using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class ModelAuftrag
    {
        private List<Auftrag> meineAuftraege;
        public bool speichern(Auftrag auftrag)
        {
            using (var context = new AuftragContext())
            {
                context.Auftraege.Add(auftrag);
                context.SaveChanges();
                return true;
            }
        }

        public bool loeschen(int auftragid)
        {
            using (var context = new AuftragContext())
            {
                var auftrag = context.Auftraege.SingleOrDefault(a => a.Id == auftragid);
                
                context.Auftraege.Remove(auftrag);                
                context.SaveChanges();
                return true;
            }
        }

        public bool aendern(Auftrag auftrag)
        {
            using (var context = new AuftragContext())
            {
                context.Auftraege.Update(auftrag);
                context.SaveChanges();
                return true;
            }
        }

        public List<Auftrag> LadeAuftraege()
        {
            using (AuftragContext context = new AuftragContext())
            {
                meineAuftraege = context.Auftraege.ToList();             
            }
            return meineAuftraege;
        }

        public List<Auftrag> SucheAuftrag(string input)
        {
            using (var context = new AuftragContext())
            {
                meineAuftraege = context.Auftraege
                    .Where(a => a.AuftragsNr == Convert.ToInt32(input) || a.KundeId == Convert.ToInt32(input)).ToList();
                return meineAuftraege;
            }
        }
    }
}
