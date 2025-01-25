namespace WorkoutPlanner.Models
{
    public class Client : User
    {
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }

        public int ProgressChartId { get; set; }
        public ProgressChart ProgressChart { get; set; }
    }
}
