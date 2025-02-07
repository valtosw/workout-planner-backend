﻿namespace WorkoutPlanner.Models
{
    public class WorkoutPlanEntry
    {
        public int Id { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } = null!;

    }
}
