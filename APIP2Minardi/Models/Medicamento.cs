using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string ?Nome { get; set; }
        public string ?Dosagem { get; set; }
        public string ?Via_Administracao { get; set; }
        public int Estoque { get; set; }

        public ICollection<PrescricaoMedicamento> ?PrecricaoMedicamentos { get; set; }

        public Medicamento() { }

        public Medicamento(string? nome, string? dosagem, string? via_administracao, int estoque)
        {
            Nome = nome;
            Dosagem = dosagem;
            Via_Administracao = via_administracao;
            Estoque = estoque;
        }
    }
}
