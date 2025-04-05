namespace WorkoutPlanner.Models.DTOs
{
    public class UpdateCustomerDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public IFormFile? ProfilePicture { get; set; }
    }
}
