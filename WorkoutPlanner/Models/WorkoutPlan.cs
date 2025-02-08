using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public string CreatedById { get; set; } = null!;
        public ApplicationUser CreatedBy { get; set; } = null!;

        public string? AssignedToId { get; set; }
        public Customer? AssignedTo { get; set; }

        public ICollection<WorkoutPlanEntry> WorkoutPlanEntries { get; set; } = [];
    }
}
