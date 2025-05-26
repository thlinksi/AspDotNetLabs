using System.ComponentModel.DataAnnotations;

namespace Lab5.Models.ViewModels
{
    public class EditProfileModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}