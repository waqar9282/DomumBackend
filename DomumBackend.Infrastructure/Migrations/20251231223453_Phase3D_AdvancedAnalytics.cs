using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase3D_AdvancedAnalytics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyticsMetrics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MetricName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MetricCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MetricValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ThresholdMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ThresholdMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMeasurementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PercentageChange = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrendDirection = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrendStrength = table.Column<int>(type: "int", nullable: false),
                    Interpretation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAnomalous = table.Column<bool>(type: "bit", nullable: false),
                    AnomalyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnomalyScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataSourceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsKeyPerformanceIndicator = table.Column<bool>(type: "bit", nullable: false),
                    AlertCount = table.Column<int>(type: "int", nullable: false),
                    LastAlertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlertStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComparisonScore = table.Column<int>(type: "int", nullable: false),
                    BenchmarkComparison = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTracked = table.Column<bool>(type: "bit", nullable: false),
                    ForecastConfidence = table.Column<int>(type: "int", nullable: false),
                    ForecastedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ForecastDaysAhead = table.Column<int>(type: "int", nullable: false),
                    ForecastModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncludeInDashboard = table.Column<bool>(type: "bit", nullable: false),
                    DisplayPriority = table.Column<int>(type: "int", nullable: false),
                    DataQuality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticsMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyticsMetrics_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomDashboards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DashboardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DashboardCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DashboardType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColumnCount = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsShared = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastViewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    RefreshIntervalSeconds = table.Column<int>(type: "int", nullable: false),
                    EnableExport = table.Column<bool>(type: "bit", nullable: false),
                    EnablePrint = table.Column<bool>(type: "bit", nullable: false),
                    EnableScheduledReport = table.Column<bool>(type: "bit", nullable: false),
                    ScheduledReportFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledReportRecipients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableAlertNotifications = table.Column<bool>(type: "bit", nullable: false),
                    ScheduledReportLastSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduledReportNextScheduled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefaultGroupBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultSortBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableDrillDown = table.Column<bool>(type: "bit", nullable: false),
                    EnableComparison = table.Column<bool>(type: "bit", nullable: false),
                    ComparisonType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComparisonPeriodStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComparisonPeriodEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighlightAnomalies = table.Column<bool>(type: "bit", nullable: false),
                    ShowForecast = table.Column<bool>(type: "bit", nullable: false),
                    ForecastDaysAhead = table.Column<int>(type: "int", nullable: false),
                    EnableCaching = table.Column<bool>(type: "bit", nullable: false),
                    CacheExpirationMinutes = table.Column<int>(type: "int", nullable: false),
                    LastCachedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproximateSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    ColorScheme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FontFamily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FontSize = table.Column<int>(type: "int", nullable: false),
                    ShowHeader = table.Column<bool>(type: "bit", nullable: false),
                    ShowFooter = table.Column<bool>(type: "bit", nullable: false),
                    CustomCSS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageLoadTimeMs = table.Column<int>(type: "int", nullable: false),
                    MaxLoadTimeMs = table.Column<int>(type: "int", nullable: false),
                    UserCount = table.Column<int>(type: "int", nullable: false),
                    WeeklyActiveUsers = table.Column<int>(type: "int", nullable: false),
                    MostViewedWidget = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomDashboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomDashboards_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PredictiveAlerts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnalyticsMetricId = table.Column<long>(type: "bigint", nullable: false),
                    AlertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AlertType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlertCategory = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlertGeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlertDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlertResolvedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailedDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ThresholdValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VarianceFromExpected = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaysUntilCritical = table.Column<int>(type: "int", nullable: true),
                    DaysSinceAlertStart = table.Column<int>(type: "int", nullable: true),
                    PredictionConfidence = table.Column<int>(type: "int", nullable: false),
                    PredictionModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredictedEventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImpactedAreas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpactedResourceIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedResourcesAtRisk = table.Column<int>(type: "int", nullable: true),
                    RiskLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskScore = table.Column<int>(type: "int", nullable: false),
                    UrgencyScore = table.Column<int>(type: "int", nullable: false),
                    PrimaryRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiresImmediateAction = table.Column<bool>(type: "bit", nullable: false),
                    ActionAssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActionDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNotificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotificationCount = table.Column<int>(type: "int", nullable: false),
                    AcknowledgedByUser = table.Column<bool>(type: "bit", nullable: false),
                    AcknowledgedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcknowledgedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcknowledgmentNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    RecurrenceCount = table.Column<int>(type: "int", nullable: false),
                    LastRecurrenceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HistoricalFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManuallyCreated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EscalationLevel = table.Column<int>(type: "int", nullable: false),
                    EscalatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EscalatedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EscalationReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictiveAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredictiveAlerts_AnalyticsMetrics_AnalyticsMetricId",
                        column: x => x.AnalyticsMetricId,
                        principalTable: "AnalyticsMetrics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PredictiveAlerts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrendAnalyses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnalyticsMetricId = table.Column<long>(type: "bigint", nullable: false),
                    MetricName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TrendType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrendStrength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrendSlope = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RSquaredValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AnalysisStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnalysisEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPointsAnalyzed = table.Column<int>(type: "int", nullable: false),
                    AverageValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StandardDeviation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VarianceExplained = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasSeasonality = table.Column<bool>(type: "bit", nullable: false),
                    SeasonalityStrength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SeasonalityPeriodDays = table.Column<int>(type: "int", nullable: false),
                    HasAnomalies = table.Column<bool>(type: "bit", nullable: false),
                    AnomalyCount = table.Column<int>(type: "int", nullable: false),
                    AnomalyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutocorrelationCoefficient = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stationarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrendChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrendChangeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsecutiveIncreases = table.Column<int>(type: "int", nullable: false),
                    ConsecutiveDecreases = table.Column<int>(type: "int", nullable: false),
                    VolatilityIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VolatilityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredictedNextTrend = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfidenceLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAnalysisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAnalysisDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsValidForForecasting = table.Column<bool>(type: "bit", nullable: false),
                    AnalysisMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interpretation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessImplication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrendAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrendAnalyses_AnalyticsMetrics_AnalyticsMetricId",
                        column: x => x.AnalyticsMetricId,
                        principalTable: "AnalyticsMetrics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrendAnalyses_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsMetrics_Category",
                table: "AnalyticsMetrics",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsMetrics_FacilityId",
                table: "AnalyticsMetrics",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsMetrics_MeasurementDate",
                table: "AnalyticsMetrics",
                column: "MeasurementDate");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsMetrics_MetricCode",
                table: "AnalyticsMetrics",
                column: "MetricCode");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsMetrics_Status",
                table: "AnalyticsMetrics",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CustomDashboards_CreatedByUserId",
                table: "CustomDashboards",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomDashboards_CreatedDate",
                table: "CustomDashboards",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CustomDashboards_DashboardType",
                table: "CustomDashboards",
                column: "DashboardType");

            migrationBuilder.CreateIndex(
                name: "IX_CustomDashboards_FacilityId",
                table: "CustomDashboards",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomDashboards_IsDefault",
                table: "CustomDashboards",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_PredictiveAlerts_AlertCategory",
                table: "PredictiveAlerts",
                column: "AlertCategory");

            migrationBuilder.CreateIndex(
                name: "IX_PredictiveAlerts_AlertGeneratedDate",
                table: "PredictiveAlerts",
                column: "AlertGeneratedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PredictiveAlerts_AlertType",
                table: "PredictiveAlerts",
                column: "AlertType");

            migrationBuilder.CreateIndex(
                name: "IX_PredictiveAlerts_AnalyticsMetricId",
                table: "PredictiveAlerts",
                column: "AnalyticsMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictiveAlerts_FacilityId",
                table: "PredictiveAlerts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictiveAlerts_Status",
                table: "PredictiveAlerts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TrendAnalyses_AnalyticsMetricId",
                table: "TrendAnalyses",
                column: "AnalyticsMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_TrendAnalyses_CreatedAnalysisDate",
                table: "TrendAnalyses",
                column: "CreatedAnalysisDate");

            migrationBuilder.CreateIndex(
                name: "IX_TrendAnalyses_FacilityId",
                table: "TrendAnalyses",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_TrendAnalyses_TrendType",
                table: "TrendAnalyses",
                column: "TrendType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomDashboards");

            migrationBuilder.DropTable(
                name: "PredictiveAlerts");

            migrationBuilder.DropTable(
                name: "TrendAnalyses");

            migrationBuilder.DropTable(
                name: "AnalyticsMetrics");
        }
    }
}
