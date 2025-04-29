namespace Lab4.Models
{
    public interface IJobRepository
    {
        IQueryable<Vacancy> Vacancies { get; }
        IQueryable<Lab4.Models.Application> Applications { get; }

        void AddVacancy(Vacancy vacancy);
        void UpdateVacancy(Vacancy vacancy);
        void DeleteVacancy(Vacancy vacancy);

        void AddApplication(Lab4.Models.Application application);
        void UpdateApplication(Lab4.Models.Application application);
        void DeleteApplication(Lab4.Models.Application application);
    }
}