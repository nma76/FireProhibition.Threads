namespace FireProhibition.Lib.Model
{
    public class FireRiskStatus
    {
        public Location? Location { get; set; }
        public required DateTime PeriodStartDate { get; set; }
        public required DateTime PeriodEndDate { get; set; }
        public required Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        public required DateTime Date { get; set; }
        public required DateTime IssuedDate { get; set; }
        public required int ForecastTypeId { get; set; }
        public required string ForecastTypeName { get; set; }
        public required int FwiIndex { get; set; }
        public required string FwiMessage { get; set; }
        public required int CombustibleIndex { get; set; }
        public required string CombustibleMessage { get; set; }
        public required int GrassIndex { get; set; }
        public required string GrassMessage { get; set; }
        public required int WoodIndex { get; set; }
        public required string WoodMessage { get; set; }
        public required int RiskIndex { get; set; }
        public required string RiskMessage { get; set; }
    }

}
