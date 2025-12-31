using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Commands.Reporting
{
    // ===== INCIDENT REPORT COMMANDS =====

    public class GenerateIncidentReportCommand : IRequest<ReportGenerationResponseDTO>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string GeneratedByUserId { get; set; }
        public string? ReportName { get; set; }
    }

    public class GenerateIncidentReportCommandHandler : IRequestHandler<GenerateIncidentReportCommand, ReportGenerationResponseDTO>
    {
        private readonly IIncidentReportingService _reportingService;

        public GenerateIncidentReportCommandHandler(IIncidentReportingService reportingService)
            => _reportingService = reportingService;

        public async Task<ReportGenerationResponseDTO> Handle(GenerateIncidentReportCommand request, CancellationToken cancellationToken)
        {
            var reportId = await _reportingService.GenerateIncidentReportAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                request.GeneratedByUserId,
                cancellationToken);

            return new ReportGenerationResponseDTO
            {
                ReportId = reportId,
                ReportName = request.ReportName ?? $"Incident Report {DateTime.Now:yyyy-MM-dd}",
                ReportType = "Incident",
                GeneratedDate = DateTime.UtcNow,
                Success = true,
                Message = "Incident report generated successfully"
            };
        }
    }

    public class ApproveIncidentReportCommand : IRequest<bool>
    {
        public required string ReportId { get; set; }
        public required string ApprovedByUserId { get; set; }
    }

    public class ApproveIncidentReportCommandHandler : IRequestHandler<ApproveIncidentReportCommand, bool>
    {
        private readonly IIncidentReportingService _reportingService;

        public ApproveIncidentReportCommandHandler(IIncidentReportingService reportingService)
            => _reportingService = reportingService;

        public async Task<bool> Handle(ApproveIncidentReportCommand request, CancellationToken cancellationToken)
            => await _reportingService.ApproveReportAsync(
                request.ReportId,
                request.ApprovedByUserId,
                cancellationToken);
    }

    // ===== HEALTH METRICS REPORT COMMANDS =====

    public class GenerateHealthMetricsReportCommand : IRequest<ReportGenerationResponseDTO>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string GeneratedByUserId { get; set; }
        public string? ReportName { get; set; }
    }

    public class GenerateHealthMetricsReportCommandHandler : IRequestHandler<GenerateHealthMetricsReportCommand, ReportGenerationResponseDTO>
    {
        private readonly IHealthAnalyticsService _analyticsService;

        public GenerateHealthMetricsReportCommandHandler(IHealthAnalyticsService analyticsService)
            => _analyticsService = analyticsService;

        public async Task<ReportGenerationResponseDTO> Handle(GenerateHealthMetricsReportCommand request, CancellationToken cancellationToken)
        {
            var reportId = await _analyticsService.GenerateHealthMetricsReportAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                request.GeneratedByUserId,
                cancellationToken);

            return new ReportGenerationResponseDTO
            {
                ReportId = reportId,
                ReportName = request.ReportName ?? $"Health Metrics Report {DateTime.Now:yyyy-MM-dd}",
                ReportType = "Health Metrics",
                GeneratedDate = DateTime.UtcNow,
                Success = true,
                Message = "Health metrics report generated successfully"
            };
        }
    }

    public class ApproveHealthMetricsReportCommand : IRequest<bool>
    {
        public required string ReportId { get; set; }
        public required string ApprovedByUserId { get; set; }
    }

    public class ApproveHealthMetricsReportCommandHandler : IRequestHandler<ApproveHealthMetricsReportCommand, bool>
    {
        private readonly IHealthAnalyticsService _analyticsService;

        public ApproveHealthMetricsReportCommandHandler(IHealthAnalyticsService analyticsService)
            => _analyticsService = analyticsService;

        public async Task<bool> Handle(ApproveHealthMetricsReportCommand request, CancellationToken cancellationToken)
            => await _analyticsService.ApproveReportAsync(
                request.ReportId,
                request.ApprovedByUserId,
                cancellationToken);
    }

    // ===== FACILITY REPORT COMMANDS =====

    public class GenerateFacilityReportCommand : IRequest<ReportGenerationResponseDTO>
    {
        public required string FacilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string GeneratedByUserId { get; set; }
        public string? ReportName { get; set; }
    }

    public class GenerateFacilityReportCommandHandler : IRequestHandler<GenerateFacilityReportCommand, ReportGenerationResponseDTO>
    {
        private readonly IFacilityReportingService _facilityService;

        public GenerateFacilityReportCommandHandler(IFacilityReportingService facilityService)
            => _facilityService = facilityService;

        public async Task<ReportGenerationResponseDTO> Handle(GenerateFacilityReportCommand request, CancellationToken cancellationToken)
        {
            var reportId = await _facilityService.GenerateFacilityReportAsync(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                request.GeneratedByUserId,
                cancellationToken);

            return new ReportGenerationResponseDTO
            {
                ReportId = reportId,
                ReportName = request.ReportName ?? $"Facility Report {DateTime.Now:yyyy-MM-dd}",
                ReportType = "Facility",
                GeneratedDate = DateTime.UtcNow,
                Success = true,
                Message = "Facility report generated successfully"
            };
        }
    }

    public class ApproveFacilityReportCommand : IRequest<bool>
    {
        public required string ReportId { get; set; }
        public required string ApprovedByUserId { get; set; }
    }

    public class ApproveFacilityReportCommandHandler : IRequestHandler<ApproveFacilityReportCommand, bool>
    {
        private readonly IFacilityReportingService _facilityService;

        public ApproveFacilityReportCommandHandler(IFacilityReportingService facilityService)
            => _facilityService = facilityService;

        public async Task<bool> Handle(ApproveFacilityReportCommand request, CancellationToken cancellationToken)
            => await _facilityService.ApproveReportAsync(
                request.ReportId,
                request.ApprovedByUserId,
                cancellationToken);
    }

    // ===== EXPORT COMMANDS =====

    public class ExportReportCommand : IRequest<byte[]>
    {
        public required string ReportId { get; set; }
        public required string Format { get; set; } // PDF, Excel, CSV
    }

    public class ExportReportCommandHandler : IRequestHandler<ExportReportCommand, byte[]>
    {
        private readonly IReportExportService _exportService;

        public ExportReportCommandHandler(IReportExportService exportService)
            => _exportService = exportService;

        public async Task<byte[]> Handle(ExportReportCommand request, CancellationToken cancellationToken)
        {
            return request.Format.ToUpper() switch
            {
                "PDF" => await _exportService.ExportReportToPdfAsync(request.ReportId, cancellationToken),
                "EXCEL" => await _exportService.ExportReportToExcelAsync(request.ReportId, cancellationToken),
                "CSV" => await _exportService.ExportReportToCsvAsync(request.ReportId, cancellationToken),
                _ => throw new ArgumentException($"Unsupported export format: {request.Format}")
            };
        }
    }
}
