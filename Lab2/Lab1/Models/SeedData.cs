using Microsoft.EntityFrameworkCore;

namespace Lab2.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            JobDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<JobDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
                Console.WriteLine("Database migrations applied");
            }

            if (!context.Employers.Any())
            {
                context.Employers.AddRange(
                    new Employer { EmployerId = "emp1", CompanyName = "TechCorp" },
                    new Employer { EmployerId = "emp2", CompanyName = "Innovate Ltd" },
                    new Employer { EmployerId = "emp3", CompanyName = "DataWorks" }
                );
                context.SaveChanges();
                Console.WriteLine("Employers added");
            }

            if (!context.Vacancies.Any())
            {
                context.Vacancies.AddRange(
                    new Vacancy
                    {
                        Title = "Software Engineer",
                        Description = "Develop web applications",
                        Company = "TechCorp",
                        Salary = 50000,
                        EmployerId = "emp1"
                    },
                    new Vacancy
                    {
                        Title = "Project Manager",
                        Description = "Manage IT projects",
                        Company = "Innovate Ltd",
                        Salary = 60000,
                        EmployerId = "emp2"
                    },
                    new Vacancy
                    {
                        Title = "Data Analyst",
                        Description = "Analyze business data",
                        Company = "DataWorks",
                        Salary = 45000,
                        EmployerId = "emp3"
                    }
                );
                context.SaveChanges();
                Console.WriteLine("Vacancies added");
            }
            if (!context.Applications.Any())
            {
                context.Applications.AddRange(
                    new Application
                    {
                        VacancyId = 1, 
                        CandidateId = "candidate-id",
                        Resume = "Test resume for Software Engineer",
                        AppliedDate = DateTime.Now
                    },
                    new Application
                    {
                        VacancyId = 2, 
                        CandidateId = "candidate-id",
                        Resume = "Test resume for Project Manager",
                        AppliedDate = DateTime.Now.AddDays(-1)
                    }
                );
                context.SaveChanges();
                Console.WriteLine("Test applications added");
            }
        }
    }
}