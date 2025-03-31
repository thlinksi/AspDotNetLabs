namespace Lab3.Models
{
    public class Vacancy
    {
        public long VacancyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Category { get; set; } = string.Empty;

        public ICollection<Lab3.Models.Application> Applications { get; set; } = new List<Lab3.Models.Application>();
    }
}