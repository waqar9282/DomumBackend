#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Domain.Entities;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class FacilityReportingService : IFacilityReportingService
    {
        private readonly ApplicationDbContext _context;

        public FacilityReportingService(ApplicationDbContext context)
            => _context = context;

        public async Task<string> GenerateFacilityReportAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            string generatedByUserId,
            CancellationToken cancellationToken)
        {
            var facility = await _context.Facilities.FindAsync(new object[] { facilityId }, cancellationToken);
            var incidents = await _context.Incidents
                .Where(i => i.FacilityId == facilityId && 
                           i.IncidentDate >= startDate && 
                           i.IncidentDate <= endDate)
                .ToListAsync(cancellationToken);

            var safeguardingConcerns = await _context.SafeguardingConcerns
                .Where(sc => sc.FacilityId == facilityId &&
                           sc.ConcernDate >= startDate &&
                           sc.ConcernDate <= endDate)
                .ToListAsync(cancellationToken);

            var report = new FacilityReport
            {
                FacilityId = facilityId,
                ReportName = $"Facility Report {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}",
                ReportGeneratedDate = DateTime.UtcNow,
                ReportPeriodStart = startDate,
                ReportPeriodEnd = endDate,
                TotalIncidentsReported = incidents.Count,
                HighRiskIncidents = incidents.Count(i => i.Severity >= IncidentSeverity.Serious),
                SafeguardingConcernsReported = safeguardingConcerns.Count,
                GeneratedByUserId = generatedByUserId,
                IsApproved = false,
                CreatedDate = DateTime.UtcNow
            };

            _context.FacilityReports.Add(report);
            await _context.SaveChangesAsync(cancellationToken);

            return report.Id.ToString();
        }

        public async Task<object> GetFacilityKPIsAsync(
            string facilityId,
            CancellationToken cancellationToken)
        {
            var youngPeople = await _context.YoungPeople
                .Where(yp => yp.FacilityId == facilityId)
                .ToListAsync(cancellationToken);

            var incidents = await _context.Incidents
                .Where(i => i.FacilityId == facilityId)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalYoungPeople = youngPeople.Count,
                TotalIncidents = incidents.Count,
                IncidentRate = youngPeople.Count > 0 ? (decimal)incidents.Count / youngPeople.Count : 0
            };
        }

        public async Task<object> GetFacilityComplianceMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var auditLogs = await _context.AuditLogs
                .Where(al => al.Timestamp >= startDate && al.Timestamp <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalAuditLogs = auditLogs.Count,
                ComplianceScore = auditLogs.Count > 0 ? 100 : 0
            };
        }

        public async Task<object> GetFacilityOutcomesAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var safeguardingConcerns = await _context.SafeguardingConcerns
                .Where(sc => sc.FacilityId == facilityId &&
                           sc.ConcernDate >= startDate &&
                           sc.ConcernDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalConcerns = safeguardingConcerns.Count,
                ResolvedConcerns = safeguardingConcerns.Count(sc => sc.Status == SafeguardingConcernStatus.Resolved)
            };
        }

        public async Task<bool> ApproveReportAsync(
            string reportId,
            string approvedByUserId,
            CancellationToken cancellationToken)
        {
            var report = await _context.FacilityReports.FindAsync(new object[] { reportId }, cancellationToken);
            if (report == null)
                return false;

            report.IsApproved = true;
            report.ApprovedByUserId = approvedByUserId;
            report.ApprovedDate = DateTime.UtcNow;

            _context.FacilityReports.Update(report);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
