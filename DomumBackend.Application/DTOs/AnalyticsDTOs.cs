namespace DomumBackend.Application.DTOs;

using System;
using System.Collections.Generic;

// Analytics Metric DTOs
public class AnalyticsMetricDTO
{
    public long Id { get; set; }
    public string FacilityId { get; set; }
    public string MetricName { get; set; }
    public string MetricCode { get; set; }
    public decimal MetricValue { get; set; }
    public decimal? PreviousValue { get; set; }
    public decimal? ThresholdMin { get; set; }
    public decimal? ThresholdMax { get; set; }
    public string Unit { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public DateTime MeasurementDate { get; set; }
    public string Status { get; set; }
    public decimal? PercentageChange { get; set; }
    public string Interpretation { get; set; }
    public bool IsAnomalous { get; set; }
    public int AlertCount { get; set; }
    public int ComparisonScore { get; set; }
    public string BenchmarkComparison { get; set; }
    public bool IsKeyPerformanceIndicator { get; set; }
    public int ForecastConfidence { get; set; }
    public decimal? ForecastedValue { get; set; }
    public string ForecastModel { get; set; }
}

public class AnalyticsMetricCreateDTO
{
    public string MetricName { get; set; }
    public string MetricCode { get; set; }
    public decimal MetricValue { get; set; }
    public string Unit { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public decimal? ThresholdMin { get; set; }
    public decimal? ThresholdMax { get; set; }
    public string Formula { get; set; }
    public bool IsKeyPerformanceIndicator { get; set; }
    public int DisplayPriority { get; set; }
}

// Trend Analysis DTOs
public class TrendAnalysisDTO
{
    public long Id { get; set; }
    public long AnalyticsMetricId { get; set; }
    public string MetricName { get; set; }
    public string TrendType { get; set; }
    public string TrendStrength { get; set; }
    public decimal? TrendSlope { get; set; }
    public decimal? RSquaredValue { get; set; }
    public DateTime AnalysisStartDate { get; set; }
    public DateTime AnalysisEndDate { get; set; }
    public decimal? AverageValue { get; set; }
    public decimal? StandardDeviation { get; set; }
    public bool HasSeasonality { get; set; }
    public string Season { get; set; }
    public bool HasAnomalies { get; set; }
    public int AnomalyCount { get; set; }
    public string TrendChangeDescription { get; set; }
    public decimal? VolatilityIndex { get; set; }
    public string PredictedNextTrend { get; set; }
    public int ConfidenceLevel { get; set; }
    public string Interpretation { get; set; }
    public List<string> RecommendedActions { get; set; } = new();
}

// Predictive Alert DTOs
public class PredictiveAlertDTO
{
    public long Id { get; set; }
    public string FacilityId { get; set; }
    public long AnalyticsMetricId { get; set; }
    public string AlertName { get; set; }
    public string AlertType { get; set; }
    public string AlertCategory { get; set; }
    public string Status { get; set; }
    public DateTime AlertGeneratedDate { get; set; }
    public DateTime? AlertDueDate { get; set; }
    public string Description { get; set; }
    public decimal? CurrentValue { get; set; }
    public decimal? ThresholdValue { get; set; }
    public int PredictionConfidence { get; set; }
    public List<string> ImpactedAreas { get; set; } = new();
    public string RiskLevel { get; set; }
    public int RiskScore { get; set; }
    public List<string> RecommendedActions { get; set; } = new();
    public Guid? AssignedUserId { get; set; }
    public string ActionStatus { get; set; }
    public bool RequiresImmediateAction { get; set; }
    public bool AcknowledgedByUser { get; set; }
    public DateTime? AcknowledgedDate { get; set; }
    public int EscalationLevel { get; set; }
    public string HistoricalFrequency { get; set; }
}

public class PredictiveAlertCreateDTO
{
    public long AnalyticsMetricId { get; set; }
    public string AlertName { get; set; }
    public string AlertType { get; set; }
    public string AlertCategory { get; set; }
    public string Description { get; set; }
    public List<string> RecommendedActions { get; set; } = new();
    public string RiskLevel { get; set; }
    public List<string> NotificationRecipients { get; set; } = new();
}

// Custom Dashboard DTOs
public class CustomDashboardDTO
{
    public long Id { get; set; }
    public string FacilityId { get; set; }
    public string DashboardName { get; set; }
    public string DashboardCode { get; set; }
    public string DashboardType { get; set; }
    public string Description { get; set; }
    public string Layout { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPublic { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public DateTime? LastViewedDate { get; set; }
    public int ViewCount { get; set; }
    public List<DashboardWidgetDTO> Widgets { get; set; } = new();
    public List<DashboardFilterDTO> Filters { get; set; } = new();
    public List<DashboardThresholdDTO> Thresholds { get; set; } = new();
    public int RefreshIntervalSeconds { get; set; }
    public bool EnableExport { get; set; }
    public bool EnablePrint { get; set; }
}

public class CustomDashboardCreateDTO
{
    public string DashboardName { get; set; }
    public string DashboardCode { get; set; }
    public string DashboardType { get; set; }
    public string Description { get; set; }
    public string Layout { get; set; }
    public List<DashboardWidgetDTO> Widgets { get; set; } = new();
    public int RefreshIntervalSeconds { get; set; }
    public bool EnableExport { get; set; }
    public bool EnablePrint { get; set; }
}

public class DashboardWidgetDTO
{
    public Guid Id { get; set; }
    public string WidgetName { get; set; }
    public string WidgetType { get; set; }
    public string ChartType { get; set; }
    public int ColumnPosition { get; set; }
    public int RowPosition { get; set; }
    public int WidthUnits { get; set; }
    public int HeightUnits { get; set; }
    public List<string> MetricCodes { get; set; } = new();
    public string TimeRange { get; set; }
    public string Aggregation { get; set; }
    public bool ShowLegend { get; set; }
    public bool ShowDataLabels { get; set; }
    public bool EnableInteractivity { get; set; }
    public bool EnableExport { get; set; }
}

public class DashboardFilterDTO
{
    public Guid Id { get; set; }
    public string FilterName { get; set; }
    public string FilterType { get; set; }
    public string FilterField { get; set; }
    public List<string> AvailableValues { get; set; } = new();
    public string DefaultValue { get; set; }
    public bool IsRequired { get; set; }
}

public class DashboardThresholdDTO
{
    public Guid Id { get; set; }
    public string MetricCode { get; set; }
    public decimal? WarningThreshold { get; set; }
    public decimal? CriticalThreshold { get; set; }
    public string Color { get; set; }
    public string AlertMessage { get; set; }
}

public class CustomDashboardAnalyticsDTO
{
    public long DashboardId { get; set; }
    public int ViewCount { get; set; }
    public DateTime? LastViewedDate { get; set; }
    public int WeeklyActiveUsers { get; set; }
    public int AverageLoadTimeMs { get; set; }
    public string MostViewedWidget { get; set; }
    public int Popularity { get; set; }
}
