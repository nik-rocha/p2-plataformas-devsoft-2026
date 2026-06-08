using System;
using System.Collections.Generic;

namespace APIP2Minardi.Models
{
    public class PrescricaoGeral
    {
        public int id { get; set; }
        public DateTime Data {  get; set; }
        public string ?Observacao { get; set; }
        public int MEDICO_id { get; set; }
        public int PACIENTE_id { get; set; }

        public Paciente ?Paciente { get; set; }
        public Medico ?Medico { get; set; }

        public ICollection<PrescricaoMedicamento> ?PrecricaoMedicamentos { get; set; }

        public PrescricaoGeral() { }
    }
}
