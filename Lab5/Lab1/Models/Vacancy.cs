using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Lab5.Models
{
    public class Vacancy
    {
        public long VacancyId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a company name")]
        [StringLength(50, ErrorMessage = "Company name cannot be longer than 50 characters")]
        public string Company { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a salary")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters")]
        public string Category { get; set; } = string.Empty;

        public ICollection<Lab5.Models.Application> Applications { get; set; } = new List<Lab5.Models.Application>();
    }
}