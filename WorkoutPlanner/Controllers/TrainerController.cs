using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController(AppDbContext context) : ControllerBase
    {

        [HttpGet("posted")]
        public async Task<IEnumerable<Trainer>> GetAllPostedTrainers()
        {
            var trainers = await context.Trainers
                .Where(t => t.IsPosted == true)
                .ToListAsync();

            return trainers;
        }

        [HttpGet("filtered")]
        public async Task<IEnumerable<Trainer>> GetFilteredTrainers(
            [FromQuery] int? experience,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isCertified,
            [FromQuery] string? location)
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

            if (!string.IsNullOrEmpty(location))
            {
                trainersQuery = trainersQuery.Where(t => t.Location!.Contains(location));
            }

            var trainers = await trainersQuery.ToListAsync();

            return trainers;
        }

        [HttpGet("max-price")]
        public async Task<decimal> GetMaxTrainingPrice()
        {
            var maxPrice = await context.Trainers
                .Where(t => t.IsPosted == true)
                .MaxAsync(t => t.TrainingPrice);

            return maxPrice;
        }

        //// GET api/<TrainerController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<TrainerController>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<TrainerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<TrainerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
