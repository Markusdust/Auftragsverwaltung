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
        public int GetCounterArtikelGruppe(string sqlcommand)
        {
            return modelArtikelgruppe.GetCounterArtikelgruppe(sqlcommand);
        }

        public void ArtikelGruppeAnlegen(string name, bool aktiv)
        {
            // Artikelgruppe ag1 = new Artikelgruppe()
            // {
            //     Name = "Werkzeug",
            //     Active = true
            // };
            // modelArtikelgruppe.artikelgruppespeichern(ag1);
            //
            // Artikelgruppe ag2 = new Artikelgruppe()
            // {
            //     Name = "Schmuck",
            //     Active = true
            // };
            Artikelgruppe ag1 = new Artikelgruppe()
            {
                Name = name,
                Active = aktiv
            };
            modelArtikelgruppe.artikelgruppespeichern(ag1);
        }

        public List<Artikelgruppe> LadeArtikelgruppe()
        {
            return ModelArtikelgruppe.LadeArtikelGruppe();
        }

        // public object GetTable(string sqlcommand)
        // {
        //     return modelArtikelgruppe.GetTable(sqlcommand);
        // }
    }
}