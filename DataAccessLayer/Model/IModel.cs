using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    interface IModel
    {
        public bool speichern(object test);
        public bool aendern();
        public bool loeschen();

    }
}
