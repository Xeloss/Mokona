namespace Mokona.Utils.Extensions
{
    using Newtonsoft.Json;

    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        public static string ApplyFormat(this string item, params object[] args)
        {
            return string.Format(item, args);
        }

        public static T DeserealizeAs<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
