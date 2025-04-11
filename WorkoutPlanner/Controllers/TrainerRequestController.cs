using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.DTOs;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerRequestController(AppDbContext context) : ControllerBase
    {
        [HttpGet("RequestStatus/{customerId}/{trainerId}")]
        public async Task<string> GetTrainerRequestStatusByIds(string customerId, string trainerId)
        {
            var request = await context.TrainerRequests
                .FirstOrDefaultAsync(r => r.CustomerId == customerId && r.TrainerId == trainerId);
            
            return request is null ? "No Request Found" : request.RequestStatus.ToString();
        }

        [HttpPost("SendRequest/{customerId}/{trainerId}")]
        public async Task SendTrainerRequest(string customerId, string trainerId)
        {
            var request = new TrainerRequest
            {
                CustomerId = customerId,
                TrainerId = trainerId,
                RequestStatus = RequestStatus.Pending
            };

            await context.TrainerRequests.AddAsync(request);
            await context.SaveChangesAsync();
        }

        [HttpPut("AcceptRequest/{id}")]
        public async Task AcceptTrainerRequest(int id)
        {
            var request = await context.TrainerRequests
                .FirstOrDefaultAsync(r => r.Id == id);
            
            if (request is not null)
            {
                request.RequestStatus = RequestStatus.Accepted;

                var customer = await context.Customers
                   .Include(c => c.Trainers)
                   .FirstOrDefaultAsync(c => c.Id == request.CustomerId);

                var trainer = await context.Trainers
                    .Include(t => t.Customers)
                    .FirstOrDefaultAsync(t => t.Id == request.TrainerId);

                if (customer is not null && trainer is not null)
                {
                    if (!customer.Trainers.Contains(trainer))
                        customer.Trainers.Add(trainer);

                    if (!trainer.Customers.Contains(customer))
                        trainer.Customers.Add(customer);
                }

                await context.SaveChangesAsync();
            }
        }

        [HttpPut("RejectRequest/{id}")]
        public async Task RejectTrainerRequest(int id)
        {
            var request = await context.TrainerRequests
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request is not null)
            {
                request.RequestStatus = RequestStatus.Rejected;
                await context.SaveChangesAsync();
            }
        }

        [HttpGet("AllTrainerRequests/{userId}")]
        public async Task<IEnumerable<TrainerRequestDto>> GetAllTrainerRequests(string userId)
        {
            var userRoleRow = await context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId);

            var userRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Id == userRoleRow!.RoleId);

            var role = userRole!.Name!;

            if (role == "Customer")
            {
                var requests = await context.TrainerRequests
                    .Where(r => r.CustomerId == userId)
                    .Select(r => new TrainerRequestDto
                    {
                        Id = r.Id,
                        Status = r.RequestStatus.ToString(),
                        UserProfilePicture = context.Trainers
                            .Where(t => t.Id == r.TrainerId)
                            .Select(t => t.ProfilePicture)
                            .FirstOrDefault(),
                        UserFirstName = context.Trainers
                            .Where(t => t.Id == r.TrainerId)
                            .Select(t => t.FirstName)
                            .FirstOrDefault()!,
                        UserLastName = context.Trainers
                            .Where(t => t.Id == r.TrainerId)
                            .Select(t => t.LastName)
                            .FirstOrDefault()!,
                    }).ToListAsync();
                return requests;
            }
            else if (role == "Trainer")
            {
                var requests = await context.TrainerRequests
                    .Where(r => r.TrainerId == userId)
                    .Select(r => new TrainerRequestDto
                    {
                        Id = r.Id,
                        Status = r.RequestStatus.ToString(),
                        UserProfilePicture = context.Customers
                            .Where(t => t.Id == r.CustomerId)
                            .Select(t => t.ProfilePicture)
                            .FirstOrDefault(),
                        UserFirstName = context.Customers
                            .Where(t => t.Id == r.CustomerId)
                            .Select(t => t.FirstName)
                            .FirstOrDefault()!,
                        UserLastName = context.Customers
                            .Where(t => t.Id == r.CustomerId)
                            .Select(t => t.LastName)
                            .FirstOrDefault()!,
                    }).ToListAsync();
                return requests;
            }
            else
            {
                return [];
            }
        }
    }
}
