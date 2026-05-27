namespace APIP2Minardi.Models
{
    public class Medicamento
    {
        public int id { get; set; }
        public string ?Nome { get; set; }
        public string ?Dosagem { get; set; }
        public string ?Via_Administracao { get; set; }
        public int Estoque { get; set; }

        public ICollection<PrescricaoMedicamento> ?PrecricaoMedicamentos { get; set; }

        public Medicamento() { }


    }
}
