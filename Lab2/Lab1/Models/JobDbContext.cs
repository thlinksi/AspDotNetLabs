using Microsoft.EntityFrameworkCore;

namespace Lab2.Models
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options) { }
        public DbSet<Vacancy> Vacancies => Set<Vacancy>();
        public DbSet<Employer> Employers => Set<Employer>();
        public DbSet<Application> Applications => Set<Application>();
    }
}