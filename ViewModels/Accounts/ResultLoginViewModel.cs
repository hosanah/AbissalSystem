using AbissalSystem.Models;

namespace AbissalSystem.ViewModels.Accounts;
public class ResultLoginViewModel
    {
        public Usuario Usuario { get; set; }
        public string token { get; set; }

         public ResultLoginViewModel(Usuario _usuario, string _token)
        {
            Usuario = _usuario;
            token = _token;
        }
    }