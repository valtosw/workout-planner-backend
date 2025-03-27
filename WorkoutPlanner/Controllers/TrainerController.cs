using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data;
using WorkoutPlanner.Helpers;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController(AppDbContext context) : ControllerBase
    {
        [HttpGet("Posted")]
        public async Task<IEnumerable<Trainer>> GetAllPostedTrainers()
        {
            var trainers = await context.Trainers
                .Where(t => t.IsPosted == true)
                .ToListAsync();

            return trainers;
        }

        [HttpGet("Filtered")]
        public async Task<IEnumerable<Trainer>> GetFilteredTrainers(
            [FromQuery] int? experience,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isCertified,
            [FromQuery] string? country,
            [FromQuery] string? city)
        {
            var trainersQuery = context.Trainers.Where(t => t.IsPosted == true).AsQueryable();

            if (experience.HasValue)
            {
                trainersQuery = trainersQuery.Where(t => t.Experience >= experience.Value);
            }

            if (minPrice.HasValue)
            {
                trainersQuery = trainersQuery.Where(t => t.TrainingPrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                trainersQuery = trainersQuery.Where(t => t.TrainingPrice <= maxPrice.Value);
            }

            if (isCertified.HasValue)
            {
                trainersQuery = trainersQuery.Where(t => t.IsCertified == isCertified.Value);
            }

            if (country is not null)
            {
                trainersQuery = trainersQuery.Where(t => t.Country!.Name == country);
            }

            if (city is not null)
            {
                trainersQuery = trainersQuery.Where(t => t.City == city.Trim());
            }

            var trainers = await trainersQuery.ToListAsync();

            return trainers;
        }

        [HttpGet("MaxPrice")]
        public async Task<decimal> GetMaxTrainingPrice()
        {
            var maxPrice = await context.Trainers
                .Where(t => t.IsPosted == true)
                .MaxAsync(t => t.TrainingPrice);

            return maxPrice;
        }

        [HttpGet("MaxExperience")]
        public async Task<int> GetMaxExperience()
        {
            var maxExperience = await context.Trainers
                .Where(t => t.IsPosted == true)
                .MaxAsync(t => t.Experience);

            return maxExperience;
        }
    }
}
