using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введіть логін")]
        public string Username { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}