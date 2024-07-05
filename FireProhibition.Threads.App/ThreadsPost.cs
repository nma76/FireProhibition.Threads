using FireProhibition.Lib.Model;
using FireProhibition.Threads.App.Interface;
using FireProhibition.Threads.App.Settings;

namespace FireProhibition.Threads.App
{
    internal class ThreadsPost(AppSettings appSettings) : IThreadsPost
    {
        private readonly AppSettings _appSettings = appSettings;

        public string CreateTextPost(List<FireProhibitionStatus> fireProhibitions)
        {
            string content = string.Format(_appSettings.FireProhibitionContent, fireProhibitions.Count);
            foreach (var fireProhibition in fireProhibitions)
            {
                content += $"{fireProhibition.County}\n";
            }

            return FormatPost(content);
        }

        public string CreateTextPost(List<FireRiskStatus> riskStatuses)
        {
            string content = _appSettings.FireRiskContent;
            foreach (var riskStatus in riskStatuses)
            {
                content += $"{riskStatus.Location?.Name}: {riskStatus.Forecast.RiskIndex}\n";
            }

            return FormatPost(content);
        }

        string FormatPost(string content)
        {
            var formattedContent = content.Length > (500 - _appSettings.Byline.Length) ? string.Concat(content.AsSpan(0, 440), "...") : content;
            return formattedContent + _appSettings.Byline;
        }
    }
}
