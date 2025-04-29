namespace Lab4.Models
{
    public class EFJobRepository : IJobRepository
    {
        private readonly JobDbContext context;

        public EFJobRepository(JobDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Vacancy> Vacancies => context.Vacancies;
        public IQueryable<Lab4.Models.Application> Applications => context.Applications;

        public void AddVacancy(Vacancy vacancy)
        {
            context.Vacancies.Add(vacancy);
            context.SaveChanges();
        }

        public void UpdateVacancy(Vacancy vacancy)
        {
            context.Vacancies.Update(vacancy);
            context.SaveChanges();
        }

        public void DeleteVacancy(Vacancy vacancy)
        {
            context.Vacancies.Remove(vacancy);
            context.SaveChanges();
        }

        public void AddApplication(Lab4.Models.Application application)
        {
            Console.WriteLine($"Adding application: CandidateId={application.CandidateId}, VacancyId={application.VacancyId}, Resume={application.Resume}, AppliedDate={application.AppliedDate}");
            context.Applications.Add(application);
            int rowsAffected = context.SaveChanges();
            Console.WriteLine($"Rows affected: {rowsAffected}");
            if (rowsAffected > 0)
            {
                Console.WriteLine("Application successfully added to database");
            }
            else
            {
                Console.WriteLine("Failed to add application to database");
            }
        }

        public void UpdateApplication(Lab4.Models.Application application)
        {
            Console.WriteLine($"Updating application: ApplicationId={application.ApplicationId}, Resume={application.Resume}, AppliedDate={application.AppliedDate}");
            context.Applications.Update(application);
            int rowsAffected = context.SaveChanges();
            Console.WriteLine($"Rows affected: {rowsAffected}");
            if (rowsAffected > 0)
            {
                Console.WriteLine("Application successfully updated in database");
            }
            else
            {
                Console.WriteLine("Failed to update application in database");
            }
        }

        public void DeleteApplication(Lab4.Models.Application application)
        {
            Console.WriteLine($"Deleting application: ApplicationId={application.ApplicationId}, AppliedDate={application.AppliedDate}");
            context.Applications.Remove(application);
            int rowsAffected = context.SaveChanges();
            Console.WriteLine($"Rows affected: {rowsAffected}");
            if (rowsAffected > 0)
            {
                Console.WriteLine("Application successfully deleted from database");
            }
            else
            {
                Console.WriteLine("Failed to delete application from database");
            }
        }
    }
}