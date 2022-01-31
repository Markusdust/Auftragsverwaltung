using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;

namespace BusinessLogik
{
    public class ControllerArtikelGruppe
    {
        private ModelArtikelgruppe modelArtikelgruppe = new ModelArtikelgruppe();
        public int GetCounterArtikelGruppe()
        {
            return modelArtikelgruppe.GetCounterArtikelgruppe();
        }

        public void ArtikelGruppeAnlegen()
        {
            Artikelgruppe ag1 = new Artikelgruppe()
            {
                Name = "Werkzeug",
                Active = true
            };
            modelArtikelgruppe.artikelgruppespeichern(ag1);

            Artikelgruppe ag2 = new Artikelgruppe()
            {
                Name = "Schmuck",
                Active = true
            };
            modelArtikelgruppe.artikelgruppespeichern(ag2);
        }

        public List<Artikelgruppe> LadeArtikelgruppe()
        {
            return ModelArtikelgruppe.LadeArtikelGruppe();
        }
    }
}