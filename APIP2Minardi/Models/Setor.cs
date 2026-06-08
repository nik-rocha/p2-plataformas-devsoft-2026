using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class Setor
    {
        public int id { get; set; }
        public string ?Nome { get; set; }
        public string ?Andar { get; set; }
        public string ?Descricao { get; set; }

        public ICollection<Paciente> Pacientes { get; set; }

        public Setor() { }
    }
}
