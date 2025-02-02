using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string Experience { get; set; }
        public string PlaceOfWork { get; set; }
        public decimal TrainingPrice { get; set; }
        public string? InstagramLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? TelegramLink { get; set; }

        public ICollection<User> Users { get; set; } = [];
        public ICollection<TrainerRequest> ReceivedRequests { get; set; } = [];
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
    }
}
