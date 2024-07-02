using System.Text.Json;

namespace Json.Lib
{
    public class Converter
    {
        public static T? ReadJson<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return DeserializeJson<T>(text);
        }
        public static async Task<T?> ReadJsonAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await DeserializeJson<T>(stream);
        }
        public static T? DeserializeJson<T>(string text)
        {
            return JsonSerializer.Deserialize<T>(text, Config.SerializerOptions);
        }
        public static async Task<T?> DeserializeJson<T>(FileStream stream)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, Config.SerializerOptions);
        }
    }
}
