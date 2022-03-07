using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
    public class ModelKundeAdresse 
    {
        public bool speichern(KundenAdresse kundenAdresse)
        {
            using (var context = new AuftragContext())
            {
                context.KundenAdressen.Add(kundenAdresse);
                context.SaveChanges();
                return true;
            }
        }

        public bool Aendern(KundenAdresse kundenAdresse)
        {
            using (var context = new AuftragContext())
            {
                context.KundenAdressen.Update(kundenAdresse);
                context.SaveChanges();
                return true;
            }
        }

        public bool loeschen()
        {
            throw new NotImplementedException();
        }

        public Kunde LadeKundezuKundeId(int kundenId)
        {
            Kunde KundezuKundeId;

            using (var context = new AuftragContext())
            {
                var queryMatchingKunde = context.Kunden
                    .Where(x => x.Id.Equals(kundenId))
                    .Select(x => x).First();

                KundezuKundeId = queryMatchingKunde;
            }

            return KundezuKundeId;
        }

        public Adresse LadeAdresseZuKunde(int KundenId)
        {
            Adresse adresseZuKunde;

            Adresse queryMatchingAdress;

            using (var context = new AuftragContext())
            {
                try
                {
                    queryMatchingAdress = context.KundenAdressen
                        .Where(x =>
                            x.Kunde.Id.Equals(KundenId) && x.GueltigBis.Equals(DateTime.MaxValue))
                        .Select(x => x.Adresse).First();
                }
                catch (Exception e)
                {
                   queryMatchingAdress = context.KundenAdressen
                        .Where(x =>
                            x.Kunde.Id.Equals(KundenId))
                        .Select(x => x.Adresse).First();
                }
                

                adresseZuKunde = queryMatchingAdress;
            }

            return adresseZuKunde;
        }

        public List<Adresse> LadeAlleAdressenzuKunde(int KundenId)
        {
            List<Adresse> meineAdressen;

            using (var context = new AuftragContext())
            {
                if (KundenId>0)
                {
                    meineAdressen = context.KundenAdressen.Where(x => x.Kunde.Id.Equals(KundenId)).Select(x => x.Adresse).ToList();
                }
                else
                {
                    meineAdressen = context.KundenAdressen.Where(x => x.Kunde.Id.Equals(KundenId)).Select(x => x.Adresse).ToList();
                }
                
            }

            return meineAdressen;
        }

        public KundenAdresse LadeKundenAdresseHilfTb(int kundenId)
        {
            KundenAdresse kundeAdresseHilftb;
            using (var context = new AuftragContext())
            {
                var query = context.KundenAdressen.Where(x =>
                        x.Kunde.Id.Equals(kundenId) && x.GueltigBis.Equals(DateTime.MaxValue))
                    .First();

                kundeAdresseHilftb = query;
            }

            return kundeAdresseHilftb;
        }
    }
}
