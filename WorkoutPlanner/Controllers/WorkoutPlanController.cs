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
    public class WorkoutPlanController(AppDbContext context) : ControllerBase
    {
        [HttpGet("UserWorkoutPlans/{id}")]
        public async Task<IEnumerable<WorkoutPlanDto>> GetUserWorkoutPlans(string id)
        {
            var workoutPlans = await context.WorkoutPlans
                .Where(wp => wp.CreatedById == id || wp.AssignedToId == id)
                .Include(wp => wp.WorkoutPlanEntries)
                    .ThenInclude(wpe => wpe.Exercise)
                        .ThenInclude(e => e.MuscleGroup)
                .Include(wp => wp.CreatedBy)
                .Include(wp => wp.AssignedTo) 
                .Select(wp => new WorkoutPlanDto
                {
                    Name = wp.Name,
                    CreatedBy = new CreatedByDto
                    {
                        FirstName = wp.CreatedBy.FirstName,
                        LastName = wp.CreatedBy.LastName,
                        Email = wp.CreatedBy.Email,
                        ProfilePicture = wp.CreatedBy.ProfilePicture
                    },
                    AssignedTo = wp.AssignedTo != null ? new AssignedToDto
                    {
                        FirstName = wp.AssignedTo.FirstName,
                        LastName = wp.AssignedTo.LastName,
                        Email = wp.AssignedTo.Email,
                        ProfilePicture = wp.AssignedTo.ProfilePicture
                    } : null,
                    WorkoutPlanEntries = wp.WorkoutPlanEntries.Select(wpe => new WorkoutPlanEntryDto
                    {
                        Exercise = wpe.Exercise.Name,
                        MuscleGroup = wpe.Exercise.MuscleGroup.Name,
                        Reps = wpe.Reps,
                        Sets = wpe.Sets,
                        Weight = wpe.Weight
                    }).ToList()
                })
                .ToListAsync();

            return workoutPlans.Count == 0 ? [] : workoutPlans;
        }
    }
}
