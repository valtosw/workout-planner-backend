﻿using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Models.DTOs.AuthDTOs
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
