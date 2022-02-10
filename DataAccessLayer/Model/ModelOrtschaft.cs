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
    }

   
}
