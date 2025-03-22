namespace Lab2.Models
{
    public class Application
    {
        public long ApplicationId { get; set; }
        public long VacancyId { get; set; }
        public Vacancy? Vacancy { get; set; } 
        public string CandidateId { get; set; }
        public string Resume { get; set; } = String.Empty;
        public DateTime AppliedDate { get; set; }
    }
}