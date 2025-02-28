﻿namespace WorkoutPlanner.Models
{
    public class Country
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Trainer> Trainers { get; set; } = [];
    }
}
