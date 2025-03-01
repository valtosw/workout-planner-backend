using System.Text.RegularExpressions;
using WorkoutPlanner.Helpers;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Datasets
{
    public class DatasetHandler
    {
        public static (List<MuscleGroup>, List<Exercise>) GetMuscleGroupAndExerciseData(string filePath)
        {


            var muscleGroupsDict = new Dictionary<string, MuscleGroup>();
            var exercises = new List<Exercise>();

            var lines = File.ReadAllLines(filePath);

            int muscleGroupIdCounter = 1;
            int exerciseIdCounter = 1;

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length != 2)
                    continue;

                string exerciseName = parts[0].Trim();
                string muscleGroupName = parts[1].Trim();

                if (!muscleGroupsDict.TryGetValue(muscleGroupName, out var muscleGroup))
                {
                    muscleGroup = new MuscleGroup
                    {
                        Id = muscleGroupIdCounter++,
                        Name = HelperMethods.CapitalizeEachWord(muscleGroupName),
                        Exercises = []
                    };
                    muscleGroupsDict[muscleGroupName] = muscleGroup;
                }

                var exercise = new Exercise
                {
                    Id = exerciseIdCounter++,
                    Name = HelperMethods.CapitalizeEachWord(exerciseName),
                    MuscleGroupId = muscleGroup.Id,
                    MuscleGroup = muscleGroup
                };

                muscleGroup.Exercises.Add(exercise);
                exercises.Add(exercise);
            }

            return (muscleGroupsDict.Values.ToList(), exercises);
        }

        public static List<Country> GetCountryData(string filePath)
        {
            var countries = new List<Country>();
            var lines = File.ReadAllLines(filePath);
            var regex = new Regex(@"^(.*?)\s\([A-Z]{2}\)$"); 

            int id = 1;
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    countries.Add(new Country { Id = id++, Name = match.Groups[1].Value });
                }
            }

            return countries;
        }
    }
}
