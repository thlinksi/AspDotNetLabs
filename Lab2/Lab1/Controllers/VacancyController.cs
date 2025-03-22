using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class VacancyController : Controller
    {
        private readonly IJobRepository repository;

        public VacancyController(IJobRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult Apply(long id)
        {
            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();
            return View(new Application { VacancyId = id });
        }

        [HttpPost]
        public IActionResult Apply(Application application)
        {
            Console.WriteLine($"Received: VacancyId={application.VacancyId}, Resume={application.Resume}");

            ModelState.Remove("CandidateId");
            ModelState.Remove("Vacancy");
            ModelState.Remove("AppliedDate");

            if (ModelState.IsValid)
            {
                application.CandidateId = "candidate-id";
                application.AppliedDate = DateTime.Now;
                Console.WriteLine($"Before saving: CandidateId={application.CandidateId}, VacancyId={application.VacancyId}, Resume={application.Resume}");
                repository.AddApplication(application);
                Console.WriteLine("Application saved successfully");
                TempData["Message"] = "Application submitted successfully!";
                return RedirectToAction("MyApplications");
            }
            Console.WriteLine("ModelState is invalid");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation error: {error.ErrorMessage}");
            }
            return View(application);
        }

        [HttpGet]
        public IActionResult MyApplications()
        {
            var applications = repository.Applications
                .Where(a => a.CandidateId == "candidate-id")
                .Include(a => a.Vacancy)
                .Select(a => new ApplicationViewModel
                {
                    ApplicationId = a.ApplicationId,
                    VacancyTitle = a.Vacancy != null ? a.Vacancy.Title : "Vacancy Removed",
                    Resume = a.Resume,
                    AppliedDate = a.AppliedDate
                });
            Console.WriteLine($"Found {applications.Count()} applications for candidate-id");
            if (!applications.Any())
            {
                ViewBag.Message = "You haven't applied to any vacancies yet.";
            }
            return View(applications);
        }

        [HttpGet]
        public IActionResult EditApplication(long id)
        {
            var application = repository.Applications.FirstOrDefault(a => a.ApplicationId == id && a.CandidateId == "candidate-id");
            if (application == null) return NotFound();
            return View(application);
        }

        [HttpPost]
        public IActionResult EditApplication(Application application)
        {
            ModelState.Remove("CandidateId");
            ModelState.Remove("Vacancy");
            ModelState.Remove("AppliedDate");

            if (ModelState.IsValid)
            {
                var existingApplication = repository.Applications.FirstOrDefault(a => a.ApplicationId == application.ApplicationId && a.CandidateId == "candidate-id");
                if (existingApplication == null) return NotFound();

                existingApplication.Resume = application.Resume; 
                repository.UpdateApplication(existingApplication);
                TempData["Message"] = "Application updated successfully!";
                return RedirectToAction("MyApplications");
            }
            return View(application);
        }

        [HttpPost]
        public IActionResult DeleteApplication(long id)
        {
            var application = repository.Applications.FirstOrDefault(a => a.ApplicationId == id && a.CandidateId == "candidate-id");
            if (application == null) return NotFound();

            repository.DeleteApplication(application); 
            TempData["Message"] = "Application deleted successfully!";
            return RedirectToAction("MyApplications");
        }

    }
}