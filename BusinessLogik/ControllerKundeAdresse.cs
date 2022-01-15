using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using System;

namespace BusinessLogik
{
    public class ControllerKundeAdresse
    {
        private ModelKunde modelKunde = new ModelKunde();

        public bool KundeAnlegen(int kundenNr, string vorname, string nachname,
            string firma, string email, string passwort, string website)
        {
            Kunde k1 = new Kunde()
            {
                KundenNr = kundenNr,
                Vorname = vorname,
                Nachname = nachname,
                Firma = firma,
                Email = email,
                Passwort = passwort,
                Website = website,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue,
            };
            modelKunde.speichern(k1);
            return true;
        }


    }
}
