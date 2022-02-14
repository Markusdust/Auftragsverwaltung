using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;

namespace BusinessLogik
{
   public class ControllerOrtschaft
    {
        private ModelOrtschaft modelOrtschaft = new ModelOrtschaft();

        public bool NeueOrtschaftAnlegen(int postleitzahl, string ortschaft)
        {
            Ortschaft o1 = new Ortschaft()
            {
                PLZ = postleitzahl,
                Ort = ortschaft,
                Aktiv = true,
            };
            modelOrtschaft.speichern(o1);
            return true;
        }

        public List<Ortschaft> LadeOrtschaften()
        {
            return modelOrtschaft.LadeOrtschaft();
        }

        public Ortschaft OrtschaftZuAdresse(int ortschaftId)
        {
            return modelOrtschaft.LadeOrtschaftZuAdresse(ortschaftId);
        }
    }

   
}
