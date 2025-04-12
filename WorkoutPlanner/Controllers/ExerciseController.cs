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

        [HttpGet("MostLoggedExercises/{id}")]
        public async Task<IEnumerable<string>> GetMostLoggedExercises(string id, int limit = 20)
        {
            var topExercises = await context.ProgressLogs
                .Where(pl => pl.CustomerId == id)
                .GroupBy(pl => pl.Exercise.Name)
                .OrderByDescending(g => g.Count())
                .Take(limit)
                .Select(g => g.Key)
                .ToListAsync();

            return topExercises;
        }

        [HttpGet("AllExercises")]
        public async Task<IEnumerable<string>> GetAllExercises()
        {
            var exercises = await context.Exercises
                .Select(e => e.Name)
                .ToListAsync();

            return exercises;
        }
    }
}
