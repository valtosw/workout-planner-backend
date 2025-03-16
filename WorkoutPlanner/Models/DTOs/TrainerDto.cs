namespace WorkoutPlanner.Models.DTOs
{
    public class TrainerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public int Experience { get; set; }
        public int? CountryId { get; set; }
        public string City { get; set; }
        public string PlaceOfWork { get; set; }
        public decimal TrainingPrice { get; set; }
        public string Bio { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public string TelegramLink { get; set; }
    }
}
