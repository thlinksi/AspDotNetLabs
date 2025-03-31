using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

public class CandidateController : Controller
{
    private readonly IJobRepository repository;

    public CandidateController(IJobRepository repo)
    {
        repository = repo;
    }

    public IActionResult MyApplications()
    {
        var applications = repository.Applications
            .Where(a => a.CandidateId == "candidate-id"); 
        return View(applications);
    }
}