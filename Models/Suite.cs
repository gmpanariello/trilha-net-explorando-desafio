namespace DesafioProjetoHospedagem.Models
{
    public class Suite
    {
        public Suite() { }

        public Suite(string tipoSuite, int capacidade, decimal valorDiaria)
        {
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }

        public string TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }

        // Método estático que retorna as suítes predefinidas
        public static List<Suite> ObterSuítesDisponíveis()
        {
            return new List<Suite>

            {   new Suite("Individual Premium", 1, 25),
                new Suite("Individual Básica", 1, 15),
                new Suite("Casal Premium", 2, 45),
                new Suite("Casal Básica", 2, 30),
                new Suite("Família Premium", 6, 60),
                new Suite("Família Básica", 6, 40),
            };
        }
    }
}
