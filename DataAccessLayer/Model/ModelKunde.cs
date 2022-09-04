using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using static System.Net.Mime.MediaTypeNames;

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

        public List<Kunde> SucheDatenesatz(string kundenNr, string vorname, string nachname, string firma, string email, string website)
        {
            IQueryable<Kunde> kunden;

            using (AuftragContext context = new AuftragContext())
            {
                kunden = context.Kunden.AsQueryable();


                //TODO Kundennr als String
                if (!string.IsNullOrEmpty(kundenNr))
                {
                    kunden = kunden.Where(x => x.KundenNr.Equals(kundenNr));
                    // listeKunden = kunden.ToList();
                }

                if (!string.IsNullOrEmpty(vorname))
                {
                    kunden = kunden.Where(x => x.Vorname.Contains(vorname));
                   // listeKunden = kunden.ToList();
                }

                if (!string.IsNullOrEmpty(nachname))
                {
                    kunden = kunden.Where(x => x.Nachname.Contains(nachname));
                }
                if (!string.IsNullOrEmpty(firma))
                {
                    kunden = kunden.Where(x => x.Firma.Contains(firma));
                }
                if (!string.IsNullOrEmpty(email))
                {
                    kunden = kunden.Where(x => x.Email.Contains(email));
                }
                if (!string.IsNullOrEmpty(website))
                {
                    kunden = kunden.Where(x => x.Website.Contains(website));
                }

             return  kunden.ToList();

            }


        }

        public bool speicherJson(string kundeValues)
        {

            string TimeNow = DateTime.Now.ToString();
            var TimeNowString = TimeNow.Replace(".", "-").Replace(" ", "_").Replace(":", "-");
            var path = $@"C:\temp\CustomerExport_{TimeNowString}.Json";
            File.WriteAllText(path, kundeValues);
            return true;

        }


    }
}
