using System.Collections.Generic;

namespace AbissalSystem.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public List<Regra> Regras { get; set; }        
    }
}