using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Model
{
    public class ModelArtikel
    {
        private static List<Artikel> artikellist;
        public bool artikelspeichern(Artikel artikel)
        {
            using (var context = new AuftragContext())
            {
                context.Artikel.Add(artikel);
                context.SaveChanges();
                return true;
            }
        }


        public int GetCounterArtikel()
        {

            using (var context = new AuftragContext())
            {

                string sqlgetcountstring = "SELECT COUNT(Id) FROM ARTIKEL";
                int count = context.GetCountColumn(sqlgetcountstring);

                // using (SqlCommand cmdCount = new SqlCommand(sqlgetcountstring))
                // {
                //     count = context.GetCountColumn(sqlgetcountstring);
                //     
                // }

                return count;
            }

        }
        // public int GetCountArtikel()
        // {
        //     int count = 0;
        //     using (SqlConnection connection = new SqlConnection(connectionstring))
        //     {
        //
        //         string sqlgetcountstring = "SELECT COUNT(Id) FROM ARTIKEL";
        //
        //         using (SqlCommand cmdCount = new SqlCommand(sqlgetcountstring, connection))
        //         {
        //             connection.Open();
        //             count = (int)cmdCount.ExecuteScalar();
        //         }
        //
        //         return count;
        //     }
        // }
        public static List<Artikel> LadeArtikel()
        {
            using (AuftragContext context = new AuftragContext())
            {
                artikellist = context.Artikel.ToList();
            }
            return artikellist;

        }

    }
}
