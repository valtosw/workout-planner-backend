using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController(AppDbContext context) : ControllerBase
    {

        // GET: api/<TrainerController>
        [HttpGet("posted")]
        public async Task<IEnumerable<Trainer>> GetAllPostedTrainers()
        {
            var trainers = await context.Trainers
                .Where(t => t.IsPosted == true)
                .ToListAsync();

            return trainers;
        }

        [HttpGet("filter")]
        public async Task<IEnumerable<Trainer>> GetFilteredTrainers(
            [FromQuery] string? experience,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isCertified,
            [FromQuery] string? location)
        {
            var trainersQuery = context.Trainers.Where(t => t.IsPosted == true).AsQueryable();

            if (!string.IsNullOrEmpty(experience))
            {
                if (experience.EndsWith("+") && int.TryParse(experience.TrimEnd('+'), out var years))
                {
                    trainersQuery = trainersQuery.Where(t =>
                        t.Experience != null &&
                        t.Experience >= years
                    );
                }
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
