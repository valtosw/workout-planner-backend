namespace WorkoutPlanner.Models
{
    public class TrainerRequest
    {
        public int Id { get; set; }

        public enum RequestStatus
        {
            Pending,
            Accepted,
            Rejected
        }

        public string TrainerId { get; set; } = null!;
        public Trainer Trainer { get; set; } = null!;

        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

    }
}
