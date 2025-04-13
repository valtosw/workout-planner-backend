namespace WorkoutPlanner.Models.DTOs
{
    public class EntryToAddDto
    {
        public int Reps { get; set; }
        public int Sets { get; set; }
        public float Weight { get; set; }
        public string Exercise { get; set; }
    }
}
