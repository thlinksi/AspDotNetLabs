using System.Linq;

namespace Lab5.Models
{
    public interface IJobRepository
    {
        IQueryable<Vacancy> Vacancies { get; }
        IQueryable<Lab5.Models.Application> Applications { get; }

        void AddVacancy(Vacancy vacancy);
        void UpdateVacancy(Vacancy vacancy);
        void DeleteVacancy(Vacancy vacancy);

        void AddApplication(Lab5.Models.Application application);
        void UpdateApplication(Lab5.Models.Application application);
        void DeleteApplication(Lab5.Models.Application application);
    }
}