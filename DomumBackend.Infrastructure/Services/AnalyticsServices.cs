namespace DomumBackend.Infrastructure.Services;

using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AnalyticsMetricService : IAnalyticsMetricService
{
    private readonly ApplicationDbContext _context;

    public AnalyticsMetricService(ApplicationDbContext context) => _context = context;

    public async Task<AnalyticsMetricDTO> CreateMetricAsync(string facilityId, AnalyticsMetricDTO dto)
    {
        var metric = new AnalyticsMetric { FacilityId = facilityId, MetricName = dto.MetricName, MetricCode = dto.MetricCode, MetricValue = dto.MetricValue, Category = dto.Category, SubCategory = dto.SubCategory, Unit = dto.Unit, MeasurementDate = DateTime.UtcNow, Status = "Normal", CreatedDate = DateTime.UtcNow };
        _context.AnalyticsMetrics.Add(metric);
        await _context.SaveChangesAsync();
        return MapToDTO(metric);
    }

    public async Task<AnalyticsMetricDTO?> GetMetricByIdAsync(Guid metricId)
    {
        var metric = await _context.AnalyticsMetrics.FindAsync(metricId);
        return metric == null ? null : MapToDTO(metric);
    }

    public async Task<AnalyticsMetricDTO?> GetMetricByCodeAsync(string facilityId, string metricCode)
    {
        var metric = await _context.AnalyticsMetrics.FirstOrDefaultAsync(m => m.FacilityId == facilityId && m.MetricCode == metricCode);
        return metric == null ? null : MapToDTO(metric);
    }

    public async Task<List<AnalyticsMetricDTO>> GetMetricsByCategoryAsync(string facilityId, string category)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.Category == category).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<List<AnalyticsMetricDTO>> GetMetricsBySubCategoryAsync(string facilityId, string category, string subCategory)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.Category == category && m.SubCategory == subCategory).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<List<AnalyticsMetricDTO>> GetAllMetricsAsync(string facilityId)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.IsActive).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<List<AnalyticsMetricDTO>> GetKeyPerformanceIndicatorsAsync(string facilityId)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.IsKeyPerformanceIndicator && m.IsActive).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<AnalyticsMetricDTO> UpdateMetricAsync(Guid metricId, AnalyticsMetricDTO dto)
    {
        var metric = await _context.AnalyticsMetrics.FindAsync(metricId);
        if (metric == null) throw new InvalidOperationException($"Metric {metricId} not found");
        metric.MetricValue = dto.MetricValue;
        metric.Status = dto.Status;
        metric.LastUpdatedDate = DateTime.UtcNow;
        _context.AnalyticsMetrics.Update(metric);
        await _context.SaveChangesAsync();
        return MapToDTO(metric);
    }

    public async Task DeleteMetricAsync(Guid metricId)
    {
        var metric = await _context.AnalyticsMetrics.FindAsync(metricId);
        if (metric != null) { _context.AnalyticsMetrics.Remove(metric); await _context.SaveChangesAsync(); }
    }

    public async Task<List<AnalyticsMetricDTO>> GetAnomalousMetricsAsync(string facilityId)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.IsAnomalous && m.IsActive).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<List<AnalyticsMetricDTO>> GetCriticalMetricsAsync(string facilityId)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.Status == "Critical").ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<List<AnalyticsMetricDTO>> GetMetricsWithAlertsAsync(string facilityId)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && m.AlertCount > 0).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<Dictionary<string, decimal>> CalculateAggregateMetricsAsync(string facilityId, List<string> metricCodes)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId && metricCodes.Contains(m.MetricCode!)).ToListAsync();
        return metrics.GroupBy(m => m.Category ?? "Unknown").ToDictionary(g => g.Key, g => g.Average(m => m.MetricValue));
    }

    public async Task<List<AnalyticsMetricDTO>> CompareMetricsAsync(string facilityId1, string facilityId2, string category)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => (m.FacilityId == facilityId1 || m.FacilityId == facilityId2) && m.Category == category).ToListAsync();
        return metrics.Select(MapToDTO).ToList();
    }

    public async Task<AnalyticsMetricDTO> ForecastMetricAsync(Guid metricId, int daysAhead)
    {
        var metric = await _context.AnalyticsMetrics.FindAsync(metricId);
        if (metric == null) throw new InvalidOperationException($"Metric {metricId} not found");
        metric.ForecastedValue = metric.MetricValue * 1.05m;
        metric.ForecastModel = "Linear";
        return MapToDTO(metric);
    }

    public async Task<int> GetBenchmarkPercentileAsync(Guid metricId)
    {
        var metric = await _context.AnalyticsMetrics.FindAsync(metricId);
        if (metric == null) throw new InvalidOperationException($"Metric {metricId} not found");
        var allMetrics = await _context.AnalyticsMetrics.ToListAsync();
        var percentile = (allMetrics.Count(m => m.MetricValue <= metric.MetricValue) * 100) / allMetrics.Count;
        return percentile;
    }

    public async Task RecalculateAllMetricsAsync(string facilityId)
    {
        var metrics = await _context.AnalyticsMetrics.Where(m => m.FacilityId == facilityId).ToListAsync();
        foreach (var metric in metrics) metric.LastUpdatedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    private AnalyticsMetricDTO MapToDTO(AnalyticsMetric metric) =>
        new AnalyticsMetricDTO { Id = metric.Id, FacilityId = metric.FacilityId, MetricName = metric.MetricName, MetricCode = metric.MetricCode, MetricValue = metric.MetricValue, PreviousValue = metric.PreviousValue, ThresholdMin = metric.ThresholdMin, ThresholdMax = metric.ThresholdMax, Unit = metric.Unit, Category = metric.Category, SubCategory = metric.SubCategory, Status = metric.Status, IsAnomalous = metric.IsAnomalous, AlertCount = metric.AlertCount, IsKeyPerformanceIndicator = metric.IsKeyPerformanceIndicator, ForecastedValue = metric.ForecastedValue, ForecastModel = metric.ForecastModel };
}

