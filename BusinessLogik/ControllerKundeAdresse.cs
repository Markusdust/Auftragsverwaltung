using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
//using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.RegularExpressions;

namespace BusinessLogik
{
    public class ControllerKundeAdresse
    {
        private ModelKunde modelKunde = new ModelKunde();
        private ModelAdresse modelAdresse = new ModelAdresse();
        private ModelKundeAdresse modelKundeAdresse = new ModelKundeAdresse();
        private ModelOrtschaft modelOrtschaft = new ModelOrtschaft();

        bool aenderungKunde;
        bool aenderungAdresse;
        bool aenderungOrtschaft;

        private bool RegexKunde(string kundenNr, string email, string webside, string passwort)
        {
            var regKundenNr = @"^CU\d{5}$";

            //var regEmail = @"/^(?!(?:(?:\x22?\x5C[\x00-\x7E]\x22?)|(?:\x22?[^\x5C\x22]\x22?)){255,})(?!(?:(?:\x22?\x5C[\x00-\x7E]\x22?)|(?:\x22?[^\x5C\x22]\x22?)){65,}@)(?:(?:[\x21 \x23-\x27\x2A\x2B\x2D\x2F-\x39\x3D\x3F\x5E-\x7E]+)|(?:\x22(?:[\x01-\x08\x0B \x0C\x0E-\x1F\x21\x23-\x5B\x5D-\x7F]|(?:\x5C[\x00-\x7F]))*\x22))(?:\.(?:(?: [\x21\x23-\x27\x2A\x2B\x2D\x2F-\x39\x3D\x3F\x5E-\x7E]+)|(?:\x22(?:[\x01-\x08 \x0B\x0C\x0E-\x1F\x21\x23-\x5B\x5D-\x7F]|(?:\x5C[\x00-\x7F]))*\x22)))*@(?:(?:(?!.*[^.]{64,})(?:(?:(?:xn--)?[a-z0-9]+(?:-+[a-z0-9]+)*\.){1,126}){1,}(?:(?: [az][a-z0-9]*)|(?:(?:xn--)[a-z0-9]+))(?:-+[a-z0-9]+)*)|(?:\[(?:(?:IPv6:(?:(?: [a-f0-9]{1,4}(?::[a-f0-9]{1,4}){7})|(?:(?!(?:.*[a-f0-9][:\]]){7,})(?:[a-f0-9]{1,4}(?::[a-f0-9]{1,4}){0,5})?::(?:[a-f0-9]{1,4}(?::[a-f0-9]{1,4}){0,5})?)))|(?:(?:IPv6:(?:(?:[a-f0-9]{1,4}(?::[a-f0-9]{1,4}){5}:)|(?:(?!(?:.*[a-f0-9]:){5,})(?:[a-f0-9]{1,4}(?::[a-f0-9]{1,4}){0,3})?::(?:[a-f0-9]{1,4}(?::[a-f0-9]{1,4}){0,3}:)?)))?(?:(?:25[0-5])|(?:2[0-4][0-9])|(?:1[0-9]{2})|(?:[1-9]?[0-9]))(?:\.(?:(?:25[0-5])|(?:2[0-4][0-9])|(?:1[0-9]{2})|(?:[1-9]?[0- 9]))){3}))\]))\z/i";
            //var regEmail = @"/^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[az0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\z/i";
            var regEmail = @"^[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*$";
            //var regWebside = "";

            var regPasswort = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
            
            Regex regexKundenNr = new Regex(regKundenNr);
            Regex regexEmail = new Regex(regEmail);
            //Regex regexWebside = new Regex(regWebside);
            Regex regexPasswort = new Regex(regPasswort);
            bool check = true;

            if (!regexKundenNr.IsMatch(kundenNr))
            {
                throw new Exception("Ungütlige KundenNr");
                check = false;
                
            }
            else if (!regexEmail.IsMatch(email))
            {
                throw new Exception("Ungültige Emailadresse");
                check = false;
            }
            //else if (!regexWebside.IsMatch(email))
            //{
            //    throw new Exception("Ungültige Emailadresse");
            //    return false;
            //}
            else if (!regexPasswort.IsMatch(passwort))
            {
                throw new Exception("Ungültige Emailadresse");
                check = false;
            }

            return check ;
        }

