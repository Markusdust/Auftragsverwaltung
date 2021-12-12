using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer
{
   public class ModelKunde
    {
        public bool kundespeichern(Kunde kunde)
        {
            using (var context = new AuftragContext())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
                return true;
            }
        }
    }
}
