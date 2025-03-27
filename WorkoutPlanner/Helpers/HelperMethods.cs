using System.Globalization;

namespace WorkoutPlanner.Helpers
{
    public static class HelperMethods
    {
        public static string CapitalizeEachWord(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}