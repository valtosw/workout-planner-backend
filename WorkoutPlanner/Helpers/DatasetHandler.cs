using System.Reflection.Metadata.Ecma335;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Helpers
{
    public class DatasetHandler
    {
        public static (List<MuscleGroup>, List<Exercise>) GetSeedData(string datasetPath)
        {
            var lines = File.ReadAllLines(datasetPath);

            var muscleGroupsDict = new Dictionary<string, int>();
            var exercises = new List<Exercise>();
            var muscleGroups = new List<MuscleGroup>();

            int muscleGroupId = 1;
            int exerciseId = 1;

            foreach (var line in lines.Distinct())
            {
                var parts = line.Split(',');
                if (parts.Length != 2) continue;

                var exerciseName = parts[0].Trim();
                var muscleGroupName = parts[1].Trim();

                if (!muscleGroupsDict.TryGetValue(muscleGroupName, out int value))
                {
                    value = muscleGroupId++;
                    muscleGroupsDict[muscleGroupName] = value;
                    muscleGroups.Add(new MuscleGroup
                    {
                        Id = muscleGroupsDict[muscleGroupName],
                        Name = muscleGroupName
                    });
                }

                exercises.Add(new Exercise
                {
                    Id = exerciseId++,
                    Name = exerciseName,
                    MuscleGroupId = value
                });
            }

            return (muscleGroups, exercises);
        }
    }
}
