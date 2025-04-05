using Humanizer;
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

        [HttpPost("UpdateCustomerProfile")]
        public async Task UpdateCustomerProfile([FromForm] UpdateCustomerDto updatedCustomer)
        {
            var customer = await context.Customers.FindAsync(updatedCustomer.Id);

            if (!string.IsNullOrEmpty(updatedCustomer.FirstName))
            {
                customer!.FirstName = updatedCustomer.FirstName;
            }

            if (!string.IsNullOrEmpty(updatedCustomer.LastName))
            {
                customer!.LastName = updatedCustomer.LastName;
            }

            if (updatedCustomer.ProfilePicture is not null)
            {
                using var ms = new MemoryStream();
                await updatedCustomer.ProfilePicture.CopyToAsync(ms);
                var imageBytes = ms.ToArray();
                customer!.ProfilePicture = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
            }

            await context.SaveChangesAsync();
        }
    }
}
