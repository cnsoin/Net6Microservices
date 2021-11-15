namespace Common.Shared.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool HasValue(this string? str, bool isTrimSpace = true)
        {
            if (isTrimSpace)
                str = str?.Trim();

            return !string.IsNullOrEmpty(str);
        }
    }
}