namespace Lab3.Models
{
    public interface IJobRepository
    {
        IQueryable<Vacancy> Vacancies { get; }
        IQueryable<Lab3.Models.Application> Applications { get; }

        void AddVacancy(Vacancy vacancy);
        void AddApplication(Lab3.Models.Application application);
        void UpdateApplication(Lab3.Models.Application application);
        void DeleteApplication(Lab3.Models.Application application);
    }
}