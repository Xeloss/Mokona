namespace Mokona.FrontEnd
{
    using Mokona.FrontEnd.WebApiExtensions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Web.Http;

    public static class FormatterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.Add(new JsonpFormatter());
            config.Formatters.Add(config.Formatters.JsonFormatter);
        }
    }
}
