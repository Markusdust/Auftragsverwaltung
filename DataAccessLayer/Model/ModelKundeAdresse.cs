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

        public bool aendern()
        {
            throw new NotImplementedException();
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
                        x.Kunde.Id.Equals(KundenId) && x.GueltigBis.Equals(DateTime.MaxValue))
                    .Select(x => x.Adresse).First();

                adresseZuKunde = queryMatchingAdress;
            }

            return adresseZuKunde;
        }
    }
}
