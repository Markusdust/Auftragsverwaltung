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

        public bool loeschen(Auftrag auftrag)
        {
            using (var context = new AuftragContext())
            {
                context.Auftraege.Remove(auftrag);                
                //context.SaveChanges();
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
    }
}
