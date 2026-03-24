using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VacancyApiController : ControllerBase
    {
        private readonly IJobRepository repository;

        public VacancyApiController(IJobRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetVacancies()
        {
            var vacancies = repository.Vacancies.Select(v => new
            {
                v.VacancyId,
                v.Title,
                v.Description,
                v.Company,
                v.Salary,
                v.Category
            });
            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetVacancy(long id)
        {
            var vacancy = repository.Vacancies.FirstOrDefault(v => v.VacancyId == id);
            if (vacancy == null) return NotFound();
            return Ok(new
            {
                vacancy.VacancyId,
                vacancy.Title,
                vacancy.Description,
                vacancy.Company,
                vacancy.Salary,
                vacancy.Category
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateVacancy([FromBody] Vacancy vacancy)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.AddVacancy(vacancy);
            return CreatedAtAction(nameof(GetVacancy), new { id = vacancy.VacancyId }, vacancy);
        }
    }
}