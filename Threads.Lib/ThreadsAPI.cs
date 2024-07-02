using System.Net;
using Threads.Lib.Model;
using Json.Lib;

namespace Threads.Lib
{
    public class ThreadsAPI
    {
        private readonly string _userId;
        private readonly string _apiKey;

        public ThreadsAPI(string userId, string apiKey)
        {
            _userId = userId;
            _apiKey = apiKey;
        }

        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(Constants.ApiBase)
        };

        public async Task Test()
        {
            var text = "Hello World from API";

            // Create a new post
            var uri = string.Format(Constants.CreatePostEndpoint, _userId, text);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri, UriKind.Relative),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), $"Bearer {_apiKey}" },
                    { HttpRequestHeader.ContentType.ToString(), "application/json" }
                },
            };
            using HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObject = Converter.DeserializeJson<ThreadsResponse>(jsonResponse);

            if (responseObject != null && !string.IsNullOrEmpty(responseObject.id))
            {
                //Publish Post
                uri = string.Format(Constants.PublishPostEndpoint, _userId, responseObject.id);

                httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri, UriKind.Relative),
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {_apiKey}" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };

                using HttpResponseMessage publishResponse = await _client.SendAsync(httpRequestMessage);
                response.EnsureSuccessStatusCode();

                jsonResponse = await response.Content.ReadAsStringAsync();
                responseObject = Converter.DeserializeJson<ThreadsResponse>(jsonResponse);
            }
        }
    }
}