using System.Net;
using Threads.Lib.Model;
using Json.Lib;

namespace Threads.Lib
{
    public class ThreadsAPI
    {
        // Variables to hold passed configuration
        private readonly string _userId;
        private readonly string _apiKey;

        // Constructor
        public ThreadsAPI(string userId, string apiKey)
        {
            _userId = userId;
            _apiKey = apiKey;
        }

        // Create HttpClient with Base Address
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(Constants.ApiBase)
        };

        // Create text post
        public async Task<bool> CreateTextPost(string content)
        {
            // Make sure there is content
            if (content == null || content.Length > 500)
            {
                return false;
            }

            // Create endpoint uri for creating post
            var uri = string.Format(Constants.CreatePostEndpoint, _userId, content);

            // Create a request message with authentication etc.
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri, UriKind.Relative),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), $"Bearer {_apiKey}" },
                    { HttpRequestHeader.ContentType.ToString(), "application/json" }
                },
            };

            // Send and get the response
            using HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);

            // Make sure we get status 200
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Read JSON response and deserialize it
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseObject = Converter.DeserializeJson<ThreadsResponse>(jsonResponse);

                // Get id of created post so we can publish it
                if (responseObject != null && !string.IsNullOrEmpty(responseObject.id))
                {
                    // Create endpoint uri for publishing post, using the previously returned id
                    uri = string.Format(Constants.PublishPostEndpoint, _userId, responseObject.id);

                    // Create a request message, with authentication etc.
                    httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(uri, UriKind.Relative),
                        Headers = {
                            { HttpRequestHeader.Authorization.ToString(), $"Bearer {_apiKey}" },
                            { HttpRequestHeader.ContentType.ToString(), "application/json" }
                        }
                    };

                    // Send and get the response
                    using HttpResponseMessage publishResponse = await _client.SendAsync(httpRequestMessage);
                    if(publishResponse.StatusCode == HttpStatusCode.OK)
                    {
                        jsonResponse = await response.Content.ReadAsStringAsync();
                        responseObject = Converter.DeserializeJson<ThreadsResponse>(jsonResponse);

                        // If all went good, return true
                        return true;
                    }

                }
            }
            return false;
        }
    }
}