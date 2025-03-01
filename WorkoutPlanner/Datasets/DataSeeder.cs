using WorkoutPlanner.Data;

namespace WorkoutPlanner.Datasets
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
                var muscleGroups = DatasetHandler.GetMuscleGroupAndExerciseData(Path.Combine(Directory.GetCurrentDirectory(), "Datasets", "DatasetFiles", "exercises_dataset.txt")).Item1;

                context.MuscleGroups.AddRange(muscleGroups);
                context.SaveChanges();
            }

            if (!context.Exercises.Any())
            {
                var exercises = DatasetHandler.GetMuscleGroupAndExerciseData(Path.Combine(Directory.GetCurrentDirectory(), "Datasets", "DatasetFiles", "exercises_dataset.txt")).Item2;

                context.Exercises.AddRange(exercises);
                context.SaveChanges();
            }

            if (!context.Countries.Any())
            {
                var countries = DatasetHandler.GetCountryData(Path.Combine(Directory.GetCurrentDirectory(), "Datasets", "DatasetFiles", "countries_dataset.txt"));

                context.Countries.AddRange(countries);
                context.SaveChanges();
            }
        }
    }
}
