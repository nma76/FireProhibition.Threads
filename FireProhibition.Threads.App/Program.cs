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
            // Read config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

            if (appSettings == null)
            {
                Console.WriteLine("Configuration file missing");
                return;
            }

            /* 
            TODO:
            Add code to filter out municipalities with prohibition and create a post
            OR
            Create post with "No municipalities have fire prohibition" 
             
             */

            // Get Fire prohibitions
            //prohibitionAPI api = new();
            //var municipalityprohibitionStatus = await api.GetFireProhibitionsAsync();
            //var filteredprohibitionStatus = municipalityprohibitionStatus.Where(x => x.FireProhibition.StatusCode == 1 || x.FireProhibition.StatusCode == 3 || x.FireProhibition.StatusCode == 4);

            //Console.WriteLine("Status for all municipalities:");
            //foreach (var item in municipalityprohibitionStatus)
            //{
            //    Console.WriteLine($"{item.Municipality,-30}{item.FireProhibition.Status,20}");
            //}

            //if (filteredprohibitionStatus.Any())
            //{
            //    Console.WriteLine("----------------------------------------");

            //    Console.WriteLine("Status for municipalities with prohibition:");
            //    foreach (var item in filteredprohibitionStatus)
            //    {
            //        Console.WriteLine($"{item.Municipality,-30}{item.FireProhibition.Status,20}");
            //    }
            //}

            //// Post to Threads
            //ThreadsAPI threadsApi = new(appSettings.Threads.UserId, appSettings.Threads.ApiKey);
            //var result = await threadsApi.CreateTextPost("Testing tags via API. #threadsapi");
            //Console.WriteLine($"Status for creating post: {result}");
        }
    }
}
