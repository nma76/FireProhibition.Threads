using FireProhibition.Threads.App.Interface;
using FireProhibition.Threads.App.Settings;

namespace FireProhibition.Threads.App
{
    internal class App : IApp
    {
        private readonly AppSettings _appSettings;

        public App(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void DoWork()
        {
            //// Get Fire prohibitions
            //ProhibitionAPI prohibitionApi = new();
            ////var prohibitionStatus = await prohibitionApi.GetFireProhibitionsAsync();
            //var riskStatus = await prohibitionApi.GetFireRiskAsync();

            ////Create a post
            ////var prohibitionPostContent = ThreadsPost.CreateTextPost(prohibitionStatus);
            //var riskPostContent = ThreadsPost.CreateTextPost(riskStatus);

            //// Write post content to console
            ////Console.WriteLine(prohibitionPostContent);
            //Console.WriteLine(riskPostContent);

            ////// Post to Threads
            ////ThreadsAPI threadsApi = new(appSettings.Threads.UserId, appSettings.Threads.ApiKey);
            ////var result = await threadsApi.CreateTextPost(postContent);
            ////Console.WriteLine($"Status for creating post: {result}");
        }
    }
}
