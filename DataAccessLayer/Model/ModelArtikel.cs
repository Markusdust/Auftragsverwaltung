using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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

        public static List<Artikel> LadeArtikel()
        {
            using (AuftragContext context = new AuftragContext())
            {
                artikellist = context.Artikel.ToList();
            }
            return artikellist;

        }

        public void DeleteArtikel(int  artikelid)
        {
            using (AuftragContext context = new AuftragContext())
            {
                var artikel = context.Artikel.SingleOrDefault(a => a.Id == artikelid);
                if (artikel == null) return;

                context.Artikel.Remove(artikel);
                context.SaveChanges();
            }
        }

        public bool Aendere(Artikel artikel)
        {
            using (AuftragContext context = new AuftragContext())
            {
                var updateartikel = context.Artikel.SingleOrDefault(a => a.Id == artikel.Id);
                if (updateartikel != null)
                {
                    updateartikel.Bezeichnung = artikel.Bezeichnung;
                    updateartikel.ArtikelNr = artikel.ArtikelNr;
                    updateartikel.Aktiv = artikel.Aktiv;
                    updateartikel.Mwst = artikel.Mwst;
                    updateartikel.PreisNetto = artikel.PreisNetto;
                    updateartikel.ArtikelgruppeId = artikel.ArtikelgruppeId;
                }
                context.SaveChanges();
                return true;
            }
        }

        public List<Artikel> SuchArtikel(string? bezeichnung,int? artikelgruppeid)
        {
            using (var context = new AuftragContext())
            {
                artikellist = context.Artikel.Where(a =>
                    a.Bezeichnung == bezeichnung ||
                    a.ArtikelgruppeId == artikelgruppeid).ToList();
                return artikellist;
            }
        }
    }
}
