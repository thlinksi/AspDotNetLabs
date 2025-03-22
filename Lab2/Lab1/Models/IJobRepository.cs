namespace Lab2.Models
{
    public interface IJobRepository
    {
        IQueryable<Vacancy> Vacancies { get; }
        void AddVacancy(Vacancy vacancy);
        void AddApplication(Application application);
        IQueryable<Application> Applications { get; }
        void UpdateApplication(Application application);
        void DeleteApplication(Application application);
    }
}
