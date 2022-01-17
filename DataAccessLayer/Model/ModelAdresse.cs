using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
    public class ModelAdresse 
    {
        public bool speichern(Adresse adresse)
        {
            using (var context = new AuftragContext())
            {
                context.Adressen.Add(adresse);
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
