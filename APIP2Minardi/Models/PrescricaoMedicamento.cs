using System;

namespace APIP2Minardi.Models
{
    public class PrescricaoMedicamento
    {
        public int id { get; set; }
        public string ?Quantidade { get; set; }
        public string ?Frequencia { get; set; }
        public TimeSpan Horario { get; set; }
        public int PRESCRICAO_id {  get; set; }
        public int MEDICAMENTO_id { get; set; }

        public PrescricaoGeral ?PrescricaoGeral { get; set; }
        public Medicamento? Medicamentos { get; set; }
        public PrescricaoMedicamento() { }
    }
}
