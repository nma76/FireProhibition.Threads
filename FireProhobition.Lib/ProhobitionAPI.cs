using FireProhobition.Lib.Model;
using System.Text.Json;

namespace FireProhobition.Lib
{
    public class ProhobitionAPI
    {
        HttpClient _client;

        internal static T? ReadJson<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }

        public Municipality[] GetMunicipalities()
        {
            var municipalities = ReadJson<Municipality[]>(Constants.MunicipalityDataPath);
            return municipalities ?? [];
        }
    }
}
