using System.Collections.Generic;

namespace AbissalSystem.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
         public List<Produto> Produtos { get; set; }
        
    }
}