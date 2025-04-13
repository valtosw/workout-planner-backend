using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.DTOs;
using WorkoutPlanner.Services;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController(AppDbContext context, PdfService pdfService) : ControllerBase
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
                    Id = wp.Id,
                    Name = wp.Name,
                    CreatedBy = new CreatedByDto
                    {
                        //Id = wp.CreatedBy.Id,
                        FirstName = wp.CreatedBy.FirstName,
                        LastName = wp.CreatedBy.LastName,
                        Email = wp.CreatedBy.Email,
                        ProfilePicture = wp.CreatedBy.ProfilePicture
                    },
                    AssignedTo = wp.AssignedTo != null ? new AssignedToDto
                    {
                        //Id = wp.AssignedTo.Id,
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

        [HttpGet("{id}/DownloadPdf")]
        public async Task<FileContentResult> DownloadWorkoutPlanPdf(int id)
        {
            var workoutPlan = await context.WorkoutPlans
                .Include(wp => wp.CreatedBy)
                .Include(wp => wp.AssignedTo)
                .Include(wp => wp.WorkoutPlanEntries)
                    .ThenInclude(wpe => wpe.Exercise)
                        .ThenInclude(e => e.MuscleGroup)
                .FirstOrDefaultAsync(w => w.Id == id);

            var pdfBytes = pdfService.GenerateWorkoutPlanPdf(workoutPlan!);

            return File(pdfBytes, "application/pdf", $"{workoutPlan!.Name}.pdf");
        }

        [HttpPost("CreateNewWorkoutPlan")]
        public async Task CreateWorkoutPlan([FromBody] CreateWorkoutPlanDto createWorkoutPlanDto)
        {
            var createdBy = await context.Users.FirstOrDefaultAsync(u => u.Id == createWorkoutPlanDto.CreatedById);

            Customer? assignedTo = null;

            if (createWorkoutPlanDto.AssignedToId is not null)
            {
                assignedTo = await context.Customers.FirstOrDefaultAsync(c => c.Id == createWorkoutPlanDto.AssignedToId);
            }

            var workoutPlan = new WorkoutPlan
            {
                Name = createWorkoutPlanDto.Name,
                CreatedById = createdBy!.Id,
                CreatedBy = createdBy!,
                AssignedToId = assignedTo?.Id,
                AssignedTo = assignedTo,
                WorkoutPlanEntries = []
            };

            foreach (var entryDto in createWorkoutPlanDto.WorkoutPlanEntries)
            {
                var exercise = await context.Exercises
                    .FirstOrDefaultAsync(e =>
                        e.Name == entryDto.Exercise);

                var workoutEntry = new WorkoutPlanEntry
                {
                    Sets = entryDto.Sets,
                    Reps = entryDto.Reps,
                    Weight = entryDto.Weight,
                    ExerciseId = exercise!.Id,
                    Exercise = exercise
                };

                workoutPlan.WorkoutPlanEntries.Add(workoutEntry);
            }

            await context.WorkoutPlans.AddAsync(workoutPlan);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteWorkoutPlan(int id)
        {
            var workoutPlan = await context.WorkoutPlans
                .Include(wp => wp.WorkoutPlanEntries)
                .FirstOrDefaultAsync(wp => wp.Id == id);
            
            if (workoutPlan is not null)
            {
                context.WorkoutPlans.Remove(workoutPlan);
                await context.SaveChangesAsync();
            }
        }
    }
}
