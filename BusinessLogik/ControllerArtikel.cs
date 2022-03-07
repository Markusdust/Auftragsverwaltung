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


        public bool NeuerArtieklAnlegen(Artikel artikel)
        {
            modelArtikel.artikelspeichern(artikel);
            return true;
        }

        public List<Artikel> LadeArtikel()
        {
            return ModelArtikel.LadeArtikel();
        }

        public void DeleteArtikel(int artikelid)
        {
            modelArtikel.DeleteArtikel(artikelid);
        }

        public bool AendereArtikel(Artikel a1)
        {
            return modelArtikel.Aendere(a1);
        }

        public List<Artikel> SuchArtikel(string? bezeichnung,int? artikelgruppeid)
        {
            return modelArtikel.SuchArtikel(bezeichnung, artikelgruppeid);
        }
    }
}
