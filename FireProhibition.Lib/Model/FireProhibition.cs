namespace FireProhibition.Lib.Model
{
    public class FireProhibitionStatus
    {
        public required string County { get; set; }
        public required string CountyCode { get; set; }
        public required string Municipality { get; set; }
        public required string MunicipalityCode { get; set; }
        public required Fireprohibition FireProhibition { get; set; }
    }

    public class Fireprohibition
    {
        public required string Status { get; set; }
        public required string StatusMessage { get; set; }
        public required int StatusCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? RevisionDate { get; set; }
        public required string Description { get; set; }
        public required string Authority { get; set; }
        public required string Url { get; set; }
    }

}
