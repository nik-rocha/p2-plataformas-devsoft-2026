using System;

namespace APIP2Minardi.Models
{
    public class PrescricaoMedicamento
    {
        public int Id { get; set; }
        public string ?Quantidade { get; set; }
        public string ?Frequencia { get; set; }
        public TimeSpan Horario { get; set; }
        public int PRESCRICAO_id {  get; set; }
        public int MEDICAMENTO_id { get; set; }

        public PrescricaoGeral ?PrescricaoGeral { get; set; }
        public Medicamento? Medicamentos { get; set; }
        public PrescricaoMedicamento() { }

        public PrescricaoMedicamento(string? quantidade, string? frequencia, TimeSpan horario, int prescricao_id, int medicamento_id)
        {
            Quantidade = quantidade;
            Frequencia = frequencia;
            Horario = horario;
            PRESCRICAO_id = prescricao_id;
            MEDICAMENTO_id = medicamento_id;
        }
    }
}
