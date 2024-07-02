using FireProhobition.Lib;
using FireProhobition.Threads.App.Settings;
using Microsoft.Extensions.Configuration;
using Threads.Lib;

namespace FireProhobition.Threads.App
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

            //// Get Fire Prohobitions
            //ProhobitionAPI api = new();
            //var municipalityProhobitionStatus = await api.GetFireProhobitionsAsync();
            //var filteredProhobitionStatus = municipalityProhobitionStatus.Where(x => x.FireProhibition.StatusCode == 1 || x.FireProhibition.StatusCode == 3 || x.FireProhibition.StatusCode == 4);

            //Console.WriteLine("Status for all municipalities:");
            //foreach (var item in municipalityProhobitionStatus)
            //{
            //    Console.WriteLine($"{item.Municipality,-30}{item.FireProhibition.Status,20}");
            //}

            //if (filteredProhobitionStatus.Any())
            //{
            //    Console.WriteLine("----------------------------------------");

            //    Console.WriteLine("Status for municipalities with prohobition:");
            //    foreach (var item in filteredProhobitionStatus)
            //    {
            //        Console.WriteLine($"{item.Municipality,-30}{item.FireProhibition.Status,20}");
            //    }
            //}

            //// Post to Threads
            //ThreadsAPI api = new(threadsSettings.UserId, threadsSettings.ApiKey);
            //await api.Test();
        }
    }
}
