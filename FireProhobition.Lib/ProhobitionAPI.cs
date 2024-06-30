using FireProhobition.Lib.Model;
using System.Text.Json;
using System.Linq;

namespace FireProhobition.Lib
{
    public class ProhobitionAPI
    {
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(Constants.ApiBase)
        };

        internal static T? ReadJson<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return DeserializeJson<T>(text);
        }
        internal static async Task<T?> ReadJsonAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await DeserializeJson<T>(stream);
        }
        internal static T? DeserializeJson<T>(string text)
        {
            return JsonSerializer.Deserialize<T>(text, Constants.SerializerOptions);
        }
        internal static async Task<T?> DeserializeJson<T>(FileStream stream)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, Constants.SerializerOptions);
        }

        internal Municipality[] GetMunicipalities()
        {
            var municipalities = ReadJson<Municipality[]>(Constants.MunicipalityDataPath);
            return municipalities ?? [];
        }

        public async Task<List<FireProhobitionStatus>> GetFireProhobitionsAsync()
        {
            List<FireProhobitionStatus> result = [];

            var municipalities = GetMunicipalities();
            foreach (var municipality in municipalities)
            {
                var uri = string.Format(Constants.FireProhobitionEndpoint, municipality.Latitude.ToString(Constants.NumberFormat), municipality.Longitude.ToString(Constants.NumberFormat));

                using HttpResponseMessage response = await _client.GetAsync(uri);

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var fireProhobition = DeserializeJson<FireProhobitionStatus>(jsonResponse);
                if (fireProhobition != null)//TODO && (fireProhobition.FireProhibition.StatusCode == 1 || fireProhobition.FireProhibition.StatusCode == 3 || fireProhobition.FireProhibition.StatusCode == 4))
                {
                    result.Add(fireProhobition);
                }
            }

            return result;
        }
    }
}
