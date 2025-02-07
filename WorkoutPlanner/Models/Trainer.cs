using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models
{
    public class Trainer : ApplicationUser
    {
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public string? PlaceOfWork { get; set; }
        public decimal TrainingPrice { get; set; }
        public string? InstagramLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? TelegramLink { get; set; }

        public ICollection<Customer> Customers { get; set; } = [];
        public ICollection<TrainerRequest> ReceivedRequests { get; set; } = [];
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
    }
}
