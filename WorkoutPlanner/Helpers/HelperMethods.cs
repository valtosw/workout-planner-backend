using System.Globalization;

namespace WorkoutPlanner.Helpers
{
    public class HelperMethods
    {
        public static string CapitalizeEachWord(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
