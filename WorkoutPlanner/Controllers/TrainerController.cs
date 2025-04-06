using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data;
using WorkoutPlanner.Helpers;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.DTOs;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController(AppDbContext context) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<TrainerInfoDto> GetTrainerInfoById(string id)
        {
            var trainer = await context.Trainers
                .Where(t => t.Id == id)
                .Include(t => t.Country)
                .Select(t => new TrainerInfoDto
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    ProfilePicture = t.ProfilePicture,
                    Experience = t.Experience,
                    Country = t.Country!.Name,
                    City = t.City,
                    PlaceOfWork = t.PlaceOfWork,
                    TrainingPrice = t.TrainingPrice,
                    Bio = t.Bio,
                    InstagramLink = t.InstagramLink,
                    FacebookLink = t.FacebookLink,
                    TelegramLink = t.TelegramLink
                }).FirstOrDefaultAsync();
            return trainer ?? new TrainerInfoDto();
        }

        [HttpPost("UpdateTrainerProfile")]
        public async Task UpdateTrainerProfile([FromForm] UpdateTrainerDto updatedTrainer)
        {
            var trainer = await context.Trainers.FindAsync(updatedTrainer.Id);

            if (!string.IsNullOrEmpty(updatedTrainer.FirstName))
            {
                trainer!.FirstName = updatedTrainer.FirstName;
            }
            if (!string.IsNullOrEmpty(updatedTrainer.LastName))
            {
                trainer!.LastName = updatedTrainer.LastName;
            }
            if (updatedTrainer.ProfilePicture is not null)
            {
                using var ms = new MemoryStream();
                await updatedTrainer.ProfilePicture.CopyToAsync(ms);
                var imageBytes = ms.ToArray();
                trainer!.ProfilePicture = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
            }

            trainer!.Experience = updatedTrainer.Experience;

            if (!string.IsNullOrEmpty(updatedTrainer.Country))
            {
                var country = await context.Countries
                    .FirstOrDefaultAsync(c => c.Name == updatedTrainer.Country);

                if (country is not null) { trainer!.CountryId = country.Id; }
            }

            if (!string.IsNullOrEmpty(updatedTrainer.City))
            {
                trainer!.City = updatedTrainer.City;
            }

            if (!string.IsNullOrEmpty(updatedTrainer.PlaceOfWork))
            {
                trainer!.PlaceOfWork = updatedTrainer.PlaceOfWork;
            }

            trainer!.TrainingPrice = updatedTrainer.TrainingPrice;

            if (!string.IsNullOrEmpty(updatedTrainer.Bio))
            {
                trainer!.Bio = updatedTrainer.Bio;
            }

            if (!string.IsNullOrEmpty(updatedTrainer.InstagramLink))
            {
                trainer!.InstagramLink = updatedTrainer.InstagramLink;
            }

            if (!string.IsNullOrEmpty(updatedTrainer.FacebookLink))
            {
                trainer!.FacebookLink = updatedTrainer.FacebookLink;
            }

            if (!string.IsNullOrEmpty(updatedTrainer.TelegramLink))
            {
                trainer!.TelegramLink = updatedTrainer.TelegramLink;
            }

            await context.SaveChangesAsync();
        }

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

        [HttpGet("PersonalCustomers/{id}")]
        public async Task<IEnumerable<CustomerDto>> GetPersonalCustomersById(string id)
        {
            var customers = await context.Trainers
                .Where(t => t.Id == id)
                .SelectMany(t => t.Customers)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ProfilePicture = c.ProfilePicture
                }).ToListAsync();

            return customers ?? [];
        }
    }
}
