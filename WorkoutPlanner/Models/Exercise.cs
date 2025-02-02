namespace WorkoutPlanner.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string MuscleGroup { get; set; }

        public ICollection<ProgressLog> ProgressLogs { get; set; } = [];
        public ICollection<WorkoutPlanEntry> WorkoutPlanEntries { get; set; } = [];
    }
}
