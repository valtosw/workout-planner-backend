using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuscleGroupController(AppDbContext context) : ControllerBase
    {
        [HttpGet("AllMuscleGroups")]
        public async Task<IEnumerable<string>> GetAllMuscleGroups()
        {
            return await context.MuscleGroups.Select(mg => mg.Name).ToListAsync();
        }
    }
}
