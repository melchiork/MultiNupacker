namespace MultiNupacker.Packing.Extensions
{
    internal static class StringExtensions
    {
        private const string Unknown = "unknown";

        public static string UnknownIfEmptyOrNull(this string input)
        {
            return string.IsNullOrEmpty(input) ? Unknown : input;
        }
    }
}