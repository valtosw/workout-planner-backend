﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Models.AuthModels;

namespace WorkoutPlanner.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string? ProfilePicture { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
    }
}
