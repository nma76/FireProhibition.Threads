using System.Text.Json;

namespace Json.Lib
{
    public class Config
    {
        public static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
