using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Models
{
    public class Vacancy
    {
        public long? VacancyId { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Company { get; set; } = String.Empty;
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Salary { get; set; }
        public string EmployerId { get; set; } 
        public Employer Employer { get; set; } 
    }
}