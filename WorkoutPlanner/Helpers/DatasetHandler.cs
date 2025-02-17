using WorkoutPlanner.Models;

namespace WorkoutPlanner.Helpers
{
    public class DatasetHandler
    {
        public static (List<MuscleGroup>, List<Exercise>) GetSeedData(string filePath)
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
                        Name = muscleGroupName,
                        Exercises = []
                    };
                    muscleGroupsDict[muscleGroupName] = muscleGroup;
                }

                var exercise = new Exercise
                {
                    Id = exerciseIdCounter++,
                    Name = exerciseName,
                    MuscleGroupId = muscleGroup.Id,
                    MuscleGroup = muscleGroup
                };

                muscleGroup.Exercises.Add(exercise);
                exercises.Add(exercise);
            }

            return (muscleGroupsDict.Values.ToList(), exercises);
        }
    }
}
