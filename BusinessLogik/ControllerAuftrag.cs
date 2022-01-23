using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace BusinessLogik
{
    public class ControllerAuftrag
    {
        private ModelAuftrag modelAuftrag = new ModelAuftrag();

        public bool NeuerAuftragAnlegen(int auftragsNr, DateTime datum, int kundenId)
        {
            Auftrag a1 = new Auftrag()
            {
                AuftragsNr = auftragsNr,
                Datum = datum,
                KundenId = kundenId
            };
            modelAuftrag.speichern(a1);
            return true;
        }

        public List<Auftrag> LadeAuftraege()
        {
            return modelAuftrag.LadeAuftraege();
        }
    }
}
