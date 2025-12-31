namespace DomumBackend.Application.DTOs
{
    /// <summary>
    /// DTO for Incident Report responses
    /// </summary>
    public class IncidentReportDTO
    {
        public string Id { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public DateTime ReportGeneratedDate { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }
        public int TotalIncidents { get; set; }
        public int HighSeverityCount { get; set; }
        public int MediumSeverityCount { get; set; }
        public int LowSeverityCount { get; set; }
        public int OpenIncidents { get; set; }
        public int ClosedIncidents { get; set; }
        public decimal IncidentTrend { get; set; }
        public bool TrendingUp { get; set; }
        public string CommonIncidentType { get; set; }
        public string RecommendedActions { get; set; }
        public bool IsApproved { get; set; }
        public string FilePath { get; set; }
    }

    /// <summary>
    /// DTO for Health Metrics Report responses
    /// </summary>
    public class HealthMetricsReportDTO
    {
        public string Id { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public DateTime ReportGeneratedDate { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }
        public int TotalAssessments { get; set; }
        public decimal AverageHeight { get; set; }
        public decimal AverageWeight { get; set; }
        public decimal AverageBodyMassIndex { get; set; }
        public int YoungPeopleOnMedication { get; set; }
        public int NutritionLogsRecorded { get; set; }
        public int ActivityLogsRecorded { get; set; }
        public int MentalHealthCheckInsRecorded { get; set; }
        public int YoungPeopleFlaggedForConcern { get; set; }
        public decimal ActivityComplianceRate { get; set; }
        public string RecommendedActions { get; set; }
        public bool IsApproved { get; set; }
        public string FilePath { get; set; }
    }

    /// <summary>
    /// DTO for Facility Report responses
    /// </summary>
    public class FacilityReportDTO
    {
        public string Id { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public DateTime ReportGeneratedDate { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }
        public int TotalYoungPeopleServed { get; set; }
        public int TotalStaffMembers { get; set; }
        public int OccupancyRate { get; set; }
        public int TotalIncidentsReported { get; set; }
        public int HighRiskIncidents { get; set; }
        public int SafeguardingConcernsReported { get; set; }
        public decimal IncidentReductionRate { get; set; }
        public int YoungPeopleWithHealthAssessments { get; set; }
        public decimal ComplianceScore { get; set; }
        public decimal SuccessfulOutcomeRate { get; set; }
        public string TopStrengths { get; set; }
        public string AreaOfImprovement { get; set; }
        public string StrategicRecommendations { get; set; }
        public bool RequiresExecutiveAttention { get; set; }
        public string FilePath { get; set; }
    }

    /// <summary>
    /// Generic response for report generation
    /// </summary>
    public class ReportGenerationResponseDTO
    {
        public string ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportType { get; set; } // Incident, Health, Facility
        public DateTime GeneratedDate { get; set; }
        public string FilePath { get; set; }
        public string DownloadUrl { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
