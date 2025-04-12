namespace WorkoutPlanner.Models.DTOs
{
    public class ProgressLogDto
    {
        public required string ExerciseName { get; set; }
        public required DateTime LogDate { get; set; }
        public required float Weight { get; set; }
    }
}
