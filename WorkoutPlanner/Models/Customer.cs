using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models
{
    public class Customer : ApplicationUser
    {
        public ICollection<Trainer> Trainers { get; set; } = [];
        public ICollection<TrainerRequest> SentRequests { get; set; } = [];
        public ICollection<ProgressLog> ProgressLogs { get; set; } = [];
    }
}
