using WorkoutPlanner.Data;

namespace WorkoutPlanner.Helpers
{
    public static class DataSeeder
    {
        public static void PopulateDb(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            AddInitialData(serviceScope.ServiceProvider.GetService<AppDbContext>()!);
        }

        private static void AddInitialData(AppDbContext context)
        {
            if (!context.MuscleGroups.Any())
            {
                var muscleGroups = DatasetHandler.GetSeedData(Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "exercises_dataset.txt")).Item1;

                context.MuscleGroups.AddRange(muscleGroups);
                context.SaveChanges();
            }

            if (!context.Exercises.Any())
            {
                var exercises = DatasetHandler.GetSeedData(Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "exercises_dataset.txt")).Item2;

                context.Exercises.AddRange(exercises);
                context.SaveChanges();
            }
        }
    }
}
