using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<JobDbContext>();

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying migrations: {ex.Message}");
                throw;
            }

            if (!context.Vacancies.Any())
            {
                context.Vacancies.AddRange(
                    new Vacancy
                    {
                        Title = "Software Engineer",
                        Description = "Develop software solutions",
                        Company = "Tech Corp",
                        Salary = 60000,
                        Category = "IT"
                    },
                    new Vacancy
                    {
                        Title = "Project Manager",
                        Description = "Manage projects",
                        Company = "Business Inc",
                        Salary = 80000,
                        Category = "Management"
                    },
                    new Vacancy
                    {
                        Title = "Data Analyst",
                        Description = "Analyze data",
                        Company = "Data Co",
                        Salary = 55000,
                        Category = "Analytics"
                    }
                );
                context.SaveChanges();
                Console.WriteLine("SeedData: Added test vacancies to the database.");
            }

            if (!context.Applications.Any())
            {
                var vacancy1 = context.Vacancies.FirstOrDefault(v => v.Title == "Software Engineer");
                var vacancy2 = context.Vacancies.FirstOrDefault(v => v.Title == "Project Manager");

                if (vacancy1 != null && vacancy2 != null)
                {
                    context.Applications.AddRange(
                        new Application
                        {
                            CandidateId = "candidate-id",
                            VacancyId = vacancy1.VacancyId,
                            Resume = "Resume for Software Engineer",
                            AppliedDate = DateTime.Now
                        },
                        new Application
                        {
                            CandidateId = "candidate-id",
                            VacancyId = vacancy2.VacancyId,
                            Resume = "Resume for Project Manager",
                            AppliedDate = DateTime.Now
                        }
                    );
                    context.SaveChanges();
                    Console.WriteLine("SeedData: Added test applications to the database.");
                }
            }
        }
    }
}