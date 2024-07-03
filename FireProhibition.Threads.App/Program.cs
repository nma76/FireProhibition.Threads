using FireProhibition.Lib;
using FireProhibition.Threads.App.Settings;
using Microsoft.Extensions.Configuration;
using Threads.Lib;

namespace FireProhibition.Threads.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Read config file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            // Get config section as typed object
            IConfiguration config = builder.Build();
            var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

            if (appSettings == null)
            {
                Console.WriteLine("Configuration file missing");
                return;
            }

            // Get Fire prohibitions
            ProhibitionAPI prohibitionApi = new();
            var prohibitionStatus = await prohibitionApi.GetFireProhibitionsAsync();

            //Create a post
            var postContent = ThreadsPost.CreateTextPost(prohibitionStatus);

            // Write post content to console
            Console.WriteLine(postContent);

            //// Post to Threads
            ThreadsAPI threadsApi = new(appSettings.Threads.UserId, appSettings.Threads.ApiKey);
            var result = await threadsApi.CreateTextPost(postContent);
            Console.WriteLine($"Status for creating post: {result}");
        }
    }
}
