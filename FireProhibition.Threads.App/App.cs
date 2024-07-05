using FireProhibition.Lib;
using FireProhibition.Threads.App.Interface;
using FireProhibition.Threads.App.Settings;
using Threads.Lib;

namespace FireProhibition.Threads.App
{
    internal class App(AppSettings appSettings, IThreadsPost threadsPost) : IApp
    {
        private readonly AppSettings _appSettings = appSettings;
        private readonly IThreadsPost _threadsPost = threadsPost;

        public void DoWork()
        {
            // Get Fire prohibitions
            ProhibitionAPI prohibitionApi = new();
            var prohibitionStatus = prohibitionApi.GetFireProhibitionsAsync().Result;
            var riskStatus = prohibitionApi.GetFireRiskAsync().Result;

            //Create a post
            var prohibitionPostContent = _threadsPost.CreateTextPost(prohibitionStatus);
            var riskPostContent = _threadsPost.CreateTextPost(riskStatus);

            // Write post content to console
            Console.WriteLine(prohibitionPostContent);
            Console.WriteLine(riskPostContent);

            // Post to Threads
            bool result;

            ThreadsAPI threadsApi = new(_appSettings.Threads.UserId, _appSettings.Threads.ApiKey);
            result = threadsApi.CreateTextPost(prohibitionPostContent).Result;
            Console.WriteLine($"Status for creating post: {result}");

             result = threadsApi.CreateTextPost(riskPostContent).Result;
            Console.WriteLine($"Status for creating post: {result}");
        }
    }
}
