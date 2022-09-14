
using System.Collections.Generic;

namespace AbissalSystem.Models
{
    public class Regra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IList<Usuario> Usuarios { get; set; }
    }
}