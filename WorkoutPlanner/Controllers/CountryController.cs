using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController(AppDbContext context) : ControllerBase
    {
        [HttpGet("AllCountries")]
        public async Task<IEnumerable<string>> GetAllCountryNames()
        {
            var countries = await context.Countries.Select(c => c.Name).ToListAsync();
            return countries;
        }

        [HttpGet("CountryName/{id}")]
        public async Task<string?> GetCountryNameById(int id)
        {
            return await context.Countries.Where(c => c.Id == id).Select(c => c.Name).FirstOrDefaultAsync();
        }
    }
}
