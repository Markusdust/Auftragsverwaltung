using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace BusinessLogik
{

    public class Controller
    {
        private Model model = new Model();


        //int id, int kundenNr,string vorname, string nachname, string firma, DateTime gueltigAb, DateTime guelitigbis 
        public void testkundeanlegen()
        {
            Kunde k1 = new Kunde()
            {
               // Id = 12,
                KundenNr = 99,
                Vorname = "Jürgen",
                Nachname = "Hans",
                Firma = "Zbw",
                GueltigAb = DateTime.Now,
                GueltigBis = DateTime.Now
            };

            model.kundespeichern(k1);
        }
        
    }
}
