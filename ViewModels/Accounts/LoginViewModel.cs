using System.ComponentModel.DataAnnotations;

namespace AbissalSystem.ViewModels.Accounts;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o usuário")]
    [MinLength(3, ErrorMessage = "O usuário deve conter no mínimo 3 caracteres!")]     
    public string Usuario { get; set; }
    
    [Required(ErrorMessage = "Informe a senha")]
    [MinLength(3, ErrorMessage = "A senha deve conter no mínimo 6 caracteres!")]    
    public string Senha { get; set; }
}