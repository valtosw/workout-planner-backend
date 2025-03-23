using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using WorkoutPlanner.Data;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController(AppDbContext context) : ControllerBase
    {
        [HttpGet("FrequentlyTrainedMuscleGroups/{id}/{period}")]
        public async Task<Dictionary<string, double>> GetFrequentlyTrainedMuscleGroupsById(string id, string period)
        {
            var muscleGroupMapping = new Dictionary<string, int[]>
            {
                { "Chest", new int[] { 6 } },
                { "Back", new int[] { 8, 11, 12, 14 } },
                { "Legs", new int[] { 2, 3, 7, 9, 10, 17 } },
                { "Shoulders", new int[] { 5, 11 } },
                { "Arms", new int[] { 4, 13, 15 } },
                { "Core", new int[] { 1, 11 } },
                { "Neck", new int[] { 16 } }
            };

            DateTime startDate = period switch
            {
                "Week" => DateTime.Now.AddDays(-7),
                "Month" => DateTime.Now.AddMonths(-1),
                _ => DateTime.MinValue
            };

            var query = context.ProgressLogs
                .Where(log => log.CustomerId == id && log.LogDate >= startDate)
                .Include(log => log.Exercise);

            var progressLogs = await query.ToListAsync();

            var muscleGroupCounts = muscleGroupMapping.Keys.ToDictionary(key => key, key => 0);
            int totalExercises = 0;

            foreach (var log in progressLogs)
            {
                foreach (var (groupName, muscleIds) in muscleGroupMapping)
                {
                    if (muscleIds.Contains(log.Exercise.MuscleGroupId))
                    {
                        muscleGroupCounts[groupName]++;
                        totalExercises++;
                        break;
                    }
                }
            }

            double minCount = muscleGroupCounts.Values.Min();
            double maxCount = muscleGroupCounts.Values.Max();

            var normalizedValues = muscleGroupCounts.ToDictionary(
                kvp => kvp.Key,
                kvp => maxCount > minCount ? (kvp.Value - minCount) / (maxCount - minCount) : 0
            );

            return normalizedValues;
        }
    }
}
