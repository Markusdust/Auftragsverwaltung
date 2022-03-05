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

        public void ArtikelGruppeAnlegen(string name, bool aktiv)
        {
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

        public bool ArtikelGruppeLöschen(int artikelgruppeId)
        {
            if (artikelgruppeId == null)
                return false;
            else if (modelArtikelgruppe.ArtikelGruppenReferenzCheck(artikelgruppeId) == true)
                return false;
            else
            {
                modelArtikelgruppe.ArtikelGruppeLöschen(artikelgruppeId);
                return true;
            }

        }

        public bool ArtikelGruppeÄndern(Artikelgruppe artikelgruppe)
        {
            return modelArtikelgruppe.Aendere(artikelgruppe);
        }

        public List<Artikelgruppe> SucheArtikelgruppe(string name)
        {
            return modelArtikelgruppe.SucheArtikelgruppe(name);
        }

        public int ArtikelGruppeID(string name)
        {
           return  modelArtikelgruppe.ArtikelgruppeID(name);
        }
    }
}