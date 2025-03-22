namespace Lab2.Models.ViewModels
{
    public class VacancyListViewModel
    {
        public IEnumerable<Vacancy> Vacancies { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}