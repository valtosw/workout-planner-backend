using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
