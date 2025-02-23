using Newtonsoft.Json;

namespace Content.Scripts.Services
{
    public static class JsonProjectSettings
    {
        public static void ApplyProjectSerializationSettings()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            JsonConvert.DefaultSettings = () => settings;
        }
    }
}