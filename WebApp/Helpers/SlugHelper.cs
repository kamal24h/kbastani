namespace WebApp.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = input.Trim().ToLowerInvariant();

            // فقط حروف، اعداد و فاصله
            input = System.Text.RegularExpressions.Regex.Replace(input, @"[^a-z0-9\s-]", "");

            // تبدیل چند فاصله به یکی
            input = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", " ").Trim();

            // تبدیل فاصله به خط تیره
            input = input.Replace(" ", "-");

            return input;
        }
    }

}
