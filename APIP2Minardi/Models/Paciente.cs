using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string ?Nome { get; set; }
        public string ?CPF { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string ?Leito { get; set; }

        public int SETOR_id { get; set; }

        public Setor ?Setor { get; set; }
        public ICollection<PrescricaoGeral> PrescricoesGerais { get; set; }

        public Paciente() { }

        public Paciente(string? nome, string? cpf, DateTime data_nascimento, string? leito, int setor_id)
        {
            Nome = nome;
            CPF = cpf;
            Data_Nascimento = data_nascimento;
            Leito = leito;
            SETOR_id = setor_id;
        }
    }
}
