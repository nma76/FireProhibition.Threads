using FireProhibition.Lib;
using FireProhibition.Threads.App.Interface;
using FireProhibition.Threads.App.Settings;

namespace FireProhibition.Threads.App
{
    internal class App : IApp
    {
        private readonly AppSettings _appSettings;
        private readonly IThreadsPost _threadsPost;

        public App(AppSettings appSettings, IThreadsPost threadsPost)
        {
            _appSettings = appSettings;
            _threadsPost = threadsPost;
        }

        public void DoWork()
        {
            // Get Fire prohibitions
            ProhibitionAPI prohibitionApi = new();
            var prohibitionStatus = prohibitionApi.GetFireProhibitionsAsync().Result;
            //var riskStatus = prohibitionApi.GetFireRiskAsync().Result;

            //Create a post
            var prohibitionPostContent = _threadsPost.CreateTextPost(prohibitionStatus);
            //var riskPostContent = _threadsPost.CreateTextPost(riskStatus);

            // Write post content to console
            Console.WriteLine(prohibitionPostContent);
            //Console.WriteLine(riskPostContent);

            //// Post to Threads
            //ThreadsAPI threadsApi = new(appSettings.Threads.UserId, appSettings.Threads.ApiKey);
            //var result = await threadsApi.CreateTextPost(postContent);
            //Console.WriteLine($"Status for creating post: {result}");
        }
    }
}
