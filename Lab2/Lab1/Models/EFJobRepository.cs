namespace Lab2.Models
{
    public class EFJobRepository : IJobRepository
    {
        private JobDbContext context;

        public EFJobRepository(JobDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Vacancy> Vacancies => context.Vacancies;
        public IQueryable<Application> Applications => context.Applications;

        public void AddVacancy(Vacancy vacancy)
        {
            context.Vacancies.Add(vacancy);
            context.SaveChanges();
        }

        public void AddApplication(Application application)
        {
            Console.WriteLine($"Adding application: CandidateId={application.CandidateId}, VacancyId={application.VacancyId}, Resume={application.Resume}");
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

        public void UpdateApplication(Application application)
        {
            Console.WriteLine($"Updating application: ApplicationId={application.ApplicationId}, Resume={application.Resume}");
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

        public void DeleteApplication(Application application)
        {
            Console.WriteLine($"Deleting application: ApplicationId={application.ApplicationId}");
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