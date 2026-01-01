namespace DomumBackend.Application.Common.Interfaces;

using DomumBackend.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAnalyticsMetricService
{
    Task<AnalyticsMetricDTO> CreateMetricAsync(string facilityId, AnalyticsMetricDTO dto);
    Task<AnalyticsMetricDTO> GetMetricByIdAsync(Guid metricId);
    Task<AnalyticsMetricDTO> GetMetricByCodeAsync(string facilityId, string metricCode);
    Task<List<AnalyticsMetricDTO>> GetMetricsByCategoryAsync(string facilityId, string category);
    Task<List<AnalyticsMetricDTO>> GetMetricsBySubCategoryAsync(string facilityId, string category, string subCategory);
    Task<List<AnalyticsMetricDTO>> GetAllMetricsAsync(string facilityId);
    Task<List<AnalyticsMetricDTO>> GetKeyPerformanceIndicatorsAsync(string facilityId);
    Task<AnalyticsMetricDTO> UpdateMetricAsync(Guid metricId, AnalyticsMetricDTO dto);
    Task DeleteMetricAsync(Guid metricId);
    Task<List<AnalyticsMetricDTO>> GetAnomalousMetricsAsync(string facilityId);
    Task<List<AnalyticsMetricDTO>> GetCriticalMetricsAsync(string facilityId);
    Task<List<AnalyticsMetricDTO>> GetMetricsWithAlertsAsync(string facilityId);
    Task<Dictionary<string, decimal>> CalculateAggregateMetricsAsync(string facilityId, List<string> metricCodes);
    Task<List<AnalyticsMetricDTO>> CompareMetricsAsync(string facilityId1, string facilityId2, string category);
    Task<AnalyticsMetricDTO> ForecastMetricAsync(Guid metricId, int daysAhead);
    Task<int> GetBenchmarkPercentileAsync(Guid metricId);
    Task RecalculateAllMetricsAsync(string facilityId);
}

public interface ITrendAnalysisService
{
    Task<TrendAnalysisDTO> CreateTrendAnalysisAsync(string facilityId, long metricId, int daysToAnalyze);
    Task<TrendAnalysisDTO> GetTrendAnalysisAsync(Guid trendId);
    Task<TrendAnalysisDTO> GetLatestTrendAsync(long metricId);
    Task<List<TrendAnalysisDTO>> GetTrendsByTypeAsync(string facilityId, string trendType);
    Task<List<TrendAnalysisDTO>> GetSeasonalTrendsAsync(string facilityId);
    Task<List<TrendAnalysisDTO>> GetAnomalouseTrendsAsync(string facilityId);
    Task<string> PredictNextTrendAsync(Guid trendId);
    Task<decimal?> ForecastValueAsync(Guid trendId, int daysAhead);
    Task<Dictionary<string, object>> GetSeasonalityDetailsAsync(Guid trendId);
    Task<List<TrendAnalysisDTO>> DetectSeasonalityAsync(long metricId);
    Task<List<TrendAnalysisDTO>> AnalyzeMultipleMetricsAsync(string facilityId, List<long> metricIds);
    Task<string> GetTrendInterpretationAsync(Guid trendId);
    Task<List<string>> GetRecommendedActionsAsync(Guid trendId);
    Task<decimal?> GetVolatilityAsync(Guid trendId);
    Task<bool> IsValidForForecastingAsync(Guid trendId);
}

