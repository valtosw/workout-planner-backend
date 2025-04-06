namespace WorkoutPlanner.Models.DTOs
{
    public class TrainerRequestDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string? UserProfilePicture { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
