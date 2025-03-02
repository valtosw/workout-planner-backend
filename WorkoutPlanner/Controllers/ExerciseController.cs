using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController(AppDbContext context) : ControllerBase
    {
        [HttpGet("ExercisesByMuscleGroup")]
        public async Task<IEnumerable<string>> GetExercisesByMuscleGroup(string muscleGroupName)
        {
            var muscleGroup = await context.MuscleGroups
                .Include(mg => mg.Exercises)
                .Where(mg => mg.Name == muscleGroupName)
                .FirstOrDefaultAsync();

            return muscleGroup is null ? [] : muscleGroup.Exercises.Select(e => e.Name);
        }
    }
}
