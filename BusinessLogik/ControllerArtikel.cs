using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;

namespace BusinessLogik
{
    public class ControllerArtikel
    {
        private ModelArtikel modelArtikel = new ModelArtikel();


        public void testartikelanlegen()
        {

            Artikel a1 = new Artikel()
            {
                Bezeichnung = "Trizeps",
                ArtikelNr = 5,
                PreisNetto = 50.50m,
                Aktiv = true,
                ArtikelgruppeId = 1
            };
            modelArtikel.artikelspeichern(a1);
            
            Artikel a2 = new Artikel()
            {
                Bezeichnung = "Discopumper",
                ArtikelNr = 5,
                PreisNetto = 50.50m,
                Aktiv = true,
                ArtikelgruppeId = 1
            };
            modelArtikel.artikelspeichern(a2);

        }

        public bool NeuerArtieklAnlegen(Artikel artikel)
        {

         
            modelArtikel.artikelspeichern(artikel);

            return true;
        }

        public List<Artikel> LadeArtikel()
        {
            return ModelArtikel.LadeArtikel();
        }

        public int GetCounterArtikel(string sqlcommand)
        {
            return modelArtikel.GetCounterArtikel(sqlcommand);
        }

        public void DeleteArtikel(int artikelid)
        {
            modelArtikel.DeleteArtikel(artikelid);
        }
    }
}
