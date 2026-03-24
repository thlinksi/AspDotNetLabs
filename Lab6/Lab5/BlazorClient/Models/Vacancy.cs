using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву вакансії")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть опис")]
        public string Description { get; set; } = string.Empty;

    }
}