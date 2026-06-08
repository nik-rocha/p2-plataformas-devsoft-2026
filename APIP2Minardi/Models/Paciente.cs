using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class Paciente
    {
        public int id { get; set; }
        public string ?Nome { get; set; }
        public string ?CPF { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string ?Leito { get; set; }

        public int SETOR_id { get; set; }

        public Setor ?Setor { get; set; }
        public ICollection<PrescricaoGeral> PrescricoesGerais { get; set; }

        public Paciente() { }
    }
}
