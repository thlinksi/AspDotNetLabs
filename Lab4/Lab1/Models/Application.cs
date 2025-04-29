using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Application
    {
        public long ApplicationId { get; set; }

        [Required(ErrorMessage = "Candidate ID is required")]
        public string CandidateId { get; set; } = string.Empty;

        public long VacancyId { get; set; }

        [Required(ErrorMessage = "Please enter a resume")]
        [StringLength(1000, ErrorMessage = "Resume cannot be longer than 1000 characters")]
        public string Resume { get; set; } = string.Empty;

        public DateTime AppliedDate { get; set; }

        public Vacancy Vacancy { get; set; }
    }
}