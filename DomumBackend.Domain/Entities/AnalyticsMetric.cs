namespace DomumBackend.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class AnalyticsMetric : BaseEntity
{
    public string? FacilityId { get; set; }
    public string? MetricName { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual Facility? Facility { get; set; }
    public string? MetricCode { get; set; }
    public decimal MetricValue { get; set; }
    public decimal? PreviousValue { get; set; }
    public decimal? ThresholdMin { get; set; }
    public decimal? ThresholdMax { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; } // Performance, Health, Compliance, Financial, Operational
    public string? SubCategory { get; set; }
    public DateTime MeasurementDate { get; set; }
    public DateTime? NextMeasurementDate { get; set; }
    public string? Status { get; set; } // Normal, Warning, Critical, Excellent
    public decimal? PercentageChange { get; set; }
    public decimal? TrendDirection { get; set; } // -1: Down, 0: Stable, 1: Up
    public int TrendStrength { get; set; } // 1-5 scale
    public string? Interpretation { get; set; }
    public string? Recommendation { get; set; }
    public bool IsAnomalous { get; set; }
    public string? AnomalyType { get; set; } // Outlier, Spike, Drop, Deviation
    public decimal? AnomalyScore { get; set; } // 0-100
    public DateTime? LastUpdatedDate { get; set; }
    public string? DataSourceType { get; set; } // Calculated, Manual, Imported, MLGenerated
    public string? Formula { get; set; } // For calculated metrics
    [NotMapped]
    public List<Guid> DependentMetricIds { get; set; } = new();
    [NotMapped]
    public Dictionary<string, object> MetricContext { get; set; } = new(); // Contextual data as JSON
    public bool IsKeyPerformanceIndicator { get; set; }
    public int AlertCount { get; set; }
    public DateTime? LastAlertDate { get; set; }
    public string? AlertStatus { get; set; } // None, Pending, Active, Resolved
    public int ComparisonScore { get; set; } // Percentile ranking (0-100)
    public string? BenchmarkComparison { get; set; } // Better, Worse, Average, NotAvailable
    public bool IsTracked { get; set; }
    public int ForecastConfidence { get; set; } // 0-100, ML forecast confidence
    public decimal? ForecastedValue { get; set; }
    public int ForecastDaysAhead { get; set; }
    public string? ForecastModel { get; set; } // LinearRegression, ExponentialSmoothing, ARIMA, Prophet
    [NotMapped]
    public List<decimal> HistoricalValues { get; set; } = new(); // Last 12 periods for trend
    public bool IncludeInDashboard { get; set; }
    public int DisplayPriority { get; set; }
    public string? DataQuality { get; set; } // Excellent, Good, Fair, Poor
    [NotMapped]
    public Dictionary<string, string> Metadata { get; set; } = new();
}
