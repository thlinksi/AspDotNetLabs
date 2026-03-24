using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть логін")]
        [MinLength(3, ErrorMessage = "Логін має містити мінімум 3 символи")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        [MinLength(8, ErrorMessage = "Пароль має містити мінімум 8 символів")]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Role { get; set; } = "User";
    }
}