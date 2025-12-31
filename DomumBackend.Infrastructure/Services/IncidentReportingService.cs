#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Domain.Entities;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class IncidentReportingService : IIncidentReportingService
    {
        private readonly ApplicationDbContext _context;

        public IncidentReportingService(ApplicationDbContext context)
            => _context = context;

        public async Task<string> GenerateIncidentReportAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            string generatedByUserId,
            CancellationToken cancellationToken)
        {
            var incidents = await _context.Incidents
                .Where(i => i.FacilityId == facilityId && 
                           i.IncidentDate >= startDate && 
                           i.IncidentDate <= endDate)
                .ToListAsync(cancellationToken);

            var report = new IncidentReport
            {
                FacilityId = facilityId,
                ReportName = $"Incident Report {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}",
                ReportGeneratedDate = DateTime.UtcNow,
                ReportPeriodStart = startDate,
                ReportPeriodEnd = endDate,
                TotalIncidents = incidents.Count,
                HighSeverityCount = incidents.Count(i => i.Severity >= IncidentSeverity.Serious),
                MediumSeverityCount = incidents.Count(i => i.Severity == IncidentSeverity.Moderate),
                LowSeverityCount = incidents.Count(i => i.Severity == IncidentSeverity.Minor),
                OpenIncidents = incidents.Count(i => i.Status == IncidentStatus.Open),
                ClosedIncidents = incidents.Count(i => i.Status == IncidentStatus.Closed),
                GeneratedByUserId = generatedByUserId,
                IsApproved = false,
                CreatedDate = DateTime.UtcNow
            };

            _context.IncidentReports.Add(report);
            await _context.SaveChangesAsync(cancellationToken);

            return report.Id.ToString();
        }

        public async Task<object> GetIncidentTrendsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var incidents = await _context.Incidents
                .Where(i => i.FacilityId == facilityId && 
                           i.IncidentDate >= startDate && 
                           i.IncidentDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalIncidents = incidents.Count,
                AveragePerDay = incidents.Count / ((endDate - startDate).Days + 1),
                HighSeverity = incidents.Count(i => (int)i.Severity >= 3),
                MediumSeverity = incidents.Count(i => (int)i.Severity == 2),
                LowSeverity = incidents.Count(i => (int)i.Severity == 1)
            };
        }

        public async Task<object> GetIncidentBreakdownByTypeAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var incidents = await _context.Incidents
                .Where(i => i.FacilityId == facilityId && 
                           i.IncidentDate >= startDate && 
                           i.IncidentDate <= endDate)
                .ToListAsync(cancellationToken);

            return incidents
                .GroupBy(i => i.IncidentType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();
        }

        public async Task<object> GetIncidentBreakdownBySeverityAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var incidents = await _context.Incidents
                .Where(i => i.FacilityId == facilityId && 
                           i.IncidentDate >= startDate && 
                           i.IncidentDate <= endDate)
                .ToListAsync(cancellationToken);

            return incidents
                .GroupBy(i => i.Severity)
                .Select(g => new { Severity = g.Key, Count = g.Count() })
                .ToList();
        }

        public async Task<bool> ApproveReportAsync(
            string reportId,
            string approvedByUserId,
            CancellationToken cancellationToken)
        {
            var report = await _context.IncidentReports.FindAsync(new object[] { reportId }, cancellationToken);
            if (report == null)
                return false;

            report.IsApproved = true;
            report.ApprovedByUserId = approvedByUserId;
            report.ApprovedDate = DateTime.UtcNow;

            _context.IncidentReports.Update(report);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
