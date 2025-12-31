namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Facility Report - High-level facility performance and statistics
    /// Aggregates all data types for facility-wide reporting
    /// </summary>
    public class FacilityReport : BaseEntity
    {
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Report Details
        public required string ReportName { get; set; }
        public string? Description { get; set; }
        public DateTime ReportGeneratedDate { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }

        // Facility Overview
        public int TotalYoungPeopleServed { get; set; }
        public int TotalStaffMembers { get; set; }
        public int TotalRooms { get; set; }
        public int OccupancyRate { get; set; } // Percentage

        // Safety Metrics
        public int TotalIncidentsReported { get; set; }
        public int HighRiskIncidents { get; set; }
        public int SafeguardingConcernsReported { get; set; }
        public int ActiveSafetyAlerts { get; set; }
        public decimal IncidentReductionRate { get; set; } // From previous period

        // Health Metrics
        public int YoungPeopleWithHealthAssessments { get; set; }
        public int YoungPeopleOnMedications { get; set; }
        public int HealthConcernsIdentified { get; set; }
        public decimal HealthGoalCompletionRate { get; set; }

        // Compliance Metrics
        public int AuditLogsRecorded { get; set; }
        public int ComplianceViolations { get; set; }
        public decimal ComplianceScore { get; set; } // 0-100

        // Staff Performance
        public int TrainingCompleted { get; set; }
        public int StaffTurnovers { get; set; }
        public decimal StaffRetentionRate { get; set; }

        // Incident Breakdown by Severity
        public int CriticalSeverity { get; set; }
        public int HighSeverity { get; set; }
        public int MediumSeverity { get; set; }
        public int LowSeverity { get; set; }

        // Young Person Outcomes
        public int YoungPeopleDischargedSuccessfully { get; set; }
        public int YoungPeopleReadmitted { get; set; }
        public decimal SuccessfulOutcomeRate { get; set; }

        // Resource Utilization
        public int DocumentsStored { get; set; }
        public int MedicalRecordsOnFile { get; set; }
        public decimal StorageUtilizationPercentage { get; set; }

        // Key Performance Indicators (KPIs)
        public string? KPI1_Name { get; set; }
        public decimal? KPI1_Value { get; set; }
        public string? KPI2_Name { get; set; }
        public decimal? KPI2_Value { get; set; }
        public string? KPI3_Name { get; set; }
        public decimal? KPI3_Value { get; set; }

        // Strategic Insights
        public string? TopStrengths { get; set; }
        public string? AreaOfImprovement { get; set; }
        public string? StrategicRecommendations { get; set; }
        public bool RequiresExecutiveAttention { get; set; }

        // Metadata
        public required string GeneratedByUserId { get; set; }
        public string? ApprovedByUserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? FilePath { get; set; } // PDF/Excel file location
    }
}
