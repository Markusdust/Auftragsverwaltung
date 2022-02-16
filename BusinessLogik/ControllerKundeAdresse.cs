using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;

namespace BusinessLogik
{
    public class ControllerKundeAdresse
    {
        private ModelKunde modelKunde = new ModelKunde();
        private ModelAdresse modelAdresse = new ModelAdresse();
        private ModelKundeAdresse modelKundeAdresse = new ModelKundeAdresse();
        private ModelOrtschaft modelOrtschaft = new ModelOrtschaft();

        public bool NeuerKundeAdresseAnlegen(string vorname, string nachname,
            string firma, string email, string passwort, string website, string strasse,
            string hausNr, string ortschaft, int plz)
        {
            ///KundeAdresse als Speicherbefehl geben somit sollte Adresse und Kunde automatisch mitgespeichert werden.
            /// muss somit nicht einzel gespeichert werden.

            Kunde k1 = new Kunde()
            {
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

            Ortschaft o1 = new Ortschaft()
            {
                Ort = ortschaft,
                PLZ = plz,
                Aktiv = true
            };
            modelOrtschaft.speichern(o1);


            Adresse a1 = new Adresse()
            {
                Strasse = strasse,
                HausNr = hausNr,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue,
                OrtschaftId = o1.Id
            };
            modelAdresse.speichern(a1);
            KundenAdresse kA1 = new KundenAdresse()
            {
                KundeId = k1.Id,
                KundenNr = k1.KundenNr,
                AdresseId = a1.Id,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue
            };
            modelKundeAdresse.speichern(kA1);
            // modelKunde aber mit KudnenAdresse also => modelKunde(kA1);

            return true;
        }

        //wenn bei Kunde und Adresse Änderungen vorliegen wird dies durchgeführt
        public bool AendereKundeAdresseOrtschaft(
            Kunde aktuellerKunde, Adresse aktuelleAdresse, int altOrtschaftId,
            string vorname, string nachname, string firma,
            string email, string passwort, string website,
            string strasse, string hausNr, string ortschaft, int plz)
        {

            //zuerst Kunde Adresse auf limited Time setzten.

            //KundeAlt auf limited Time setzten
            Kunde kundeAlt = new Kunde()
            {
                Id = aktuellerKunde.Id,
                KundenNr = aktuellerKunde.KundenNr,
                Vorname = aktuellerKunde.Vorname,
                Nachname = aktuellerKunde.Nachname,
                Firma = aktuellerKunde.Firma,
                Email = aktuellerKunde.Email,
                Passwort = aktuellerKunde.Passwort,
                Website = aktuellerKunde.Website,
                GueltigAb = aktuellerKunde.GueltigAb,
                GueltigBis = DateTime.Now,
            };
            modelKunde.Aendern(kundeAlt);

            //AdresseAlt auf limited Time setzten
            Adresse adresseAlt = new Adresse()
            {
                Id = aktuelleAdresse.Id,
                Strasse = aktuelleAdresse.Strasse,
                HausNr = aktuelleAdresse.HausNr,
                OrtschaftId = aktuelleAdresse.OrtschaftId,
                GueltigAb = aktuelleAdresse.GueltigAb,
                GueltigBis = DateTime.Now,
            };
            modelAdresse.Aendern(adresseAlt);

            //Kunden-Adresse Kombination abfragen und als Objekt variable setzen
            var kundeAdresseAlt = modelKundeAdresse.LadeKundenAdresseHilfTb(kundeAlt.Id);

            //Kunde-Adresse Kombination auf limited Time setzen
            KundenAdresse altKundenAdresse = new KundenAdresse()
            {
                Id = kundeAdresseAlt.Id,
                KundeId = kundeAdresseAlt.KundeId,
                KundenNr = kundeAdresseAlt.KundenNr,
                AdresseId = kundeAdresseAlt.AdresseId,
                GueltigAb = kundeAdresseAlt.GueltigAb,
                GueltigBis = DateTime.Now,
            };
            modelKundeAdresse.Aendern(altKundenAdresse);

            //Kunde mit Änderungen anlegen KundenNR bleibt gleich!!
            Kunde kundeNeu = new Kunde()
            {
                KundenNr = kundeAlt.KundenNr,
                Vorname = vorname,
                Nachname = nachname,
                Firma = firma,
                Email = email,
                Passwort = passwort,
                Website = website,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue,
            };
            modelKunde.speichern(kundeNeu);

            //neue Ortschaft abspeichern (falls noch nicht vorhanden)
            Ortschaft o1 = new Ortschaft()
            {
                Ort = ortschaft,
                PLZ = plz,
                Aktiv = true
            };
            modelOrtschaft.speichern(o1);

            //neue Adresse mit Änderungen anlegen
            Adresse adresseNeu = new Adresse()
            {
                Strasse = strasse,
                HausNr = hausNr,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue,
                OrtschaftId = o1.Id
            };
            modelAdresse.speichern(adresseNeu);


            //Kunden-Adresse Kombi anlegen KundenNr bleibt gleich!!
            KundenAdresse kA1 = new KundenAdresse()
            {
                KundeId = kundeNeu.Id, 
                KundenNr = kundeNeu.KundenNr, //kundeNeu hat bereits alte nummer beim speichern übernommen
                AdresseId = adresseNeu.Id,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue
            };
            modelKundeAdresse.speichern(kA1);

            return true;
        }

        public List<Kunde> LadeKunden()
        {
            return modelKunde.LadeKunden();
        }

        public List<Adresse> LadeAdressen()
        {
            return modelAdresse.LadeAdressen();
        }

        public Adresse AdresseZuKunde(int KundenId)
        {
            return modelKundeAdresse.LadeAdresseZuKunde(KundenId);
        }
    }
}