public class TrendAnalysisService : ITrendAnalysisService
{
    private readonly ApplicationDbContext _context;
    public TrendAnalysisService(ApplicationDbContext context) => _context = context;

    public async Task<TrendAnalysisDTO> CreateTrendAnalysisAsync(string facilityId, long metricId, int daysToAnalyze)
    {
        var metric = await _context.AnalyticsMetrics.FindAsync(metricId);
        if (metric == null) throw new InvalidOperationException($"Metric {metricId} not found");
        var trend = new TrendAnalysis { FacilityId = facilityId, AnalyticsMetricId = metricId, MetricName = metric.MetricName, AnalysisStartDate = DateTime.UtcNow.AddDays(-daysToAnalyze), AnalysisEndDate = DateTime.UtcNow, TrendType = "Stable", TrendStrength = "Moderate", DataPointsAnalyzed = daysToAnalyze, CreatedAnalysisDate = DateTime.UtcNow, AnalysisMethod = "LinearRegression", CreatedDate = DateTime.UtcNow, IsActive = true };
        _context.TrendAnalyses.Add(trend);
        await _context.SaveChangesAsync();
        return MapToDTO(trend);
    }

    public async Task<TrendAnalysisDTO?> GetTrendAnalysisAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend == null ? null : MapToDTO(trend);
    }

    public async Task<TrendAnalysisDTO?> GetLatestTrendAsync(long metricId)
    {
        var trend = await _context.TrendAnalyses.Where(t => t.AnalyticsMetricId == metricId).OrderByDescending(t => t.CreatedAnalysisDate).FirstOrDefaultAsync();
        return trend == null ? null : MapToDTO(trend);
    }

    public async Task<List<TrendAnalysisDTO>> GetTrendsByTypeAsync(string facilityId, string trendType)
    {
        var trends = await _context.TrendAnalyses.Where(t => t.FacilityId == facilityId && t.TrendType == trendType).ToListAsync();
        return trends.Select(MapToDTO).ToList();
    }

    public async Task<List<TrendAnalysisDTO>> GetSeasonalTrendsAsync(string facilityId)
    {
        var trends = await _context.TrendAnalyses.Where(t => t.FacilityId == facilityId && t.TrendType == "Seasonal").ToListAsync();
        return trends.Select(MapToDTO).ToList();
    }

    public async Task<List<TrendAnalysisDTO>> GetAnomalouseTrendsAsync(string facilityId)
    {
        var trends = await _context.TrendAnalyses.Where(t => t.FacilityId == facilityId && t.HasAnomalies).ToListAsync();
        return trends.Select(MapToDTO).ToList();
    }

    public async Task<string> PredictNextTrendAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend?.PredictedNextTrend ?? "Unknown";
    }

    public async Task<decimal?> ForecastValueAsync(Guid trendId, int daysAhead)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend == null ? null : (trend.TrendSlope ?? 0) * daysAhead;
    }

    public async Task<Dictionary<string, object>> GetSeasonalityDetailsAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        if (trend == null) return new Dictionary<string, object>();
        return new Dictionary<string, object> { { "HasSeasonality", trend.HasSeasonality }, { "SeasonalityPeriod", trend.SeasonalityPeriodDays } };
    }

    public async Task<List<TrendAnalysisDTO>> DetectSeasonalityAsync(long metricId)
    {
        var trends = await _context.TrendAnalyses.Where(t => t.AnalyticsMetricId == metricId).OrderByDescending(t => t.CreatedAnalysisDate).Take(12).ToListAsync();
        return trends.Select(MapToDTO).ToList();
    }

    public async Task<List<TrendAnalysisDTO>> AnalyzeMultipleMetricsAsync(string facilityId, List<long> metricIds)
    {
        var trends = await _context.TrendAnalyses.Where(t => t.FacilityId == facilityId && metricIds.Contains(t.AnalyticsMetricId)).ToListAsync();
        return trends.Select(MapToDTO).ToList();
    }

    public async Task<string> GetTrendInterpretationAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend?.Interpretation ?? "No interpretation available";
    }

    public async Task<List<string>> GetRecommendedActionsAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend?.RecommendedActions ?? new List<string>();
    }

    public async Task<decimal?> GetVolatilityAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend?.VolatilityIndex;
    }

    public async Task<bool> IsValidForForecastingAsync(Guid trendId)
    {
        var trend = await _context.TrendAnalyses.FindAsync(trendId);
        return trend?.IsValidForForecasting ?? false;
    }

    private TrendAnalysisDTO MapToDTO(TrendAnalysis trend) =>
        new TrendAnalysisDTO { Id = trend.Id, AnalyticsMetricId = trend.AnalyticsMetricId, MetricName = trend.MetricName, TrendType = trend.TrendType, TrendStrength = trend.TrendStrength, TrendSlope = trend.TrendSlope, RSquaredValue = trend.RSquaredValue, AnalysisStartDate = trend.AnalysisStartDate, AnalysisEndDate = trend.AnalysisEndDate, HasSeasonality = trend.HasSeasonality, HasAnomalies = trend.HasAnomalies, AnomalyCount = trend.AnomalyCount, VolatilityIndex = trend.VolatilityIndex, PredictedNextTrend = trend.PredictedNextTrend, ConfidenceLevel = trend.ConfidenceLevel, Interpretation = trend.Interpretation, RecommendedActions = trend.RecommendedActions };
}

