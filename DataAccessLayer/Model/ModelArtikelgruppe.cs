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

        public int GetCounterArtikelgruppe(string sqlcommand)
        {

            using (var context = new AuftragContext())
            {
                int count = 0;
                // string sqlgetcountstring = "SELECT COUNT(Id) FROM ARTIKELGRUPPE";

                using (SqlCommand cmdCount = new SqlCommand(sqlcommand))
                {
                    count = context.GetCountColumn(sqlcommand);

                }
                return count +1;
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

        // public static string[] GetTable(string sqlcommand)
        // {
        //     string[] table;
        //     using (var context = new AuftragContext())
        //     {
        //         int count = 0;
        //         // string sqlgetcountstring = "SELECT COUNT(Id) FROM ARTIKELGRUPPE";
        //
        //         using (SqlCommand cmdCount = new SqlCommand(sqlcommand))
        //         {
        //             table = context.GetCountColumn(sqlcommand);
        //
        //         }
        //         return table ;
        //     }
        // }
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

        // public void DeleteArtikel(int artikelid)
        // {
        //     using (AuftragContext context = new AuftragContext())
        //     {
        //         var artikel = context.Artikel.SingleOrDefault(a => a.Id == artikelid);
        //         if (artikel == null) return;
        //
        //         context.Artikel.Remove(artikel);
        //         context.SaveChanges();
        //     }
        // }
    }
}