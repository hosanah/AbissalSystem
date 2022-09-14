namespace AbissalSystem.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Apelido { get; set; }
        public DateTime DataAniversario { get; set; }
        public string NumeroCelular { get; set; }
        public string Email { get; set; }
        
    }
}