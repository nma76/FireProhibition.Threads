using FireProhibition.Lib.Model;

namespace FireProhibition.Threads.App
{
    internal class ThreadsPost
    {
        internal static readonly string byline = "\nCreated by https://github.com/nma76/FireProhibition.Threads";

        internal static string CreateTextPost(List<FireProhibitionStatus> fireProhibitions)
        {
            string content;
            if (fireProhibitions.Count == 0)
            {
                content = $"Just nu finns inga eldningsförbud i Värmland!\n";
            }
            else
            {
                content = $"Just nu är det eldningsförbud i {fireProhibitions.Count} kommuner i Värmland!\n";
                foreach (var fireProhibition in fireProhibitions)
                {
                    content += $"{fireProhibition.County}\n";
                }
            }

            return FormatPost(content);
        }

        internal static string CreateTextPost(List<FireRiskStatus> riskStatuses)
        {
            string content = "Brandrisk i Värmland:\n";
            foreach (var riskStatus in riskStatuses)
            {
                content += $"{riskStatus.Location?.Name}: {riskStatus.Forecast.RiskIndex}\n";
            }

            return FormatPost(content);
        }

        internal static string FormatPost(string content)
        {
            var formattedContent = content.Length > (500 - byline.Length) ? string.Concat(content.AsSpan(0, 440), "...") : content;
            return formattedContent + byline;
        }
    }
}
