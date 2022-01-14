using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
    public class ModelKunde  //:IModel
    {
        public bool speichern(Kunde kunde)
        {
            using (var context= new AuftragContext())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
                return true;
            }
        }

        public bool aendern()
        {
            throw new NotImplementedException();
        }

        public bool loeschen()
        {
            throw new NotImplementedException();
        }

        
    }
}
