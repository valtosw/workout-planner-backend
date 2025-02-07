namespace WorkoutPlanner.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; } = null!;

        public ICollection<ProgressLog> ProgressLogs { get; set; } = [];
        public ICollection<WorkoutPlanEntry> WorkoutPlanEntries { get; set; } = [];
    }
}
