using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class ModelPosition
    {
        private List<Position> meinePositionen;

        public bool speichern(Position position)
        {
            using (var context = new AuftragContext())
            {
                context.Positionen.Add(position);
                context.SaveChanges();
                return true;
            }
        }

        public List<Position> LadePositionen()
        {
            using (AuftragContext context = new AuftragContext())
            {
                meinePositionen = context.Positionen.ToList();
            }
            return meinePositionen;
        }
    }
}
