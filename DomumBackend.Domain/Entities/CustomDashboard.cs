namespace DomumBackend.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class CustomDashboard : BaseEntity
{
    public string? FacilityId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string? DashboardName { get; set; }
    
    // Navigation properties
    public virtual Facility? Facility { get; set; }
    public string? DashboardCode { get; set; }
    public string? Description { get; set; }
    public string? DashboardType { get; set; } // Executive, Operational, Clinical, Financial, Compliance, Custom
    public string? Purpose { get; set; }
    public string? Layout { get; set; } // SingleColumn, TwoColumn, ThreeColumn, Custom
    public int ColumnCount { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPublic { get; set; }
    public bool IsShared { get; set; }
    [NotMapped]
    public List<Guid> SharedWithUserIds { get; set; } = new();
    [NotMapped]
    public List<string> SharedWithRoles { get; set; } = new();
    public new DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public Guid? LastModifiedByUserId { get; set; }
    public DateTime? LastViewedDate { get; set; }
    public int ViewCount { get; set; }
    public bool IsActive { get; set; }
    public bool IsTemplate { get; set; }
    public int RefreshIntervalSeconds { get; set; } // 0: Manual, 30, 60, 300, 900 for auto-refresh
    public bool EnableExport { get; set; }
    public bool EnablePrint { get; set; }
    public bool EnableScheduledReport { get; set; }
    public string? ScheduledReportFrequency { get; set; } // Daily, Weekly, Monthly, Custom
    public string? ScheduledReportRecipients { get; set; } // Comma-separated emails
    public bool EnableAlertNotifications { get; set; }
    [NotMapped]
    public List<string> AlertNotificationFilters { get; set; } = new();
    public DateTime? ScheduledReportLastSent { get; set; }
    public DateTime? ScheduledReportNextScheduled { get; set; }
    
    // Widget Configuration
    [NotMapped]
    public List<DashboardWidget> Widgets { get; set; } = new();
    
    // Filter and Grouping
    [NotMapped]
    public List<DashboardFilter> Filters { get; set; } = new();
    public string? DefaultGroupBy { get; set; } // Facility, Department, YearMonth, Category
    public string? DefaultSortBy { get; set; }
    public bool EnableDrillDown { get; set; }
    
    // Comparison Settings
    public bool EnableComparison { get; set; }
    public string? ComparisonType { get; set; } // YearOverYear, MonthOverMonth, PeriodToPeriod, Benchmark
    public string? ComparisonPeriodStart { get; set; }
    public string? ComparisonPeriodEnd { get; set; }
    
    // Thresholds and Alerts
    [NotMapped]
    public List<DashboardThreshold> Thresholds { get; set; } = new();
    public bool HighlightAnomalies { get; set; }
    public bool ShowForecast { get; set; }
    public int ForecastDaysAhead { get; set; }
    
    // Performance and Cache
    public bool EnableCaching { get; set; }
    public int CacheExpirationMinutes { get; set; }
    public DateTime? LastCachedDate { get; set; }
    public long ApproximateSizeBytes { get; set; }
    
    // Customization
    public string? ColorScheme { get; set; } // Light, Dark, Custom
    [NotMapped]
    public Dictionary<string, string> CustomColors { get; set; } = new();
    public string? FontFamily { get; set; }
    public int FontSize { get; set; }
    public bool ShowHeader { get; set; }
    public bool ShowFooter { get; set; }
    public string? CustomCSS { get; set; }
    
    // Analytics
    public int AverageLoadTimeMs { get; set; }
    public int MaxLoadTimeMs { get; set; }
    public int UserCount { get; set; } // Total users with access
    public int WeeklyActiveUsers { get; set; }
    public string? MostViewedWidget { get; set; }
    [NotMapped]
    public List<string> UserFeedback { get; set; } = new();
    
    // Metadata
    [NotMapped]
    public List<string> Tags { get; set; } = new();
    public string? Category { get; set; }
    public int Popularity { get; set; } // 0-100 ranking
    public string? Status { get; set; } // Draft, Active, Archived, Deprecated
    [NotMapped]
    public Dictionary<string, string> Metadata { get; set; } = new();
}

public class DashboardWidget
{
    public Guid Id { get; set; }
    public string? WidgetName { get; set; }
    public string? WidgetType { get; set; } // KPI, Chart, Table, Gauge, Heatmap, Scorecard, Map, Custom
    public string? ChartType { get; set; } // LineChart, BarChart, PieChart, AreaChart, ScatterPlot, Histogram
    public int ColumnPosition { get; set; }
    public int RowPosition { get; set; }
    public int WidthUnits { get; set; }
    public int HeightUnits { get; set; }
    public List<string> MetricCodes { get; set; } = new();
    public string? DataSource { get; set; } // MetricsTable, Query, Report, External
    public string? CustomQuery { get; set; }
    public bool ShowLegend { get; set; }
    public bool ShowGridLines { get; set; }
    public bool ShowDataLabels { get; set; }
    public string? TimeRange { get; set; } // Last7Days, Last30Days, LastQuarter, Custom
    public string? Aggregation { get; set; } // Sum, Average, Max, Min, Count
    public bool EnableInteractivity { get; set; }
    public string? DrillDownTarget { get; set; }
    public bool EnableExport { get; set; }
}

public class DashboardFilter
{
    public Guid Id { get; set; }
    public string? FilterName { get; set; }
    public string? FilterType { get; set; } // Dropdown, DateRange, TextSearch, MultiSelect, Slider
    public string? FilterField { get; set; } // Column or property to filter
    public List<string> AvailableValues { get; set; } = new();
    public string? DefaultValue { get; set; }
    public bool IsRequired { get; set; }
    public bool IsMutable { get; set; } // User can change
}

public class DashboardThreshold
{
    public Guid Id { get; set; }
    public string? MetricCode { get; set; }
    public decimal? WarningThreshold { get; set; }
    public decimal? CriticalThreshold { get; set; }
    public string? Color { get; set; } // For visual highlighting
    public string? AlertMessage { get; set; }
}
