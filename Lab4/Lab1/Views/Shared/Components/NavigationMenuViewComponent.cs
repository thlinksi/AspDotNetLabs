using Microsoft.AspNetCore.Mvc;
using Lab4.Models;
using Lab4.Models.ViewModels;

namespace Lab4.Components
{
    [ViewComponent(Name = "NavigationMenuViewComponent")]
    public class NavigationMenuComponent : ViewComponent
    {
        private readonly IJobRepository repository;

        public NavigationMenuComponent(IJobRepository repo)
        {
            repository = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = repository.Vacancies
                .Select(v => v.Category)
                .Distinct()
                .ToList();

            string candidateId = "candidate-id";
            var applicationCount = repository.Applications
                .Count(a => a.CandidateId == candidateId);

            var viewModel = new NavigationMenuViewModel
            {
                Categories = categories,
                ApplicationCount = applicationCount
            };

            Console.WriteLine($"NavigationMenuViewComponent: Found {categories.Count} categories: {string.Join(", ", categories)}");
            return View(viewModel);
        }
    }

    public class NavigationMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public int ApplicationCount { get; set; }
    }
}
