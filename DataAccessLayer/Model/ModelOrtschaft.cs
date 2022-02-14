using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Model
{
   public class ModelOrtschaft
   {
       private List<Ortschaft> meineOrtschaften;


       public bool speichern(Ortschaft ortschaft)
       {
           using (var context = new AuftragContext())
           {
               context.Ortschaften.Add(ortschaft);
               context.SaveChanges();
               return true;
           }
       }
       public List<Ortschaft> LadeOrtschaft()
       {
           using (var context = new AuftragContext())
           {
               meineOrtschaften = context.Ortschaften.ToList();
           }

           return meineOrtschaften;
       }

       public Ortschaft LadeOrtschaftZuAdresse(int OrtschaftId)
       {
           Ortschaft ortschaftZuAdresse;
           using (var context = new AuftragContext())
           {
               var queryMatchingOrtschaft = context.Adressen.Where(x =>
                       x.Ortschaft.Id.Equals(OrtschaftId) && x.GueltigBis.Equals(DateTime.MaxValue))
                   .Select(x => x.Ortschaft).First();

               ortschaftZuAdresse = queryMatchingOrtschaft;
           }

           return ortschaftZuAdresse;
       }
    }

   
}
