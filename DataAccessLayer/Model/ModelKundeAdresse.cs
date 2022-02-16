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

        public Adresse LadeAdresseZuKunde(int KundenId)
        {
            Adresse adresseZuKunde;
            using (var context = new AuftragContext())
            {
                var queryMatchingAdress = context.KundenAdressen.Where(x =>
                        x.Kunde.Id.Equals(KundenId))
                    .Select(x => x.Adresse).First();

                adresseZuKunde = queryMatchingAdress;
            }

            return adresseZuKunde;
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
