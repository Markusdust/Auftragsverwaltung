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

        public bool AlterAuftragBearbeiten(int id, int auftragsNr, DateTime datum, int kundeId)
        {
            Auftrag a1 = new Auftrag()
            {
                Id = id,
                AuftragsNr = auftragsNr,
                Datum = datum,
                KundeId = kundeId
            };
            modelAuftrag.aendern(a1);
            return true;
        }

        public bool NeuePositionAnlegen(int positionNr, int menge, int auftragId, int artikelId)
        {
            Position p1 = new Position()
            {                
                PositionNr = positionNr,
                Menge = menge,
                AuftragId = auftragId,
                ArtikelId = artikelId                
            };
            modelPosition.speichern(p1);
            return true;
        }

        public bool AltePositionBearbeiten(int positionNr, int menge, int auftragId, int artikelId)
        {
            Position p1 = new Position()
            {
                PositionNr = positionNr,
                Menge = menge,
                AuftragId = auftragId,
                ArtikelId = artikelId
            };
            modelPosition.aendern(p1);
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

        public List<Position> LadeTeilPositionen(int auftragsId)
        {
            return modelPosition.LadeTeilPositionen(auftragsId);
        }
    }
}
