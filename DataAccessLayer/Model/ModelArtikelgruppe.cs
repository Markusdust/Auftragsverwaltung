using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataAccessLayer.Entities;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Model
{
    public class ModelArtikelgruppe
    {
        private static List<Artikelgruppe> artikelgruppelist;

        public bool artikelgruppespeichern(Artikelgruppe artikelgruppe)
        {
            using (var context = new AuftragContext())
            {
                context.Artikelgruppe.Add(artikelgruppe);
                context.SaveChanges();
                return true;
            }
        }

        public static List<Artikelgruppe> LadeArtikelGruppe()
        {
            using (AuftragContext context = new AuftragContext())
            {

                artikelgruppelist = context.Artikelgruppe.ToList();

            }

            return artikelgruppelist;
        }

        public bool ArtikelGruppeLöschen(int artikelgruppeId)
        {
            using (AuftragContext context = new AuftragContext())
            {
                var artikelgruppe = context.Artikelgruppe.SingleOrDefault(a => a.Id == artikelgruppeId);

                context.Artikelgruppe.Remove(artikelgruppe);
                context.SaveChanges();

                return true;
            }
        }

        public bool Aendere(Artikelgruppe artikelgruppe)
        {
            using (var context = new AuftragContext())
            {
                var updateartikelgruppe = context.Artikelgruppe.Where(a => a.Id == artikelgruppe.Id).SingleOrDefault();
                if (updateartikelgruppe != null)
                {
                    updateartikelgruppe.Name = artikelgruppe.Name;
                    updateartikelgruppe.Active = artikelgruppe.Active;

                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public List<Artikelgruppe> SucheArtikelgruppe(string name)
        {
            using (var context = new AuftragContext())
            {
                artikelgruppelist = context.Artikelgruppe.Where(a => a.Name == name).ToList();
                return artikelgruppelist;
            }
                
        }
        public bool ArtikelGruppenReferenzCheck(int artikelgruppeId)
        {
            using (var context = new AuftragContext())
            {
                bool check = false;
                var listArtikel = context.Artikel.ToList();
                foreach (var Artikel in listArtikel)
                {
                    if (check = Artikel.ArtikelgruppeId.Equals(artikelgruppeId) == true)
                        break;
                }

                return check;
            }
        }

        public int ArtikelgruppeID(string name)
        {
            using (var context = new AuftragContext())
            {
                var ag = context.Artikelgruppe.Where(ag => ag.Name == name).SingleOrDefault();
                return ag.Id;
            }
        }
    }
}