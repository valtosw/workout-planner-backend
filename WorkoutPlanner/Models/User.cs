using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }

        public ICollection<Trainer> Trainers { get; set; } = [];
        public ICollection<TrainerRequest> SentRequests { get; set; } = [];
        public ICollection<ProgressLog> ProgressLogs { get; set; } = [];
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
    }
}
