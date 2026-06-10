using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class PrescricaoGeral
    {
        public int Id { get; set; }
        public DateTime Data {  get; set; }
        public string ?Observacao { get; set; }
        public int MEDICO_id { get; set; }
        public int PACIENTE_id { get; set; }

        public Paciente ?Paciente { get; set; }
        public Medico ?Medico { get; set; }

        public ICollection<PrescricaoMedicamento> ?PrecricaoMedicamentos { get; set; }

        public PrescricaoGeral() { }

        public PrescricaoGeral(DateTime data, string? observacao, int medico_id, int paciente_id)
        {
            Data = data;
            Observacao = observacao;
            MEDICO_id = medico_id;
            PACIENTE_id = paciente_id;
        }
    }
}
