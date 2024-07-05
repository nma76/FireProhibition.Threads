using FireProhibition.Threads.App.Interface;
using FireProhibition.Threads.App.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FireProhibition.Threads.App
{
    internal class Program
    {
        static void Main(string[] args)
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

            var serviceProvider = new ServiceCollection()
                .AddSingleton(appSettings)
                .AddSingleton<IApp, App>()
                .BuildServiceProvider();

            var app = serviceProvider.GetService<IApp>();
            app?.DoWork();
        }
    }
}
