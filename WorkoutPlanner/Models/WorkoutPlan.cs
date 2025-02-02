namespace WorkoutPlanner.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public int? CreatedByTrainerId { get; set; }
        public Trainer? CreatedByTrainer { get; set; }

        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }

        public ICollection<WorkoutPlanEntry> WorkoutPlanEntries { get; set; } = [];
    }
}
