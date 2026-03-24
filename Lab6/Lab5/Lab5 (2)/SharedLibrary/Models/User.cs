using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть логін")]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Role { get; set; } = "User";
    }
}