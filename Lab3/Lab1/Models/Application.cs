namespace Lab3.Models
{
    public class Application
    {
        public long ApplicationId { get; set; }
        public string CandidateId { get; set; } = string.Empty;
        public long VacancyId { get; set; }
        public string Resume { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}