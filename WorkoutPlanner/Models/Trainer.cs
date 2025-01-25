namespace WorkoutPlanner.Models
{
    public class Trainer : User
    {
        public string Experience { get; set; }
        public string PlaceOfWork { get; set; }
        public string PhoneNumber { get; set; }
        public float WorkoutPrice { get; set; }
        public string? InstagramLink { get; set; }
        public string? TelegramLink { get; set; }
        public string? FacebookLink { get; set; }
    }
}
