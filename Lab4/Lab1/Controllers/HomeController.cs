using Lab4.Models;
using Lab4.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobRepository repository;
        private readonly int pageSize = 2;

        public HomeController(IJobRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(string category = null, int page = 1)
        {
            var vacanciesQuery = repository.Vacancies
                .Where(v => category == null || v.Category == category)
                .OrderBy(v => v.VacancyId);

            var totalItems = vacanciesQuery.Count();

            var pagedVacancies = vacanciesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(v => new VacancyViewModel
                {
                    VacancyId = v.VacancyId,
                    Title = v.Title,
                    Description = v.Description,
                    Company = v.Company,         
                    Salary = v.Salary,          
                    Category = v.Category
                });

            var viewModel = new VacanciesListViewModel
            {
                Vacancies = pagedVacancies,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems
                },
                CurrentCategory = category
            };

            Console.WriteLine($"Rendering Home/Index with VacanciesListViewModel - Page: {page}, Category: {category ?? "All"}");
            return View("Index", viewModel);
        }
    }
}