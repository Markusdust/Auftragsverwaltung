using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
    public class ModelAdresse
    {
        private List<Adresse> meineAdressen;
        public bool speichern(Adresse adresse)
        {
            using (var context = new AuftragContext())
            {
                context.Adressen.Add(adresse);
                context.SaveChanges();
                return true;
            }
        }

        public bool Aendern(Adresse adresse)
        {
            using (var context = new AuftragContext())
            {
                context.Adressen.Update(adresse);
                context.SaveChanges();
                return true;
            }
        }

        public bool loeschen()
        {
            throw new NotImplementedException();
        }

        public List<Adresse> LadeListeAdressen()
        {
            using (var context = new AuftragContext())
            {
               meineAdressen= context.Adressen.ToList();
            }

            return meineAdressen;
        }

        public Adresse LadeAdresseZuAdressId(int adressId)
        {
            Adresse adresseZuAdressId;

            using (var context = new AuftragContext())
            {
                var queryMatchingAdresse = context.Adressen
                    .Where(x => x.Id.Equals(adressId))
                    .Select(x => x).First();

                adresseZuAdressId = queryMatchingAdresse;
            }

            return adresseZuAdressId;
        }

    }
}
