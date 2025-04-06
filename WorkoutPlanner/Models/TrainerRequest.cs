namespace WorkoutPlanner.Models
{
    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class TrainerRequest
    {
        public int Id { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public string TrainerId { get; set; } = null!;
        public Trainer Trainer { get; set; } = null!;

        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

    }
}
