using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models.DTOs;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(AppDbContext context) : ControllerBase
    {
        [HttpGet("PersonalTrainers/{id}")]
        public async Task<IEnumerable<TrainerDto>> GetPersonalTrainersById(string id)
        {
            var trainers = await context.Customers
                .Where(c => c.Id == id)
                .SelectMany(c => c.Trainers)
                .Select(t => new TrainerDto
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    ProfilePicture = t.ProfilePicture,
                    Experience = t.Experience,
                    CountryId = t.CountryId,
                    City = t.City,
                    PlaceOfWork = t.PlaceOfWork,
                    TrainingPrice = t.TrainingPrice,
                    Bio = t.Bio,
                    InstagramLink = t.InstagramLink,
                    FacebookLink = t.FacebookLink,
                    TelegramLink = t.TelegramLink
                }).ToListAsync();

            return trainers ?? [];
        }
    }
}
