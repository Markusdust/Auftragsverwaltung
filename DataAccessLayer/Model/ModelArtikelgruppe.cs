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

        public int GetCounterArtikelgruppe()
        {

            using (var context = new AuftragContext())
            {
                int count = 0;
                string sqlgetcountstring = "SELECT COUNT(Id) FROM ARTIKELGRUPPE";

                using (SqlCommand cmdCount = new SqlCommand(sqlgetcountstring))
                {
                    count = context.GetCountColumn(sqlgetcountstring);

                }
                return count + 1;
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
    }
}