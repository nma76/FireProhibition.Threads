using FireProhobition.Lib.Model;
using Json.Lib;
using System.Net;

namespace FireProhobition.Lib
{
    public class ProhobitionAPI
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

        // Fetch fire prohobition status for all municipalities
        public async Task<List<FireProhobitionStatus>> GetFireProhobitionsAsync()
        {
            // List to store all fire prohobitions
            List<FireProhobitionStatus> result = [];

            // Get list of all municipalities from data file
            var municipalities = GetMunicipalities();

            // Iterate all municipalities and fetch current fire prohobition status
            foreach (var municipality in municipalities)
            {
                // Create endpoint uri for fetching fire prohobition
                var uri = string.Format(Constants.FireProhobitionEndpoint, municipality.Latitude.ToString(Constants.NumberFormat), municipality.Longitude.ToString(Constants.NumberFormat));

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
                    var fireProhobition = Converter.DeserializeJson<FireProhobitionStatus>(jsonResponse);

                    // Add to list
                    if (fireProhobition != null)
                    {
                        result.Add(fireProhobition);
                    }
                }
            }

            return result;
        }
    }
}
