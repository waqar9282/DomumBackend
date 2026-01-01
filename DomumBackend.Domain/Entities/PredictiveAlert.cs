namespace DomumBackend.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class PredictiveAlert : BaseEntity
{
    public string FacilityId { get; set; }
    public long AnalyticsMetricId { get; set; }
    public string AlertName { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual Facility Facility { get; set; }
    public virtual AnalyticsMetric AnalyticsMetric { get; set; }
    public string AlertType { get; set; } // ThresholdBreach, AnomalyDetected, TrendDeviation, ResourceShortage, ComplianceRisk, PredictedFailure, HealthRisk
    public string AlertCategory { get; set; } // Critical, High, Medium, Low, Info
    public string Status { get; set; } // Active, Acknowledged, Investigating, Resolved, Dismissed, Expired
    public DateTime AlertGeneratedDate { get; set; }
    public DateTime? AlertDueDate { get; set; }
    public DateTime? AlertResolvedDate { get; set; }
    public string Description { get; set; }
    public string DetailedDescription { get; set; }
    public decimal? CurrentValue { get; set; }
    public decimal? ThresholdValue { get; set; }
    public decimal? VarianceFromExpected { get; set; }
    public int? DaysUntilCritical { get; set; }
    public int? DaysSinceAlertStart { get; set; }
    public int PredictionConfidence { get; set; } // 0-100, ML model confidence
    public string PredictionModel { get; set; } // LSTMNetwork, RandomForest, GradientBoosting, TimeSeriesAnalysis, RuleBase
    public DateTime? PredictedEventDate { get; set; }
    public List<string> ImpactedAreas { get; set; } = new(); // Systems, departments affected
    public List<Guid> ImpactedResourceIds { get; set; } = new();
    public int? EstimatedResourcesAtRisk { get; set; }
    public string RiskLevel { get; set; } // Minimal, Low, Medium, High, Critical
    public int RiskScore { get; set; } // 0-100
    public int UrgencyScore { get; set; } // 0-100
    [NotMapped]
    public List<string> RecommendedActions { get; set; } = new();
    public string PrimaryRecommendation { get; set; }
    public bool RequiresImmediateAction { get; set; }
    public string ActionAssignedTo { get; set; } // Role or user
    public Guid? AssignedUserId { get; set; }
    public DateTime? ActionDueDate { get; set; }
    public string ActionStatus { get; set; } // NotStarted, InProgress, Completed, Overdue, Cancelled
    public DateTime? ActionCompletedDate { get; set; }
    public string ActionNotes { get; set; }
    [NotMapped]
    public List<string> NotificationRecipients { get; set; } = new(); // Email, SMS, InApp
    public DateTime? LastNotificationDate { get; set; }
    public int NotificationCount { get; set; }
    public bool AcknowledgedByUser { get; set; }
    public Guid? AcknowledgedByUserId { get; set; }
    public DateTime? AcknowledgedDate { get; set; }
    public string AcknowledgmentNotes { get; set; }
    public bool IsRecurring { get; set; }
    public int RecurrenceCount { get; set; }
    public DateTime? LastRecurrenceDate { get; set; }
    public string HistoricalFrequency { get; set; } // FirstTime, Rare, Occasional, Regular, Frequent
    [NotMapped]
    public List<Guid> RelatedAlertIds { get; set; } = new();
    public bool IsManuallyCreated { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public int EscalationLevel { get; set; } // 0: Initial, 1-3: Escalation levels
    public DateTime? EscalatedDate { get; set; }
    public Guid? EscalatedToUserId { get; set; }
    public string EscalationReason { get; set; }
    [NotMapped]
    public Dictionary<string, object> AlertContext { get; set; } = new(); // Additional context data
    [NotMapped]
    public Dictionary<string, string> Metadata { get; set; } = new();
}
