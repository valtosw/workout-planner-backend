namespace WorkoutPlanner.Models.AuthModels
{
    public class EmailSettings
    {
        public string SendGridApiKey { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
    }
}