public class PredictiveAlertService : IPredictiveAlertService
{
    private readonly ApplicationDbContext _context;
    public PredictiveAlertService(ApplicationDbContext context) => _context = context;

    public async Task<PredictiveAlertDTO> CreateAlertAsync(string facilityId, PredictiveAlertDTO dto)
    {
        var alert = new PredictiveAlert { FacilityId = facilityId, AnalyticsMetricId = dto.AnalyticsMetricId, AlertName = dto.AlertName, AlertType = dto.AlertType, AlertCategory = dto.AlertCategory, Status = "Active", AlertGeneratedDate = DateTime.UtcNow, Description = dto.Description, CreatedDate = DateTime.UtcNow, IsActive = true };
        _context.PredictiveAlerts.Add(alert);
        await _context.SaveChangesAsync();
        return MapToDTO(alert);
    }

    public async Task<PredictiveAlertDTO?> GetAlertByIdAsync(Guid alertId)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        return alert == null ? null : MapToDTO(alert)!;
    }

    public async Task<List<PredictiveAlertDTO>> GetActiveAlertsAsync(string facilityId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.Status == "Active").ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<List<PredictiveAlertDTO>> GetAlertsByStatusAsync(string facilityId, string status)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.Status == status).ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<List<PredictiveAlertDTO>> GetAlertsByTypeAsync(string facilityId, string alertType)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.AlertType == alertType).ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<List<PredictiveAlertDTO>> GetCriticalAlertsAsync(string facilityId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.AlertCategory == "Critical").ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<List<PredictiveAlertDTO>> GetPendingActionsAsync(string facilityId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.ActionStatus == "NotStarted").ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<List<PredictiveAlertDTO>> GetOverdueAlertsAsync(string facilityId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.ActionDueDate < DateTime.UtcNow && a.ActionStatus != "Completed").ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<List<PredictiveAlertDTO>> GetRecurringAlertsAsync(string facilityId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId && a.IsRecurring).ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    public async Task<PredictiveAlertDTO> UpdateAlertAsync(Guid alertId, PredictiveAlertDTO dto)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert == null) throw new InvalidOperationException($"Alert {alertId} not found");
        alert.Status = dto.Status;
        alert.Description = dto.Description;
        _context.PredictiveAlerts.Update(alert);
        await _context.SaveChangesAsync();
        return MapToDTO(alert);
    }

    public async Task<PredictiveAlertDTO> AcknowledgeAlertAsync(Guid alertId, Guid userId, string? notes = null)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert == null) throw new InvalidOperationException($"Alert {alertId} not found");
        alert.AcknowledgedByUserId = userId;
        alert.AcknowledgedDate = DateTime.UtcNow;
        alert.AcknowledgedByUser = true;
        alert.AcknowledgmentNotes = notes;
        _context.PredictiveAlerts.Update(alert);
        await _context.SaveChangesAsync();
        return MapToDTO(alert);
    }

    public async Task<PredictiveAlertDTO> ResolveAlertAsync(Guid alertId, Guid userId, string? notes = null)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert == null) throw new InvalidOperationException($"Alert {alertId} not found");
        alert.Status = "Resolved";
        alert.ActionStatus = "Completed";
        alert.ActionCompletedDate = DateTime.UtcNow;
        alert.ActionNotes = notes;
        _context.PredictiveAlerts.Update(alert);
        await _context.SaveChangesAsync();
        return MapToDTO(alert);
    }

    public async Task<PredictiveAlertDTO> AssignAlertAsync(Guid alertId, Guid userId, string? notes = null)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert == null) throw new InvalidOperationException($"Alert {alertId} not found");
        alert.AssignedUserId = userId;
        alert.ActionStatus = "InProgress";
        _context.PredictiveAlerts.Update(alert);
        await _context.SaveChangesAsync();
        return MapToDTO(alert);
    }

    public async Task<PredictiveAlertDTO> EscalateAlertAsync(Guid alertId, Guid escalatedToUserId, string reason)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert == null) throw new InvalidOperationException($"Alert {alertId} not found");
        alert.EscalationLevel += 1;
        alert.EscalatedDate = DateTime.UtcNow;
        alert.EscalatedToUserId = escalatedToUserId;
        alert.EscalationReason = reason;
        _context.PredictiveAlerts.Update(alert);
        await _context.SaveChangesAsync();
        return MapToDTO(alert);
    }

    public async Task DeleteAlertAsync(Guid alertId)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert != null) { _context.PredictiveAlerts.Remove(alert); await _context.SaveChangesAsync(); }
    }

    public async Task<int> GetAlertCountByTypeAsync(string facilityId, string alertType) =>
        await _context.PredictiveAlerts.CountAsync(a => a.FacilityId == facilityId && a.AlertType == alertType);

    public async Task<int> GetAlertCountByStatusAsync(string facilityId, string status) =>
        await _context.PredictiveAlerts.CountAsync(a => a.FacilityId == facilityId && a.Status == status);

    public async Task<Dictionary<string, int>> GetAlertSummaryAsync(string facilityId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.FacilityId == facilityId).ToListAsync();
        return new Dictionary<string, int> { { "Total", alerts.Count }, { "Active", alerts.Count(a => a.Status == "Active") }, { "Resolved", alerts.Count(a => a.Status == "Resolved") }, { "Critical", alerts.Count(a => a.AlertCategory == "Critical") } };
    }

    public async Task NotifyAlertAsync(Guid alertId, List<string> recipients)
    {
        var alert = await _context.PredictiveAlerts.FindAsync(alertId);
        if (alert != null) { alert.LastNotificationDate = DateTime.UtcNow; alert.NotificationCount += 1; _context.PredictiveAlerts.Update(alert); await _context.SaveChangesAsync(); }
    }

    public async Task<List<PredictiveAlertDTO>> GetAlertsForUserAsync(Guid userId)
    {
        var alerts = await _context.PredictiveAlerts.Where(a => a.AssignedUserId == userId || a.AcknowledgedByUserId == userId).ToListAsync();
        return alerts.Select(MapToDTO).ToList();
    }

    private PredictiveAlertDTO MapToDTO(PredictiveAlert alert) =>
        new PredictiveAlertDTO { Id = alert.Id, FacilityId = alert.FacilityId, AnalyticsMetricId = alert.AnalyticsMetricId, AlertName = alert.AlertName, AlertType = alert.AlertType, AlertCategory = alert.AlertCategory, Status = alert.Status, AlertGeneratedDate = alert.AlertGeneratedDate, AlertDueDate = alert.AlertDueDate, Description = alert.Description, EscalationLevel = alert.EscalationLevel, HistoricalFrequency = alert.HistoricalFrequency };
}

