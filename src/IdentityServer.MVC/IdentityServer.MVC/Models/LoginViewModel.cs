using System.ComponentModel.DataAnnotations;

namespace IdentityServer.MVC.Models
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
        }
        public LoginViewModel(string returnUrl) : this()
        {
            ReturnUrl = returnUrl;
        }
        public LoginViewModel(string username, string password, string returnUrl) : this(returnUrl)
        {
            Username = username;
            Password = password;
        }

        [Required(ErrorMessage = "Obrigatório informar o Usuário")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a Senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
