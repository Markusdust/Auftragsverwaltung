﻿using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Ortschaft
    {
        public int Id { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
        public bool Aktiv { get; set; }

        public ICollection<Adresse> Adressen { get; set; }
    }
}
