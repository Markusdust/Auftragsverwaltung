using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
    public class ModelKunde
    {
        private List<Kunde> meineKunden;
        public bool speichern(Kunde kunde)
        {
            using (var context= new AuftragContext())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
                return true;
            }
        }

        public bool Aendern(Kunde kunde)
        {
            using (var context = new AuftragContext())
            {
                context.Kunden.Update(kunde);
                context.SaveChanges();
                return true;
            }
        }

        public bool Loeschen()
        {
            throw new NotImplementedException();
        }

        public List<Kunde> LadeKunden(string filtergrad)
        {
            using (AuftragContext context = new AuftragContext())
            {
                if (filtergrad == "aktive")
                {
                    meineKunden = context.Kunden.Where(x => x.GueltigBis.Equals(DateTime.MaxValue)).ToList();
                }
                else if (filtergrad == "alte")
                {
                    meineKunden = context.Kunden.Where(x => x.GueltigBis < DateTime.MaxValue).ToList();
                }
                else //lade alle
                {
                    meineKunden = context.Kunden.ToList();
                }
            }

            return meineKunden;
        }

        public List<Kunde> LadeAlleKunden()
        {
            using (AuftragContext context = new AuftragContext())
            {
                meineKunden = context.Kunden.ToList();
            }

            return meineKunden;
        }

    }
}
