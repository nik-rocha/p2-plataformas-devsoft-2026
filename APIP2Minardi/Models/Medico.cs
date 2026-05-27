namespace APIP2Minardi.Models
{
    public class Medico
    {
        public int id { get; set; }
        public string ?Nome { get; set; }
        private string ?_crm;
        public string CRM
        {
            get {  return _crm; }
            set { _crm = value; }
        }
        public string ?Especialidade { get; set; }
        public string ?Telefone { get; set; }

        public ICollection<PrescricaoGeral> ?PrescricaosGerais { get; set; }

        public Medico() { }
    }
}
