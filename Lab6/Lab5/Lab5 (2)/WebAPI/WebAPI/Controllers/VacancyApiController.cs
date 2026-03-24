using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Data;
using SharedLibrary.Models;

namespace WebAPI.Controllers
{
    [Route("api/vacancyapi")]
    [ApiController]
    [Authorize] 
    public class VacancyApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VacancyApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetVacancies()
        {
            return await _context.Vacancies.ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<Vacancy>> PostVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Add(vacancy);
            await _context.SaveChangesAsync();
            return Ok(vacancy);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null) return NotFound();

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}