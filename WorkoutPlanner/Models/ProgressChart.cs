namespace WorkoutPlanner.Models
{
    public class ProgressChart
    {
        public int Id { get; set; }
        public List<float> BenchPressData;
        public List<float> DeadliftData;
        public List<float> BarbellSquatData;

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
