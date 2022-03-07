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

        public bool aendern(Position position)
        {
            using (var context = new AuftragContext())
            {
                context.Positionen.Update(position);
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

       

        //public List<Position> LadeTeilPositionen()
        //{
        //    using (AuftragContext context = new AuftragContext())
        //    {
        //        meinePositionen = context.Positionen.ToList();
        //    }
        //    return meinePositionen;
        //}

        public List<Position> LadeTeilPositionen(int auftragsId)
        {
           List<Position> queryPositionen;
            using (AuftragContext context = new AuftragContext())
            {
                queryPositionen = context.Positionen.Where
                     (x => x.AuftragId.Equals(auftragsId)).Select(x=>x).ToList();
              
            }
            return queryPositionen;
        }

        public bool loeschen(int posId)
        {
            using (var context = new AuftragContext())
            {
                var position = context.Positionen.SingleOrDefault(a => a.Id == posId);

                context.Positionen.Remove(position);
                context.SaveChanges();
                return true;
            }
        }

        public List<Position> SuchePositionen(int input)
        {
            using (var context = new AuftragContext())
            {
                meinePositionen = context.Positionen
                    .Where(b => b.ArtikelId == input || b.AuftragId == input).ToList();
                return meinePositionen;
            }
        }
    }
}
