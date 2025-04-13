namespace WorkoutPlanner.Models.DTOs
{
    public class CreateWorkoutPlanDto
    {
        public string Name { get; set; }
        public string CreatedById { get; set; }
        public string? AssignedToId { get; set; }
        public IEnumerable<EntryToAddDto> WorkoutPlanEntries { get; set; }
    }
}
