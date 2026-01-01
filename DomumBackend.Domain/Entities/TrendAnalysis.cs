namespace DomumBackend.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class TrendAnalysis : BaseEntity
{
    public string FacilityId { get; set; }
    public long AnalyticsMetricId { get; set; }
    public string MetricName { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual Facility Facility { get; set; }
    public virtual AnalyticsMetric AnalyticsMetric { get; set; }
    public string TrendType { get; set; } // Upward, Downward, Stable, Seasonal, Cyclical, NoTrend
    public string TrendStrength { get; set; } // Weak, Moderate, Strong, VeryStrong
    public decimal? TrendSlope { get; set; } // Rate of change
    public decimal? RSquaredValue { get; set; } // R² for trend fit (0-1)
    public DateTime AnalysisStartDate { get; set; }
    public DateTime AnalysisEndDate { get; set; }
    public int DataPointsAnalyzed { get; set; }
    public decimal? AverageValue { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public decimal? StandardDeviation { get; set; }
    public decimal? VarianceExplained { get; set; } // Percentage
    public string Season { get; set; } // Winter, Spring, Summer, Fall, Quarter1-4, Month1-12
    public bool HasSeasonality { get; set; }
    public decimal? SeasonalityStrength { get; set; }
    public int SeasonalityPeriodDays { get; set; }
    [NotMapped]
    public List<decimal> SeasonalIndices { get; set; } = new(); // Seasonal adjustment factors
    public bool HasAnomalies { get; set; }
    public int AnomalyCount { get; set; }
    [NotMapped]
    public List<DateTime> AnomalyDates { get; set; } = new();
    [NotMapped]
    public List<decimal> AnomalyValues { get; set; } = new();
    public string AnomalyDescription { get; set; }
    public decimal? AutocorrelationCoefficient { get; set; }
    public string Stationarity { get; set; } // Stationary, NonStationary, Detrended
    public DateTime? TrendChangeDate { get; set; }
    public string TrendChangeDescription { get; set; }
    public int ConsecutiveIncreases { get; set; }
    public int ConsecutiveDecreases { get; set; }
    public decimal? VolatilityIndex { get; set; }
    public string VolatilityLevel { get; set; } // Low, Medium, High, VeryHigh
    [NotMapped]
    public List<decimal> MovingAverages { get; set; } = new(); // 7-day, 30-day, 90-day
    public string PredictedNextTrend { get; set; } // Expected trend direction
    public int ConfidenceLevel { get; set; } // 0-100
    public DateTime CreatedAnalysisDate { get; set; }
    public DateTime? UpdatedAnalysisDate { get; set; }
    public bool IsValidForForecasting { get; set; }
    public string AnalysisMethod { get; set; } // LinearRegression, ExponentialSmoothing, ARIMA, MovingAverage, Prophet
    [NotMapped]
    public Dictionary<string, object> MethodParameters { get; set; } = new();
    public string Interpretation { get; set; }
    public string BusinessImplication { get; set; }
    [NotMapped]
    public List<string> RecommendedActions { get; set; } = new();
    [NotMapped]
    public Dictionary<string, string> Metadata { get; set; } = new();
}