public class CustomDashboardService : ICustomDashboardService
{
    private readonly ApplicationDbContext _context;
    public CustomDashboardService(ApplicationDbContext context) => _context = context;

    public async Task<CustomDashboardDTO> CreateDashboardAsync(string facilityId, Guid userId, CustomDashboardDTO dto)
    {
        var dashboard = new CustomDashboard { FacilityId = facilityId, CreatedByUserId = userId, DashboardName = dto.DashboardName, DashboardCode = dto.DashboardCode, DashboardType = dto.DashboardType, Description = dto.Description, CreatedDate = DateTime.UtcNow, IsActive = true };
        _context.CustomDashboards.Add(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO?> GetDashboardByIdAsync(Guid dashboardId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        return dashboard == null ? null : MapToDTO(dashboard)!;
    }

    public async Task<CustomDashboardDTO?> GetDashboardByCodeAsync(string facilityId, string dashboardCode)
    {
        var dashboard = await _context.CustomDashboards.FirstOrDefaultAsync(d => d.FacilityId == facilityId && d.DashboardCode == dashboardCode);
        return dashboard == null ? null : MapToDTO(dashboard)!;
    }

    public async Task<List<CustomDashboardDTO>> GetDashboardsByFacilityAsync(string facilityId)
    {
        var dashboards = await _context.CustomDashboards.Where(d => d.FacilityId == facilityId && d.IsActive).ToListAsync();
        return dashboards.Select(MapToDTO).ToList();
    }

    public async Task<List<CustomDashboardDTO>> GetDashboardsByTypeAsync(string facilityId, string dashboardType)
    {
        var dashboards = await _context.CustomDashboards.Where(d => d.FacilityId == facilityId && d.DashboardType == dashboardType).ToListAsync();
        return dashboards.Select(MapToDTO).ToList();
    }

    public async Task<List<CustomDashboardDTO>> GetDefaultDashboardsAsync(string facilityId)
    {
        var dashboards = await _context.CustomDashboards.Where(d => d.FacilityId == facilityId && d.IsDefault).ToListAsync();
        return dashboards.Select(MapToDTO).ToList();
    }

    public async Task<List<CustomDashboardDTO>> GetSharedDashboardsAsync(Guid userId)
    {
        var dashboards = await _context.CustomDashboards.Where(d => d.IsShared).ToListAsync();
        return dashboards.Select(MapToDTO).ToList();
    }

    public async Task<List<CustomDashboardDTO>> GetUserDashboardsAsync(Guid userId)
    {
        var dashboards = await _context.CustomDashboards.Where(d => d.CreatedByUserId == userId).ToListAsync();
        return dashboards.Select(MapToDTO).ToList();
    }

    public async Task<CustomDashboardDTO> UpdateDashboardAsync(Guid dashboardId, CustomDashboardDTO dto)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.DashboardName = dto.DashboardName;
        dashboard.Description = dto.Description;
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO> AddWidgetAsync(Guid dashboardId, DashboardWidgetDTO widget)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task RemoveWidgetAsync(Guid dashboardId, Guid widgetId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard != null) { dashboard.LastModifiedDate = DateTime.UtcNow; _context.CustomDashboards.Update(dashboard); await _context.SaveChangesAsync(); }
    }

    public async Task<CustomDashboardDTO> UpdateWidgetAsync(Guid dashboardId, Guid widgetId, DashboardWidgetDTO widget)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO> AddFilterAsync(Guid dashboardId, DashboardFilterDTO filter)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO> RemoveFilterAsync(Guid dashboardId, Guid filterId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO> ShareDashboardAsync(Guid dashboardId, List<Guid> userIds, List<string>? roles = null)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.IsShared = true;
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO> UnshareWithUserAsync(Guid dashboardId, Guid userId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.LastModifiedDate = DateTime.UtcNow;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task<CustomDashboardDTO> MakeDashboardDefaultAsync(Guid dashboardId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        dashboard.IsDefault = true;
        _context.CustomDashboards.Update(dashboard);
        await _context.SaveChangesAsync();
        return MapToDTO(dashboard);
    }

    public async Task DeleteDashboardAsync(Guid dashboardId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard != null) { _context.CustomDashboards.Remove(dashboard); await _context.SaveChangesAsync(); }
    }

    public async Task<CustomDashboardDTO> CloneDashboardAsync(Guid dashboardId, string facilityId, string newName)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        var cloned = new CustomDashboard { FacilityId = facilityId, CreatedByUserId = dashboard.CreatedByUserId, DashboardName = newName, DashboardCode = dashboard.DashboardCode + "_Clone", DashboardType = dashboard.DashboardType, Description = dashboard.Description, CreatedDate = DateTime.UtcNow, IsActive = true };
        _context.CustomDashboards.Add(cloned);
        await _context.SaveChangesAsync();
        return MapToDTO(cloned);
    }

    public async Task<List<CustomDashboardDTO>> GetDashboardTemplatesAsync()
    {
        var dashboards = await _context.CustomDashboards.Where(d => d.IsTemplate).ToListAsync();
        return dashboards.Select(MapToDTO).ToList();
    }

    public async Task<byte[]> ExportDashboardAsync(Guid dashboardId, string format = "pdf") => new byte[] { };

    public async Task RecordDashboardViewAsync(Guid dashboardId, Guid userId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard != null) { dashboard.ViewCount += 1; dashboard.LastViewedDate = DateTime.UtcNow; _context.CustomDashboards.Update(dashboard); await _context.SaveChangesAsync(); }
    }

    public async Task<CustomDashboardAnalyticsDTO> GetDashboardAnalyticsAsync(long dashboardId)
    {
        var dashboard = await _context.CustomDashboards.FindAsync(dashboardId);
        if (dashboard == null) throw new InvalidOperationException($"Dashboard {dashboardId} not found");
        return new CustomDashboardAnalyticsDTO { DashboardId = dashboardId, ViewCount = dashboard.ViewCount, LastViewedDate = dashboard.LastViewedDate };
    }

    private CustomDashboardDTO MapToDTO(CustomDashboard dashboard) =>
        new CustomDashboardDTO { Id = dashboard.Id, FacilityId = dashboard.FacilityId, DashboardName = dashboard.DashboardName, DashboardCode = dashboard.DashboardCode, DashboardType = dashboard.DashboardType, Description = dashboard.Description, IsActive = dashboard.IsActive, CreatedDate = dashboard.CreatedDate };
}
