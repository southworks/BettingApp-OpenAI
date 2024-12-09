using System.Text.Json;

namespace OpenAIPoC.API.Options
{
    public class JsonOptions
    {
        public JsonSerializerOptions SerializerOptions { get; }

        public JsonOptions()
        {
            SerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }
}
