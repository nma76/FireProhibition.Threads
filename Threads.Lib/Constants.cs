using System.Text.Json;

namespace Threads.Lib
{
    internal class Constants
    {
        internal const string ApiBase = "https://graph.threads.net/v1.0/";
        internal const string CreatePostEndpoint = "{0}/threads/?media_type=TEXT&text={1}";
        internal const string PublishPostEndpoint = "{0}/threads_publish/?creation_id={1}";
    }
}
