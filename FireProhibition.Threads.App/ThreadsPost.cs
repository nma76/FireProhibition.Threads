using FireProhibition.Lib.Model;

namespace FireProhibition.Threads.App
{
    internal class ThreadsPost
    {
        internal static string CreateTextPost(List<FireProhibitionStatus> fireProhibitions)
        {
            string content;
            var byline = "\nCreated by https://github.com/nma76/FireProhibition.Threads";

            if (fireProhibitions.Count == 0)
            {
                content = $"Just nu finns inga eldningsförbud i Värmland!\n";
            }
            else {
                content = $"Just nu är det eldningsförbud i {fireProhibitions.Count} kommuner i Värmland!\n";
                foreach (var fireProhibition in fireProhibitions)
                {
                    content += $"{fireProhibition.County}\n";
                    //if (fireProhibition.FireProhibition.Url != null)
                    //{
                    //    content += $"Läs mer: {fireProhibition.FireProhibition.Url}\n\n";
                    //}
                }

                if (content.Length > (500 - byline.Length))
                {
                    content = string.Concat(content.AsSpan(0, 440), "...");
                }
            }

            content += byline;
            return content;
        }
    }
}
