namespace WorkoutPlanner.Models.DTOs
{
    public class WorkoutPlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CreatedByDto CreatedBy { get; set; }
        public AssignedToDto? AssignedTo { get; set; }
        public IEnumerable<WorkoutPlanEntryDto> WorkoutPlanEntries { get; set; }
    }
}
