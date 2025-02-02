namespace WorkoutPlanner.Models
{
    public class TrainerRequest
    {
        public int Id { get; set; }

        public string RequestStatus { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
