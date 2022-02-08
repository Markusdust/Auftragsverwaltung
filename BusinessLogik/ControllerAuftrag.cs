﻿using DataAccessLayer.Entities;
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
        private ModelPosition modelPosition = new ModelPosition();

        public bool NeuerAuftragAnlegen(int auftragsNr, DateTime datum, int kundeId)
        {
            Auftrag a1 = new Auftrag()
            {
                AuftragsNr = auftragsNr,
                Datum = datum,
                KundeId = kundeId
            };
            modelAuftrag.speichern(a1);
            return true;
        }

        public bool NeuerArtikelAnlegen(int id, int positionNr, int menge, int auftragId, int artikelId)
        {
            Position p1 = new Position()
            {
                Id = id,
                PositionNr = positionNr,
                Menge = menge,
                AuftragId = auftragId,
                ArtikelId = artikelId                
            };
            modelPosition.speichern(p1);
            return true;
        }

        public List<Auftrag> LadeAuftraege()
        {
            return modelAuftrag.LadeAuftraege();
        }

        public List<Position> LadePositionen()
        {
            return modelPosition.LadePositionen();
        }
    }
}
