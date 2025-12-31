using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Incident Report - Analytics and reporting for incidents
    /// Aggregates incident data for trend analysis and reporting
    /// </summary>
    public class IncidentReport : BaseEntity
    {
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Report Details
        public required string ReportName { get; set; }
        public string? Description { get; set; }
        public DateTime ReportGeneratedDate { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }

        // Incident Statistics
        public int TotalIncidents { get; set; }
        public int HighSeverityCount { get; set; }
        public int MediumSeverityCount { get; set; }
        public int LowSeverityCount { get; set; }

        // Incident Types Breakdown
        public int BehavioralIncidents { get; set; }
        public int InjuryIncidents { get; set; }
        public int SafetyIncidents { get; set; }
        public int MedicalIncidents { get; set; }
        public int OtherIncidents { get; set; }

        // Status Breakdown
        public int OpenIncidents { get; set; }
        public int ClosedIncidents { get; set; }
        public int InvestigationIncidents { get; set; }

        // Trend Analysis
        public decimal IncidentTrend { get; set; } // Percentage change from previous period
        public bool TrendingUp { get; set; }
        public string? PeakIncidentDay { get; set; } // Day of week with most incidents
        public string? CommonIncidentType { get; set; }

        // Young Person Impact
        public int YoungPeopleAffected { get; set; }
        public int RepeatedOffenders { get; set; }

        // Recommendations
        public string? RecommendedActions { get; set; }
        public bool RequiresImmedateAction { get; set; }

        // Metadata
        public required string GeneratedByUserId { get; set; }
        public string? ApprovedByUserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? FilePath { get; set; } // PDF/Excel file location
    }
}
