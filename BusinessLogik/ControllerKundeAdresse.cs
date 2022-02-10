using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogik
{
    public class ControllerKundeAdresse
    {
        private ModelKunde modelKunde = new ModelKunde();
        private ModelAdresse modelAdresse = new ModelAdresse();
        private ModelKundeAdresse modelKundeAdresse = new ModelKundeAdresse();

        public bool NeuerKundeAdresseAnlegen( string vorname, string nachname,
            string firma, string email, string passwort, string website, string strasse,
            string hausNr, int ortschaft)
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

           Adresse a1 = new Adresse()
           {
               Strasse = strasse,
               HausNr = hausNr,
               GueltigAb = DateTime.Now,
               GueltigBis = DateTime.MaxValue,
               OrtschaftId = ortschaft
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
