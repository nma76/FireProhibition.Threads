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

        // Load the file containing locations and return them as array
        internal static Location[] GetLocations()
        {
            var locations = Converter.ReadJson<Location[]>(Constants.DataPath);
            return locations ?? [];
        }

        // Fetch fire prohibition status for all locations
        public async Task<List<FireProhibitionStatus>> GetFireProhibitionsAsync(bool returnAll = false)
        {
            // List to store all fire prohibitions
            List<FireProhibitionStatus> result = [];

            // Get list of all locations from data file
            var locations = GetLocations();

            // Iterate all locations and fetch current fire prohibition status
            foreach (var location in locations)
            {
                // Create endpoint uri for fetching fire prohibition
                var uri = string.Format(Constants.FireProhibitionEndpoint, location.Latitude.ToString(Constants.NumberFormat), location.Longitude.ToString(Constants.NumberFormat));

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
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get JSON response and deserialize
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var FireProhibition = Converter.DeserializeJson<FireProhibitionStatus>(jsonResponse);

                    // Add to list
                    if (FireProhibition != null)
                    {
                        // Only add location if returnAll = true or the location has a fire prohibition
                        if (returnAll || FireProhibition.FireProhibition.StatusCode is 1 or 3 or 4)
                        {
                            result.Add(FireProhibition);
                        }
                    }
                }
            }

            return result;
        }
    }
}
