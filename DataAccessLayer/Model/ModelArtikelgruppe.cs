using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataAccessLayer.Entities;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public bool ArtikelGruppeLöschen(Artikelgruppe artikelgruppe)
        {
            using (AuftragContext context = new AuftragContext())
            {
                var refUebergeortArtikelgruppe =
                    context.Artikelgruppe.SingleOrDefault(a => a.UebergeordneteGruppeId == artikelgruppe.Id);
                if (refUebergeortArtikelgruppe != null)
                {
                    refUebergeortArtikelgruppe.UebergeordneteGruppeId = artikelgruppe.UebergeordneteGruppeId;
                }

                var ag = context.Artikelgruppe.SingleOrDefault(a => a.Id == artikelgruppe.Id);

                context.Artikelgruppe.Remove(ag);
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
        public bool ArtikelGruppenReferenzCheck(Artikelgruppe artikelgruppe)
        {
            using (var context = new AuftragContext())
            {
                bool check = false;
                var listArtikel = context.Artikel.ToList();
                foreach (var Artikel in listArtikel)
                {
                    if (check = Artikel.ArtikelgruppeId.Equals(artikelgruppe.Id) == true)
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

        public int? GetUebergeortneteAG()
        {
            List<Artikelgruppe> Ag = LadeArtikelGruppe();
            int? lastAg = null;

            for (int i = 0; i < Ag.Count; i++)
            {
                lastAg = artikelgruppelist[i].Id;
                if (lastAg == 0)
                    lastAg = null;
            }

            return lastAg;
        }

        public List<Artikelgruppe> ladeCte()
        {
            using (var context = new AuftragContext())
            {
                List<Artikelgruppe> AG = new List<Artikelgruppe>();

                var query = ";WITH CTE_Artikelgruppe(Id, Name, UebergeordneteGruppeId, Active, Produktlevel) " +
                            "AS(Select Id, Name, UebergeordneteGruppeId, Active, 0 As Produktlevel " +
                            "FROM Artikelgruppe WHERE UebergeordneteGruppeId IS NULL " +
                            "UNION ALL " +
                            "SELECT pn.Id, pn.Name, pn.UebergeordneteGruppeId, pn.Active, p1.Produktlevel + 1 " +
                            "FROM Artikelgruppe AS pn " +
                            "INNER JOIN CTE_Artikelgruppe AS p1 ON p1.Id = pn.UebergeordneteGruppeId) "
                            +
                            "SELECT Id, Name, UebergeordneteGruppeId, Active, Produktlevel " +
                            "FROM CTE_Artikelgruppe " +
                            "ORDER BY UebergeordneteGruppeId; ";

                var result = context.Artikelgruppe.FromSqlRaw(query);

                foreach (var item in result)
                {
                    AG.Add(item);
                }

                return AG;
            }

            
        }
    }
}