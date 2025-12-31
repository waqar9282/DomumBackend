#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Domain.Entities;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class HealthAnalyticsService : IHealthAnalyticsService
    {
        private readonly ApplicationDbContext _context;

        public HealthAnalyticsService(ApplicationDbContext context)
            => _context = context;

        public async Task<string> GenerateHealthMetricsReportAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            string generatedByUserId,
            CancellationToken cancellationToken)
        {
            var assessments = await _context.HealthAssessments
                .Where(ha => ha.FacilityId == facilityId &&
                           ha.AssessmentDate >= startDate &&
                           ha.AssessmentDate <= endDate)
                .ToListAsync(cancellationToken);

            var medications = await _context.Medications
                .Where(m => m.FacilityId == facilityId &&
                          m.CreatedDate >= startDate &&
                          m.CreatedDate <= endDate)
                .ToListAsync(cancellationToken);

            var nutritionLogs = await _context.NutritionLogs
                .Where(nl => nl.FacilityId == facilityId &&
                           nl.LogDate >= startDate &&
                           nl.LogDate <= endDate)
                .ToListAsync(cancellationToken);

            var report = new HealthMetricsReport
            {
                FacilityId = facilityId,
                ReportName = $"Health Metrics Report {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}",
                ReportGeneratedDate = DateTime.UtcNow,
                ReportPeriodStart = startDate,
                ReportPeriodEnd = endDate,
                TotalAssessments = assessments.Count,
                AverageHeight = assessments.Any() ? (decimal)assessments.Average(a => a.Height ?? 0) : 0,
                AverageWeight = assessments.Any() ? (decimal)assessments.Average(a => a.Weight ?? 0) : 0,
                YoungPeopleOnMedication = medications.Select(m => m.YoungPersonId).Distinct().Count(),
                ActiveMedicationCount = medications.Count,
                NutritionLogsRecorded = nutritionLogs.Count,
                MentalHealthCheckInsRecorded = await _context.MentalHealthCheckIns
                    .Where(m => m.FacilityId == facilityId &&
                              m.CheckInDate >= startDate &&
                              m.CheckInDate <= endDate)
                    .CountAsync(cancellationToken),
                GeneratedByUserId = generatedByUserId,
                IsApproved = false,
                CreatedDate = DateTime.UtcNow
            };

            _context.HealthMetricsReports.Add(report);
            await _context.SaveChangesAsync(cancellationToken);

            return report.Id.ToString();
        }

        public async Task<object> GetHealthTrendsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var assessments = await _context.HealthAssessments
                .Where(ha => ha.FacilityId == facilityId &&
                           ha.AssessmentDate >= startDate &&
                           ha.AssessmentDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalAssessments = assessments.Count,
                AverageWeight = assessments.Any() ? assessments.Average(a => a.Weight ?? 0) : 0,
                AverageHeight = assessments.Any() ? assessments.Average(a => a.Height ?? 0) : 0,
                AverageBMI = assessments.Any() ? assessments.Average(a => a.BMI ?? 0) : 0
            };
        }

        public async Task<object> GetMedicationMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var medications = await _context.Medications
                .Where(m => m.FacilityId == facilityId &&
                          m.CreatedDate >= startDate &&
                          m.CreatedDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalMedications = medications.Count,
                YoungPeopleOnMeds = medications.Select(m => m.YoungPersonId).Distinct().Count(),
                ActiveMeds = medications.Count(m => m.Status == DomumBackend.Domain.Enums.MedicationStatus.Active)
            };
        }

        public async Task<object> GetNutritionMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var logs = await _context.NutritionLogs
                .Where(nl => nl.FacilityId == facilityId &&
                           nl.LogDate >= startDate &&
                           nl.LogDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalLogsRecorded = logs.Count,
                AverageCalories = logs.Any() ? logs.Average(l => l.Calories ?? 0) : 0,
                AverageProtein = logs.Any() ? logs.Average(l => l.Protein ?? 0) : 0
            };
        }

        public async Task<object> GetActivityMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var activities = await _context.PhysicalActivityLogs
                .Where(pal => pal.FacilityId == facilityId &&
                            pal.ActivityDate >= startDate &&
                            pal.ActivityDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalActivities = activities.Count,
                AverageDurationMinutes = activities.Any() ? activities.Average(a => a.DurationMinutes) : 0,
                YoungPeopleMeetingGoals = activities.Count(a => a.MeetsPhysicalActivityGoal)
            };
        }

        public async Task<object> GetMentalHealthMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var checkIns = await _context.MentalHealthCheckIns
                .Where(m => m.FacilityId == facilityId &&
                          m.CheckInDate >= startDate &&
                          m.CheckInDate <= endDate)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalCheckIns = checkIns.Count,
                FlaggedForConcern = checkIns.Count(c => c.HasSuicidalThoughts || c.IsSelfHarming),
                GoodMood = checkIns.Count(c => c.CurrentMood >= MoodLevel.Good)
            };
        }

        public async Task<bool> ApproveReportAsync(
            string reportId,
            string approvedByUserId,
            CancellationToken cancellationToken)
        {
            var report = await _context.HealthMetricsReports.FindAsync(new object[] { reportId }, cancellationToken);
            if (report == null)
                return false;

            report.IsApproved = true;
            report.ApprovedByUserId = approvedByUserId;
            report.ApprovedDate = DateTime.UtcNow;

            _context.HealthMetricsReports.Update(report);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
