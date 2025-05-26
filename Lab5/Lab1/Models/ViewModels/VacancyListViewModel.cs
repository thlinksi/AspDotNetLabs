namespace Lab5.Models.ViewModels
{
    public class VacancyViewModel
    {
        public long VacancyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Category { get; set; } = string.Empty;
    }

    public class VacanciesListViewModel
    {
        public IEnumerable<VacancyViewModel> Vacancies { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}