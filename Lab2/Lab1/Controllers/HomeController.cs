using Lab2.Models;
using Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
        private IJobRepository repository;
        private int pageSize = 2; 

        public HomeController(IJobRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(int page = 1)
        {
            var vacancies = repository.Vacancies
                .OrderBy(v => v.VacancyId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = repository.Vacancies.Count()
            };

            var viewModel = new VacancyListViewModel
            {
                Vacancies = vacancies,
                PagingInfo = pagingInfo
            };

            return View(viewModel);
        }
    }
}