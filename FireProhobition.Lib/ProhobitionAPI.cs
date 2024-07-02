using FireProhobition.Lib.Model;
using Json.Lib;
using System.Net;

namespace FireProhobition.Lib
{
    public class ProhobitionAPI
    {
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(Constants.ApiBase)
        };

        internal Municipality[] GetMunicipalities()
        {
            var municipalities = Converter.ReadJson<Municipality[]>(Constants.MunicipalityDataPath);
            return municipalities ?? [];
        }

        public async Task<List<FireProhobitionStatus>> GetFireProhobitionsAsync()
        {
            List<FireProhobitionStatus> result = [];

            var municipalities = GetMunicipalities();
            foreach (var municipality in municipalities)
            {
                var uri = string.Format(Constants.FireProhobitionEndpoint, municipality.Latitude.ToString(Constants.NumberFormat), municipality.Longitude.ToString(Constants.NumberFormat));

                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,                    
                    RequestUri = new Uri(uri, UriKind.Relative),
                    Headers = {
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };

                using HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var fireProhobition = Converter.DeserializeJson<FireProhobitionStatus>(jsonResponse);
                if (fireProhobition != null)
                {
                    result.Add(fireProhobition);
                }
            }

            return result;
        }
    }
}
