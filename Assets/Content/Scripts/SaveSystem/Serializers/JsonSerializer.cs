using Newtonsoft.Json;

namespace SaveSystem.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return json;
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}