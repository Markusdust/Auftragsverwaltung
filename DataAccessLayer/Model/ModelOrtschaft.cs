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


       public List<Ortschaft> LadeOrtschaft()
       {
           using (AuftragContext context = new AuftragContext())
           {
               meineOrtschaften = context.Ortschaften.ToList();
           }

           return meineOrtschaften;
       }
    }

   
}
