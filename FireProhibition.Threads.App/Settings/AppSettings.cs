namespace FireProhibition.Threads.App.Settings
{
    internal class AppSettings
    {
        public required ThreadsSettings Threads {  get; set; }
        public required string Byline { get; set; }
        public required string FireProhibitionContent { get; set; }
        public required string FireRiskContent { get; set; }
    }
}
