using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class Setor
    {
        public int Id { get; set; }
        public string ?Nome { get; set; }
        public string ?Andar { get; set; }
        public string ?Descricao { get; set; }

        public ICollection<Paciente> Pacientes { get; set; }

        public Setor() { }

        public Setor(string nome, string andar, string descricao)
        {
            Nome = nome;
            Andar = andar;
            Descricao = descricao;
        }
    }
}