        public bool NeuerKundeAdresseAnlegen(string kundenNr,string vorname, string nachname,
            string firma, string email, string passwort, string website, string strasse,
            string hausNr, string ortschaft, int plz)
        {
            if(RegexKunde(kundenNr, email, website, passwort))
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

                ///KundeAdresse als Speicherbefehl geben somit sollte Adresse und Kunde automatisch mitgespeichert werden.
                /// muss somit nicht einzel gespeichert werden.
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
            return false;
            


        }

        //wenn bei Kunde und Adresse Änderungen vorliegen wird dies durchgeführt
        public bool AendereKundeAdresseOrtschaftAAALT(
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

        public List<Kunde> LadeKunden(string filtergrad)
        {
            return modelKunde.LadeKunden(filtergrad);
        }

        public List<Kunde> LadeAlleKunden()
        {
            return modelKunde.LadeAlleKunden();
        }

        public List<Adresse> LadeAdressen(int kundenId)
        {
            //return modelAdresse.LadeListeAdressen();
            return modelKundeAdresse.LadeAlleAdressenzuKunde(kundenId);
        }

        public Adresse AdresseZuKunde(int kundenId)
        {
            return modelKundeAdresse.LadeAdresseZuKunde(kundenId);
        }

        public Kunde KundezuKundeId(int kundenId)
        {
            return modelKundeAdresse.LadeKundezuKundeId(kundenId);
        }

        public Adresse AdresseZuAdressId(int adressId)
        {
            return modelAdresse.LadeAdresseZuAdressId(adressId);
        }


        /////////////Neu Ansatz Kunde ändern
        ///
        ///zuerst aktuellen kunde abfragen mit Methode KundezuKundeId
        /// Dann aktuellen kunden vergeliche mit den Änderungen welche vorgenommen wurden um
        /// zu prüfen ob wirklich änderungen vorhanden sind
        ///Wenn Änderungen vorhanden sind dann änderungen speichern....
        ///
        ///NEUE ANSATZ
        ///
        //
        public bool AendereKundeAdresseOrtschaft(int kundeIdAlt, string vorname, string nachname, string firma,
            string email, string passwort, string website, string strasse, string hausNr, int plz,
            string ortschaft)
        {

            KundenAdresse altKundenAdresse;

            //ladet den alten (angewählten) Kunden
            Kunde kundeAlt = KundezuKundeId(kundeIdAlt);

            //neues Kundenobjekt erstellen mit den Werten
            Kunde kundeNeu = new Kunde()
            {
                KundenNr = kundeAlt.KundenNr, // KundenNr bleibt gleich
                Vorname = vorname,
                Nachname = nachname,
                Firma = firma,
                Email = email,
                Passwort = passwort,
                Website = website,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue
            };

            //ladet den alte Adresse zum alten Kunden
            Adresse adresseAlt = AdresseZuKunde(kundeIdAlt);

            //Neues Adressobjekt erstellen mit den Werten
            Adresse adresseNeu = new Adresse()
            {
                Strasse = strasse,
                HausNr = hausNr,
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.MaxValue,
            };

            //Ladet die alte Ortschaft zur alten Adresse
            Ortschaft ortschaftAlt = modelOrtschaft.LadeOrtschaftZuAdresse(adresseAlt.OrtschaftId);

            //Neues Ortschaftobjekt erstellen mit den Werten
            Ortschaft ortschaftNeu = new Ortschaft()
            {
                Ort = ortschaft,
                PLZ = plz,
                Aktiv = true,
            };

            //Prüfe ob änderungen von neu Kunde und altKunde vorhanden sind OHNE GUELTIGAB, GUELTIGBIS
            //Falls JA 
            if (KundePruefeAenderungen(kundeAlt, kundeNeu))
            {
                //Alter Kunde auf Limited Time setzten
                kundeAlt.GueltigBis = DateTime.Now;
                //Alter Kunde Änderung speichern
                modelKunde.Aendern(kundeAlt);
            }

            //Prüfe ob änderungen von neue Adresse zu alte Adresse vorhanden sind ohne GUELTIGAB, GUELTIGBIS
            //FALLS JA 
            if (AdressePruefeAenderugen(adresseAlt, adresseNeu))
            {
                //Alte Adresse auf Limited Time setzten
                adresseAlt.GueltigBis = DateTime.Now;
                //alter Adresse änderung speichern
                modelAdresse.Aendern(adresseAlt);
            }

            //Prueft ob änderungen von neue Ortschaft zu alte Ortschaft vorhanden sind ohne Aktive
            if (OrtschaftPruefeAenderung(ortschaftAlt, ortschaftNeu))
            {
                //alte ortschaft muss nicht gelöscht werden, da sie für andere verwendet werden können
            }


            // WENN ALLES geändert hat Kunde, Adresse, Ortschaft
            if (aenderungKunde && aenderungAdresse && aenderungOrtschaft)
            {
                //neuen kunden speichern
                //Datum nochmals auf aktuelle zeit setzen um keine überschneidung zu haben.
                kundeNeu.GueltigAb = DateTime.Now;
                modelKunde.speichern(kundeNeu);

                //neue Ortschaft speichern
                modelOrtschaft.speichern(ortschaftNeu);

                //neue Ortschaft der Adresse zuweisen
                adresseNeu.OrtschaftId = ortschaftNeu.Id;
                //neue Adresse speichern
                //Datum nochmals auf aktuelle zeit setzen um keine überschneidung zu haben.
                adresseNeu.GueltigAb = DateTime.Now;
                modelAdresse.speichern(adresseNeu);

                //alte kundenAdresse verknüpfung auf limietd Zeit ändern
                altKundenAdresse = modelKundeAdresse.LadeKundenAdresseHilfTb(kundeIdAlt);
                altKundenAdresse.GueltigBis = DateTime.Now;
                modelKundeAdresse.Aendern(altKundenAdresse);


                //KundenAdresse verknüpfung speichern
                KundenAdresse kundenAdresseNeu = new KundenAdresse()
                {
                    KundeId = kundeNeu.Id,
                    KundenNr = kundeNeu.KundenNr, //kundeNeu hat bereits alte nummer beim speichern übernommen
                    AdresseId = adresseNeu.Id,
                    GueltigAb = DateTime.Now,
                    GueltigBis = DateTime.MaxValue
                };
                modelKundeAdresse.speichern(kundenAdresseNeu);

            }

            //---------------------------------------------------------------
            //wenn sich NUR Kunde ändert
            else if (aenderungKunde && aenderungAdresse == false)
            {
                //neuen kunden speichern
                //Datum nochmals auf aktuelle zeit setzen um keine überschneidung zu haben.
                kundeNeu.GueltigAb = DateTime.Now;
                modelKunde.speichern(kundeNeu);

                //alte kundenAdresse verknüpfung auf limietd Zeit ändern
                altKundenAdresse = modelKundeAdresse.LadeKundenAdresseHilfTb(kundeIdAlt);
                altKundenAdresse.GueltigBis = DateTime.Now;
                modelKundeAdresse.Aendern(altKundenAdresse);

                //KundenAdresse verknüpfung speichern
                KundenAdresse kundenAdresseNeuKundeAenderung = new KundenAdresse()
                {
                    KundeId = kundeNeu.Id,
                    KundenNr = kundeNeu.KundenNr, //kundeNeu hat bereits alte nummer beim speichern übernommen
                    AdresseId = adresseAlt.Id,
                    GueltigAb = DateTime.Now,
                    GueltigBis = DateTime.MaxValue
                };
                modelKundeAdresse.speichern(kundenAdresseNeuKundeAenderung);
            }

            //Wenn sich NUR Adresse (oder Ortschaft) ändert
            else if (aenderungOrtschaft || aenderungAdresse && aenderungKunde == false)
            {
                if (aenderungOrtschaft)
                {
                    //neue Ortschaft speichern
                    modelOrtschaft.speichern(ortschaftNeu);
                    //neue Ortschaft der Adresse zuweisen
                    adresseNeu.OrtschaftId = ortschaftNeu.Id;
                    modelAdresse.speichern(adresseNeu);
                }
                else
                {
                    //neue Adresse speichern
                    //Datum nochmals auf aktuelle zeit setzen um keine überschneidung zu haben.
                    adresseNeu.GueltigAb = DateTime.Now;
                    adresseNeu.OrtschaftId = ortschaftAlt.Id;

                    modelAdresse.speichern(adresseNeu);
                }

                //KundeAdresse verknüpfung auf limited Zeit ändern
                altKundenAdresse = modelKundeAdresse.LadeKundenAdresseHilfTb(kundeIdAlt);
                altKundenAdresse.GueltigBis = DateTime.Now;
                modelKundeAdresse.Aendern(altKundenAdresse);

                //KundenAdresse verknüpfung speichern
                KundenAdresse kundenAdresseNeuKundeAenderung = new KundenAdresse()
                {
                    KundeId = altKundenAdresse.KundeId,
                    KundenNr = altKundenAdresse.KundenNr, //kundeNeu hat bereits alte nummer beim speichern übernommen
                    AdresseId = adresseNeu.Id,
                    GueltigAb = DateTime.Now,
                    GueltigBis = DateTime.MaxValue
                };
                modelKundeAdresse.speichern(kundenAdresseNeuKundeAenderung);


            }

            //Wenn sich Nur Ortschaft ändert muss ebenfalls die Adresse angepasst werden da Ortschaft kein Limited Time hat.

            return true;

        }

        //---------------------------

        public bool KundePruefeAenderungen(Kunde altkunde, Kunde neuKunde)
        {
            if (altkunde.Vorname != neuKunde.Vorname ||
                altkunde.Nachname != neuKunde.Nachname ||
                altkunde.Firma != neuKunde.Firma ||
                altkunde.Email != neuKunde.Email ||
                altkunde.Passwort != neuKunde.Passwort ||
                altkunde.Website != neuKunde.Website
               )
            {
                //Änderungen sind vorhanden
                aenderungKunde = true;
                return true;
            }

            aenderungKunde = false;
            return false;

        }

        public bool AdressePruefeAenderugen(Adresse altAdresse, Adresse neuAdresse)
        {
            if (altAdresse.Strasse != neuAdresse.Strasse ||
                altAdresse.HausNr != neuAdresse.HausNr)
            {

                //Änderung vorhanden
                aenderungAdresse = true;
                return true;
            }

            aenderungAdresse = false;
            return false;
        }

        public bool OrtschaftPruefeAenderung(Ortschaft altOrtschaft, Ortschaft neuOrtschaft)
        {
            if (altOrtschaft.Ort != neuOrtschaft.Ort ||
                altOrtschaft.PLZ != neuOrtschaft.PLZ)
            {
                //Änderungen vorhanden
                aenderungOrtschaft = true;
                return true;
            }

            aenderungOrtschaft = false;
            return false;
        }

        //benötigt altKunden => kann mit hilfe von KundeZuId(int kundenId) herausgefunden werden
        public Kunde KundeAendern(Kunde altKunden, string vorname, string nachname, string firma,
            string email, string passwort, string website)
        {
            //alten Kunden Lebensdauer setzten
            altKunden.GueltigBis = DateTime.Now;
            modelKunde.Aendern(altKunden);

            //neuen Kunde anlegen mit akutellen informationen und der gleichen KundenNr.


            Kunde kundeNeu = new Kunde()
            {
                KundenNr = altKunden.KundenNr,
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

            return kundeNeu;
        }

        public void KundenAdresseAendern()
        {

        }

        public List<Kunde> SucheDatensatz(string kundenNr, string vorname, string nachname, string firma, string email,
            string website)
        {
            return modelKunde.SucheDatenesatz(kundenNr, vorname, nachname, firma, email, website);
        }


        public bool KundeExportieren(int KundenId)
        {
            var kunde = KundezuKundeId(KundenId);

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string serialized = JsonSerializer.Serialize(kunde, options);

            modelKunde.speicherJson(serialized);

            return true;
        }

        public bool KundeAdresseExportieren(int kundenId, int adressId, int ortschaftId)
        {
            var controllerOrtschaft = new ControllerOrtschaft();

            var kunde = KundezuKundeId(kundenId);
            var adresse = AdresseZuAdressId(adressId);
            var ortschaft = controllerOrtschaft.OrtschaftZuAdresse(adressId);


            //Todo implementieren
            var exportObjekt = new ExportKundeAdresse()
            {
                KundenId = kunde.Id,
                KundenNr = kunde.KundenNr,
                Vorname = kunde.Vorname,
                Nachname = kunde.Nachname,
                Firma = kunde.Firma,
                Email = kunde.Email,
                Passwort = kunde.Passwort,
                Website = kunde.Website,
                GueltigAb = kunde.GueltigAb,
                GueltigBis = kunde.GueltigBis,

                AdressId = adresse.Id,
                Strasse = adresse.Strasse,
                HausNr = adresse.HausNr,
                AdressGueltigAb = adresse.GueltigAb,
                AdressGueltigBis = adresse.GueltigBis,

                OrtschaftId = ortschaft.Id,
                PLZ = ortschaft.PLZ,
                Ort = ortschaft.Ort
            };



            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string serialized = JsonSerializer.Serialize(exportObjekt, options);

          //  serialized += JsonSerializer.Serialize(adresse, options);


            //Auf KundenAdresse model ändern
            modelKunde.speicherJson(serialized);

            return true;
        }
    }
}
