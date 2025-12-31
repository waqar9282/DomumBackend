using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.Reporting
{
    // ===== INCIDENT REPORT QUERIES =====

    public class GetIncidentReportQuery : IRequest<IncidentReportDTO>
    {
        public required string ReportId { get; set; }
    }

    public class GetIncidentReportQueryHandler : IRequestHandler<GetIncidentReportQuery, IncidentReportDTO>
    {
        private readonly IIncidentReportingService _reportingService;

        public GetIncidentReportQueryHandler(IIncidentReportingService reportingService)
            => _reportingService = reportingService;

        public async Task<IncidentReportDTO> Handle(GetIncidentReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _reportingService.GetIncidentTrendsAsync(
                request.ReportId, 
                DateTime.Now.AddMonths(-1), 
                DateTime.Now, 
                cancellationToken);
            return (IncidentReportDTO)report;
        }
    }

    public class GetIncidentTrendQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetIncidentTrendQueryHandler : IRequestHandler<GetIncidentTrendQuery, object>
    {
        private readonly IIncidentReportingService _reportingService;

        public GetIncidentTrendQueryHandler(IIncidentReportingService reportingService)
            => _reportingService = reportingService;

        public async Task<object> Handle(GetIncidentTrendQuery request, CancellationToken cancellationToken)
            => await _reportingService.GetIncidentTrendsAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                cancellationToken);
    }

    public class GetIncidentBreakdownByTypeQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetIncidentBreakdownByTypeQueryHandler : IRequestHandler<GetIncidentBreakdownByTypeQuery, object>
    {
        private readonly IIncidentReportingService _reportingService;

        public GetIncidentBreakdownByTypeQueryHandler(IIncidentReportingService reportingService)
            => _reportingService = reportingService;

        public async Task<object> Handle(GetIncidentBreakdownByTypeQuery request, CancellationToken cancellationToken)
            => await _reportingService.GetIncidentBreakdownByTypeAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                cancellationToken);
    }

    public class GetIncidentBreakdownBySeverityQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetIncidentBreakdownBySeverityQueryHandler : IRequestHandler<GetIncidentBreakdownBySeverityQuery, object>
    {
        private readonly IIncidentReportingService _reportingService;

        public GetIncidentBreakdownBySeverityQueryHandler(IIncidentReportingService reportingService)
            => _reportingService = reportingService;

        public async Task<object> Handle(GetIncidentBreakdownBySeverityQuery request, CancellationToken cancellationToken)
            => await _reportingService.GetIncidentBreakdownBySeverityAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                cancellationToken);
    }

    // ===== HEALTH METRICS REPORT QUERIES =====

    public class GetHealthMetricsReportQuery : IRequest<HealthMetricsReportDTO>
    {
        public required string ReportId { get; set; }
    }

    public class GetHealthMetricsReportQueryHandler : IRequestHandler<GetHealthMetricsReportQuery, HealthMetricsReportDTO>
    {
        private readonly IHealthAnalyticsService _analyticsService;

        public GetHealthMetricsReportQueryHandler(IHealthAnalyticsService analyticsService)
            => _analyticsService = analyticsService;

        public async Task<HealthMetricsReportDTO> Handle(GetHealthMetricsReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _analyticsService.GetHealthTrendsAsync(
                request.ReportId,
                DateTime.Now.AddMonths(-1),
                DateTime.Now,
                cancellationToken);
            return (HealthMetricsReportDTO)report;
        }
    }

    public class GetHealthTrendQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetHealthTrendQueryHandler : IRequestHandler<GetHealthTrendQuery, object>
    {
        private readonly IHealthAnalyticsService _analyticsService;

        public GetHealthTrendQueryHandler(IHealthAnalyticsService analyticsService)
            => _analyticsService = analyticsService;

        public async Task<object> Handle(GetHealthTrendQuery request, CancellationToken cancellationToken)
            => await _analyticsService.GetHealthTrendsAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                cancellationToken);
    }

    public class GetMedicationMetricsQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetMedicationMetricsQueryHandler : IRequestHandler<GetMedicationMetricsQuery, object>
    {
        private readonly IHealthAnalyticsService _analyticsService;

        public GetMedicationMetricsQueryHandler(IHealthAnalyticsService analyticsService)
            => _analyticsService = analyticsService;

        public async Task<object> Handle(GetMedicationMetricsQuery request, CancellationToken cancellationToken)
            => await _analyticsService.GetMedicationMetricsAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                cancellationToken);
    }

    // ===== FACILITY REPORT QUERIES =====

    public class GetFacilityReportQuery : IRequest<FacilityReportDTO>
    {
        public required string ReportId { get; set; }
    }

    public class GetFacilityReportQueryHandler : IRequestHandler<GetFacilityReportQuery, FacilityReportDTO>
    {
        private readonly IFacilityReportingService _facilityService;

        public GetFacilityReportQueryHandler(IFacilityReportingService facilityService)
            => _facilityService = facilityService;

        public async Task<FacilityReportDTO> Handle(GetFacilityReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _facilityService.GetFacilityKPIsAsync(request.ReportId, cancellationToken);
            return (FacilityReportDTO)report;
        }
    }

    public class GetFacilityKPIsQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
    }

    public class GetFacilityKPIsQueryHandler : IRequestHandler<GetFacilityKPIsQuery, object>
    {
        private readonly IFacilityReportingService _facilityService;

        public GetFacilityKPIsQueryHandler(IFacilityReportingService facilityService)
            => _facilityService = facilityService;

        public async Task<object> Handle(GetFacilityKPIsQuery request, CancellationToken cancellationToken)
            => await _facilityService.GetFacilityKPIsAsync(request.FacilityId, cancellationToken);
    }

    public class GetFacilityComplianceMetricsQuery : IRequest<object>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetFacilityComplianceMetricsQueryHandler : IRequestHandler<GetFacilityComplianceMetricsQuery, object>
    {
        private readonly IFacilityReportingService _facilityService;

        public GetFacilityComplianceMetricsQueryHandler(IFacilityReportingService facilityService)
            => _facilityService = facilityService;

        public async Task<object> Handle(GetFacilityComplianceMetricsQuery request, CancellationToken cancellationToken)
            => await _facilityService.GetFacilityComplianceMetricsAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                cancellationToken);
    }
}
