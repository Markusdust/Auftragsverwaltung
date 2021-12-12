using System;
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
        private ModelArtikel modelArtikModel = new ModelArtikel();
        public void testartikelanlegen()
        {
            Artikel a1 = new Artikel()
            {
                // Id = ??
                Bezeichnung = "Hammer",
                ArtikelNr = 5,
                PreisNetto = 50.50m,
                Aktiv = true
            };
            modelArtikModel.artikelspeichern(a1);
        }

        public void testartikelgruppeanlegen()
        {
            Artikelgruppe agruppe1 = new Artikelgruppe()
            {
                Name = "TestGruppe",
                Active = true,
            };
            modelArtikModel.artikelgruppespeichern(agruppe1);
        }
    }
}
