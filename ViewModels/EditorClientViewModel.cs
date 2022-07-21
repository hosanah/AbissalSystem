using System.ComponentModel.DataAnnotations;

namespace AbissalSystem.ViewModels
{
    public class EditorClientViewModel
    {
        [Required(ErrorMessage ="Campo nome completo é obrigatório!")]
        [MinLength(3, ErrorMessage ="Este campo deve contar o mínimo de 3 letras!")]
        public string FullName { get; set; }
        [MinLength(3, ErrorMessage ="Este campo deve contar o mínimo de 3 letras!")]
        [Required(ErrorMessage ="Campo apelido é obrigatório!")]
        public string Nickname { get; set; }
        [Required(ErrorMessage ="Campo data de nascimento é obrigatório!")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage ="Campo número de telefone é obrigatório!")]
        public string CallPhone { get; set; }
        [Required(ErrorMessage ="Campo email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Você não forneceu um e-mail válido!")]
        public string Email { get; set; }
    }
}