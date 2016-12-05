namespace Mokona.Utils.Extensions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static object GetValueOf(this object obj, string path)
        {
            var members = path.Split('.');

            return InnerGetValueOf(obj, 0, members);
        }

        private static object InnerGetValueOf(object obj, int index, string[] memberNames)
        {
            if (index >= memberNames.Length)
                return obj;

            var member = memberNames[index];

            var type = obj.GetType();
            var property = type.GetProperty(member);

            var value = property.GetValue(obj);

            return InnerGetValueOf(value, ++index, memberNames);
        }
    }
}
