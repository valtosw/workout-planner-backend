namespace WorkoutPlanner.Models.AuthModels
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
