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

        public void ArtikelGruppeAnlegen(string name, bool aktiv, int? uebergeortneteAg)
        {
            Artikelgruppe ag1 = new Artikelgruppe()
            {
                Name = name,
                Active = aktiv,
                UebergeordneteGruppeId = uebergeortneteAg
            };
            modelArtikelgruppe.artikelgruppespeichern(ag1);
        }

        public List<Artikelgruppe> LadeArtikelgruppe()
        {
            return ModelArtikelgruppe.LadeArtikelGruppe();
        }

        public bool ArtikelGruppeLöschen(Artikelgruppe artikelgruppe)
        {
            if (artikelgruppe == null)
                return false;
            else if (modelArtikelgruppe.ArtikelGruppenReferenzCheck(artikelgruppe) == true)
                return false;
            else
            {
                modelArtikelgruppe.ArtikelGruppeLöschen(artikelgruppe);
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

        public int? GetUebergeortneteAG()
        {
            return modelArtikelgruppe.GetUebergeortneteAG();
        }

        public List<Artikelgruppe> LadeCte()
        {
            return modelArtikelgruppe.ladeCte();
        }
    }
}