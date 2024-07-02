using FireProhibition.Lib.Model;
using Json.Lib;
using System.Net;

namespace FireProhibition.Lib
{
    public class ProhibitionAPI
    {
        // Create HttpClient with Base Address
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(Constants.ApiBase)
        };

        // Load the file containing municipalities and return them as array
        internal static Municipality[] GetMunicipalities()
        {
            var municipalities = Converter.ReadJson<Municipality[]>(Constants.MunicipalityDataPath);
            return municipalities ?? [];
        }

        // Fetch fire prohibition status for all municipalities
        public async Task<List<FireProhibitionStatus>> GetFireProhibitionsAsync()
        {
            // List to store all fire prohibitions
            List<FireProhibitionStatus> result = [];

            // Get list of all municipalities from data file
            var municipalities = GetMunicipalities();

            // Iterate all municipalities and fetch current fire prohibition status
            foreach (var municipality in municipalities)
            {
                // Create endpoint uri for fetching fire prohibition
                var uri = string.Format(Constants.FireProhibitionEndpoint, municipality.Latitude.ToString(Constants.NumberFormat), municipality.Longitude.ToString(Constants.NumberFormat));

                // Create request message
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,                    
                    RequestUri = new Uri(uri, UriKind.Relative),
                    Headers = {
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };

                // Send and get response
                using HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    // Get JSON response and deserialize
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var FireProhibition = Converter.DeserializeJson<FireProhibitionStatus>(jsonResponse);

                    // Add to list
                    if (FireProhibition != null)
                    {
                        result.Add(FireProhibition);
                    }
                }
            }

            return result;
        }
    }
}
