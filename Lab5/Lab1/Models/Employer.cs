namespace Lab5.Models
{
    public class Employer
    {
        public string EmployerId { get; set; } 
        public string CompanyName { get; set; } = String.Empty;
        public ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
    }
}