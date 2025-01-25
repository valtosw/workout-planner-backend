namespace WorkoutPlanner.Models
{
    public class Exercise
    {
        public enum TargetMuscles
        {
            Chest,
            Back,
            Legs,
            Shoulders,
            Biceps,
            Triceps,
            Forearms,
            Abs
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSets { get; set; }
        public int NumberOfReps { get; set; }
        public TargetMuscles TargetMuscle { get; set; }
        public float Weight { get; set; }

        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
    }
}
