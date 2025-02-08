namespace WorkoutPlanner.Models
{
    public class ProgressLog
    {
        public int Id { get; set; }

        public float Weight { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;

        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;
    }
}
