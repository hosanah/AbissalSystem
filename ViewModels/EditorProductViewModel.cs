using System.ComponentModel.DataAnnotations;

namespace AbissalSystem.ViewModels
{
    public class EditorProductViewModel
    {
        [Required(ErrorMessage ="Campo nome é obrigatório!")]
        [MinLength(3, ErrorMessage ="Este campo deve contar o mínimo de 3 letras!")]
        public string Name { get; set; }
        [MinLength(3, ErrorMessage ="Este campo deve contar o mínimo de 1 letras!")]
        [Required(ErrorMessage ="Campo descrição é obrigatório!")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Preço é obrigatório!")]
        public double Price { get; set; }
        
    }
}