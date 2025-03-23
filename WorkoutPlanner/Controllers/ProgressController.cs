using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SkiaSharp;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models.DTOs;

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

        [HttpGet("WeightLifted/{id}/{exerciseName}/{period}")]
        public async Task<IEnumerable<LogDto>> GetWeightLiftedByExercise(string id, string exerciseName, string period = "Month")
        {
            var logs = await context.ProgressLogs
                .Where(log => log.CustomerId == id && log.Exercise.Name == exerciseName)
                .OrderBy(log => log.LogDate)
                .ToListAsync();

            if (logs is null || logs.Count == 0)
            {
                return [];
            }

            if (period.Equals("Month", StringComparison.OrdinalIgnoreCase))
            {
                var currentDate = DateTime.UtcNow;
                int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                var logsByDay = logs
                    .Where(log => log.LogDate.Year == currentDate.Year && log.LogDate.Month == currentDate.Month)
                    .ToDictionary(log => log.LogDate.Day, log => log);

                var result = new List<LogDto>();
                for (int day = 1; day <= daysInMonth; day++)
                {
                    if (logsByDay.TryGetValue(day, out var log))
                    {
                        result.Add(new LogDto { LogDate = log.LogDate, Weight = log.Weight });
                    }
                    else
                    {
                        result.Add(new LogDto { LogDate = new DateTime(currentDate.Year, currentDate.Month, day), Weight = 0 });
                    }
                }
                return result;
            }
            else if (period.Equals("Year", StringComparison.OrdinalIgnoreCase))
            {
                var currentYear = DateTime.UtcNow.Year;
                var groupedLogs = logs
                    .Where(log => log.LogDate.Year == currentYear)
                    .GroupBy(log => log.LogDate.Month)
                    .ToDictionary(g => g.Key, g => g.Max(log => log.Weight));

                var result = new List<LogDto>();
                for (int month = 1; month <= 12; month++)
                {
                    result.Add(new LogDto
                    {
                        LogDate = new DateTime(currentYear, month, 1),
                        Weight = groupedLogs.ContainsKey(month) ? groupedLogs[month] : 0
                    });
                }

                return result;
            }

            return [];
        }
    }
}
