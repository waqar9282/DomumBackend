namespace DomumBackend.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsMetricService _metricService;
    private readonly ITrendAnalysisService _trendService;
    private readonly IPredictiveAlertService _alertService;
    private readonly ICustomDashboardService _dashboardService;

    public AnalyticsController(
        IAnalyticsMetricService metricService,
        ITrendAnalysisService trendService,
        IPredictiveAlertService alertService,
        ICustomDashboardService dashboardService)
    {
        _metricService = metricService;
        _trendService = trendService;
        _alertService = alertService;
        _dashboardService = dashboardService;
    }

    private string GetFacilityId()
    {
        var facilityId = User.FindFirst("FacilityId");
        return facilityId?.Value ?? string.Empty;
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirst("UserId");
        return userId != null ? Guid.Parse(userId.Value) : Guid.Empty;
    }

    #region Analytics Metrics

    [HttpPost("metrics")]
    public async Task<IActionResult> CreateMetric([FromBody] AnalyticsMetricDTO request)
    {
        var facilityId = GetFacilityId();
        var result = await _metricService.CreateMetricAsync(facilityId, request);
        return CreatedAtAction(nameof(GetMetricById), new { id = result.Id }, result);
    }

    [HttpGet("metrics/{id}")]
    public async Task<IActionResult> GetMetricById(Guid id)
    {
        var result = await _metricService.GetMetricByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("metrics/category/{category}")]
    public async Task<IActionResult> GetMetricsByCategory(string category)
    {
        var facilityId = GetFacilityId();
        var result = await _metricService.GetMetricsByCategoryAsync(facilityId, category);
        return Ok(result);
    }

    [HttpGet("metrics")]
    public async Task<IActionResult> GetAllMetrics()
    {
        var facilityId = GetFacilityId();
        var result = await _metricService.GetAllMetricsAsync(facilityId);
        return Ok(result);
    }

    [HttpPut("metrics/{id}")]
    public async Task<IActionResult> UpdateMetric(Guid id, [FromBody] AnalyticsMetricDTO request)
    {
        var result = await _metricService.UpdateMetricAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("metrics/{id}")]
    public async Task<IActionResult> DeleteMetric(Guid id)
    {
        await _metricService.DeleteMetricAsync(id);
        return NoContent();
    }

    [HttpGet("metrics/kpi")]
    public async Task<IActionResult> GetKeyPerformanceIndicators()
    {
        var facilityId = GetFacilityId();
        var result = await _metricService.GetKeyPerformanceIndicatorsAsync(facilityId);
        return Ok(result);
    }

    #endregion

    #region Trend Analysis

    [HttpPost("trends")]
    public async Task<IActionResult> CreateTrendAnalysis([FromBody] CreateTrendRequest request)
    {
        var facilityId = GetFacilityId();
        var result = await _trendService.CreateTrendAnalysisAsync(facilityId, request.MetricId, request.DaysToAnalyze);
        return CreatedAtAction(nameof(GetLatestTrend), new { metricId = request.MetricId }, result);
    }

    [HttpGet("trends/metric/{metricId}")]
    public async Task<IActionResult> GetLatestTrend(long metricId)
    {
        var result = await _trendService.GetLatestTrendAsync(metricId);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("trends/type/{trendType}")]
    public async Task<IActionResult> GetTrendsByType(string trendType)
    {
        var facilityId = GetFacilityId();
        var result = await _trendService.GetTrendsByTypeAsync(facilityId, trendType);
        return Ok(result);
    }

    [HttpGet("trends/seasonal")]
    public async Task<IActionResult> GetSeasonalTrends()
    {
        var facilityId = GetFacilityId();
        var result = await _trendService.GetSeasonalTrendsAsync(facilityId);
        return Ok(result);
    }

    #endregion

    #region Predictive Alerts

    [HttpPost("alerts")]
    public async Task<IActionResult> CreateAlert([FromBody] PredictiveAlertDTO request)
    {
        var facilityId = GetFacilityId();
        var result = await _alertService.CreateAlertAsync(facilityId, request);
        return CreatedAtAction(nameof(GetAlertById), new { id = result.Id }, result);
    }

    [HttpGet("alerts/{id}")]
    public async Task<IActionResult> GetAlertById(Guid id)
    {
        var result = await _alertService.GetAlertByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("alerts/active")]
    public async Task<IActionResult> GetActiveAlerts()
    {
        var facilityId = GetFacilityId();
        var result = await _alertService.GetActiveAlertsAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("alerts/critical")]
    public async Task<IActionResult> GetCriticalAlerts()
    {
        var facilityId = GetFacilityId();
        var result = await _alertService.GetCriticalAlertsAsync(facilityId);
        return Ok(result);
    }

    [HttpPost("alerts/{id}/acknowledge")]
    public async Task<IActionResult> AcknowledgeAlert(Guid id, [FromBody] string notes)
    {
        var userId = GetUserId();
        var result = await _alertService.AcknowledgeAlertAsync(id, userId, notes);
        return Ok(result);
    }

    [HttpPost("alerts/{id}/resolve")]
    public async Task<IActionResult> ResolveAlert(Guid id, [FromBody] string notes)
    {
        var userId = GetUserId();
        var result = await _alertService.ResolveAlertAsync(id, userId, notes);
        return Ok(result);
    }

    [HttpDelete("alerts/{id}")]
    public async Task<IActionResult> DeleteAlert(Guid id)
    {
        await _alertService.DeleteAlertAsync(id);
        return NoContent();
    }

    #endregion

    #region Custom Dashboards

    [HttpPost("dashboards")]
    public async Task<IActionResult> CreateDashboard([FromBody] CustomDashboardDTO request)
    {
        var facilityId = GetFacilityId();
        var userId = GetUserId();
        var result = await _dashboardService.CreateDashboardAsync(facilityId, userId, request);
        return CreatedAtAction(nameof(GetDashboardById), new { id = result.Id }, result);
    }

    [HttpGet("dashboards/{id}")]
    public async Task<IActionResult> GetDashboardById(Guid id)
    {
        var result = await _dashboardService.GetDashboardByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("dashboards")]
    public async Task<IActionResult> GetDashboardsByFacility()
    {
        var facilityId = GetFacilityId();
        var result = await _dashboardService.GetDashboardsByFacilityAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("dashboards/user")]
    public async Task<IActionResult> GetUserDashboards()
    {
        var userId = GetUserId();
        var result = await _dashboardService.GetUserDashboardsAsync(userId);
        return Ok(result);
    }

    [HttpPut("dashboards/{id}")]
    public async Task<IActionResult> UpdateDashboard(Guid id, [FromBody] CustomDashboardDTO request)
    {
        var result = await _dashboardService.UpdateDashboardAsync(id, request);
        return Ok(result);
    }

    [HttpPost("dashboards/{id}/clone")]
    public async Task<IActionResult> CloneDashboard(Guid id, [FromBody] string newName)
    {
        var facilityId = GetFacilityId();
        var result = await _dashboardService.CloneDashboardAsync(id, facilityId, newName);
        return CreatedAtAction(nameof(GetDashboardById), new { id = result.Id }, result);
    }

    [HttpDelete("dashboards/{id}")]
    public async Task<IActionResult> DeleteDashboard(Guid id)
    {
        await _dashboardService.DeleteDashboardAsync(id);
        return NoContent();
    }

    [HttpPost("dashboards/{id}/view")]
    public async Task<IActionResult> RecordDashboardView(Guid id)
    {
        var userId = GetUserId();
        await _dashboardService.RecordDashboardViewAsync(id, userId);
        return Ok();
    }

    #endregion
}

// Request DTOs
public class CreateTrendRequest
{
    public long MetricId { get; set; }
    public int DaysToAnalyze { get; set; }
}
