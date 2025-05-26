using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Lab5.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lab5.Controllers
{
    public class VacancyController : Controller
    {
        private readonly IJobRepository repository;
        private int pageSize = 5;

        public VacancyController(IJobRepository repo)
        {
            repository = repo;
        }

        [AllowAnonymous]
        public IActionResult Index(string category, int page = 1)
        {
            var vacancies = repository.Vacancies
                .Where(v => category == null || v.Category == category)
                .OrderBy(v => v.VacancyId);

            var totalItems = vacancies.Count();
            var pagedVacancies = vacancies
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

            return View("~/Views/Home/Index.cshtml", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                repository.AddVacancy(vacancy);
                return RedirectToAction("Index");
            }
            return View(vacancy);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(long id)
        {
            var vacancy = repository.Vacancies
                .Include(v => v.Applications)
                .FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();
            return View(vacancy);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();
            return View(vacancy);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateVacancy(vacancy);
                return RedirectToAction("Index");
            }
            return View(vacancy);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(long id)
        {
            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();
            return View(vacancy);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();
            repository.DeleteVacancy(vacancy);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Apply(long id)
        {
            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();

            var draftKey = $"DraftApplication_{id}";
            var draft = HttpContext.Session.GetString(draftKey);
            var application = draft != null
                ? JsonSerializer.Deserialize<Application>(draft)
                : new Application { VacancyId = id };

            ViewBag.VacancyTitle = vacancy.Title;
            return View(application);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Apply(Application application)
        {
            ModelState.Remove("CandidateId");
            ModelState.Remove("Vacancy");
            ModelState.Remove("AppliedDate");

            if (ModelState.IsValid)
            {
                application.CandidateId = User.Identity.Name ?? "candidate-id";
                application.AppliedDate = DateTime.Now;
                repository.AddApplication(application);

                var draftKey = $"DraftApplication_{application.VacancyId}";
                HttpContext.Session.Remove(draftKey);
                TempData["Message"] = "Application submitted successfully!";
                return RedirectToAction("MyApplications");
            }

            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == application.VacancyId);
            ViewBag.VacancyTitle = vacancy?.Title;
            return View(application);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SaveDraft(Application application)
        {
            var draftKey = $"DraftApplication_{application.VacancyId}";
            HttpContext.Session.SetString(draftKey, JsonSerializer.Serialize(application));
            return Json(new { success = true });
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult MyApplications()
        {
            var candidateId = User.Identity?.Name ?? "candidate-id";

            var applications = repository.Applications
                .Where(a => a.CandidateId == candidateId)
                .Include(a => a.Vacancy)
                .Select(a => new ApplicationViewModel
                {
                    ApplicationId = a.ApplicationId,
                    VacancyTitle = a.Vacancy != null ? a.Vacancy.Title : "Vacancy Removed",
                    Resume = a.Resume,
                    AppliedDate = a.AppliedDate
                });

            if (!applications.Any())
                ViewBag.Message = "You haven't applied to any vacancies yet.";

            return View(applications);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult EditApplication(long id)
        {
            var candidateId = User.Identity?.Name ?? "candidate-id";
            var application = repository.Applications.FirstOrDefault(a => a.ApplicationId == id && a.CandidateId == candidateId);
            if (application == null) return NotFound();

            var draftKey = $"DraftEditApplication_{id}";
            var draft = HttpContext.Session.GetString(draftKey);
            if (draft != null)
            {
                var draftApplication = JsonSerializer.Deserialize<Application>(draft);
                application.Resume = draftApplication.Resume;
            }

            return View(application);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult EditApplication(Application application)
        {
            ModelState.Remove("CandidateId");
            ModelState.Remove("Vacancy");
            ModelState.Remove("AppliedDate");

            var candidateId = User.Identity?.Name ?? "candidate-id";
            if (ModelState.IsValid)
            {
                var existingApplication = repository.Applications.FirstOrDefault(a => a.ApplicationId == application.ApplicationId && a.CandidateId == candidateId);
                if (existingApplication == null) return NotFound();

                existingApplication.Resume = application.Resume;
                repository.UpdateApplication(existingApplication);

                var draftKey = $"DraftEditApplication_{application.ApplicationId}";
                HttpContext.Session.Remove(draftKey);

                TempData["Message"] = "Application updated successfully!";
                return RedirectToAction("MyApplications");
            }

            return View(application);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SaveEditDraft(Application application)
        {
            var draftKey = $"DraftEditApplication_{application.ApplicationId}";
            HttpContext.Session.SetString(draftKey, JsonSerializer.Serialize(application));
            return Json(new { success = true });
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult DeleteApplication(long id)
        {
            var candidateId = User.Identity?.Name ?? "candidate-id";
            var application = repository.Applications.FirstOrDefault(a => a.ApplicationId == id && a.CandidateId == candidateId);
            if (application == null) return NotFound();

            repository.DeleteApplication(application);
            TempData["Message"] = "Application deleted successfully!";
            return RedirectToAction("MyApplications");
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult ApplicationDetails(long id)
        {
            var candidateId = User.Identity?.Name ?? "candidate-id";
            var application = repository.Applications
                .Include(a => a.Vacancy)
                .FirstOrDefault(a => a.ApplicationId == id && a.CandidateId == candidateId);
            if (application == null) return NotFound();
            return View(application);
        }
    }
}