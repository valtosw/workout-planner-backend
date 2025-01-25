namespace WorkoutPlanner.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Exercise> Exercises { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
