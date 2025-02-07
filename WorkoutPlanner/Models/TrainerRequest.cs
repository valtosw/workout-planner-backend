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

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

    }
}
