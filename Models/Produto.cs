namespace AbissalSystem.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Double Preco { get; set; }
        public Empresa Empresa { get; set; }        
    }
}