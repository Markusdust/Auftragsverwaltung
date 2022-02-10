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

        public List<Ortschaft> LadeOrtschaften()
        {
            return modelOrtschaft.LadeOrtschaft();
        }
    }

   
}