public interface IPredictiveAlertService
{
    Task<PredictiveAlertDTO> CreateAlertAsync(string facilityId, PredictiveAlertDTO dto);
    Task<PredictiveAlertDTO> GetAlertByIdAsync(Guid alertId);
    Task<List<PredictiveAlertDTO>> GetActiveAlertsAsync(string facilityId);
    Task<List<PredictiveAlertDTO>> GetAlertsByStatusAsync(string facilityId, string status);
    Task<List<PredictiveAlertDTO>> GetAlertsByTypeAsync(string facilityId, string alertType);
    Task<List<PredictiveAlertDTO>> GetCriticalAlertsAsync(string facilityId);
    Task<List<PredictiveAlertDTO>> GetPendingActionsAsync(string facilityId);
    Task<List<PredictiveAlertDTO>> GetOverdueAlertsAsync(string facilityId);
    Task<List<PredictiveAlertDTO>> GetRecurringAlertsAsync(string facilityId);
    Task<PredictiveAlertDTO> UpdateAlertAsync(Guid alertId, PredictiveAlertDTO dto);
    Task<PredictiveAlertDTO> AcknowledgeAlertAsync(Guid alertId, Guid userId, string notes = null);
    Task<PredictiveAlertDTO> ResolveAlertAsync(Guid alertId, Guid userId, string notes = null);
    Task<PredictiveAlertDTO> AssignAlertAsync(Guid alertId, Guid userId, string notes = null);
    Task<PredictiveAlertDTO> EscalateAlertAsync(Guid alertId, Guid escalatedToUserId, string reason);
    Task DeleteAlertAsync(Guid alertId);
    Task<int> GetAlertCountByTypeAsync(string facilityId, string alertType);
    Task<int> GetAlertCountByStatusAsync(string facilityId, string status);
    Task<Dictionary<string, int>> GetAlertSummaryAsync(string facilityId);
    Task NotifyAlertAsync(Guid alertId, List<string> recipients);
    Task<List<PredictiveAlertDTO>> GetAlertsForUserAsync(Guid userId);
}

public interface ICustomDashboardService
{
    Task<CustomDashboardDTO> CreateDashboardAsync(string facilityId, Guid userId, CustomDashboardDTO dto);
    Task<CustomDashboardDTO> GetDashboardByIdAsync(Guid dashboardId);
    Task<CustomDashboardDTO> GetDashboardByCodeAsync(string facilityId, string dashboardCode);
    Task<List<CustomDashboardDTO>> GetDashboardsByFacilityAsync(string facilityId);
    Task<List<CustomDashboardDTO>> GetDashboardsByTypeAsync(string facilityId, string dashboardType);
    Task<List<CustomDashboardDTO>> GetDefaultDashboardsAsync(string facilityId);
    Task<List<CustomDashboardDTO>> GetSharedDashboardsAsync(Guid userId);
    Task<List<CustomDashboardDTO>> GetUserDashboardsAsync(Guid userId);
    Task<CustomDashboardDTO> UpdateDashboardAsync(Guid dashboardId, CustomDashboardDTO dto);
    Task<CustomDashboardDTO> AddWidgetAsync(Guid dashboardId, DashboardWidgetDTO widget);
    Task RemoveWidgetAsync(Guid dashboardId, Guid widgetId);
    Task<CustomDashboardDTO> UpdateWidgetAsync(Guid dashboardId, Guid widgetId, DashboardWidgetDTO widget);
    Task<CustomDashboardDTO> AddFilterAsync(Guid dashboardId, DashboardFilterDTO filter);
    Task<CustomDashboardDTO> RemoveFilterAsync(Guid dashboardId, Guid filterId);
    Task<CustomDashboardDTO> ShareDashboardAsync(Guid dashboardId, List<Guid> userIds, List<string> roles = null);
    Task<CustomDashboardDTO> UnshareWithUserAsync(Guid dashboardId, Guid userId);
    Task<CustomDashboardDTO> MakeDashboardDefaultAsync(Guid dashboardId);
    Task DeleteDashboardAsync(Guid dashboardId);
    Task<CustomDashboardDTO> CloneDashboardAsync(Guid dashboardId, string facilityId, string newName);
    Task<List<CustomDashboardDTO>> GetDashboardTemplatesAsync();
    Task<byte[]> ExportDashboardAsync(Guid dashboardId, string format = "pdf"); // pdf, excel, json
    Task RecordDashboardViewAsync(Guid dashboardId, Guid userId);
    Task<CustomDashboardAnalyticsDTO> GetDashboardAnalyticsAsync(long dashboardId);
}
