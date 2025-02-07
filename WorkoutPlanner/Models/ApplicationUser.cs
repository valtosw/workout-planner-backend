using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
