namespace DomumBackend.Application.Common.Interfaces
{
    using DomumBackend.Application.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for incident reporting and analytics
    /// </summary>
    public interface IIncidentReportingService
    {
        Task<string> GenerateIncidentReportAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            string generatedByUserId,
            CancellationToken cancellationToken);

        Task<object> GetIncidentTrendsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetIncidentBreakdownByTypeAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetIncidentBreakdownBySeverityAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<bool> ApproveReportAsync(
            string reportId,
            string approvedByUserId,
            CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface for health metrics analytics
    /// </summary>
    public interface IHealthAnalyticsService
    {
        Task<string> GenerateHealthMetricsReportAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            string generatedByUserId,
            CancellationToken cancellationToken);

        Task<object> GetHealthTrendsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetMedicationMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetNutritionMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetActivityMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetMentalHealthMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<bool> ApproveReportAsync(
            string reportId,
            string approvedByUserId,
            CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface for facility-wide reporting
    /// </summary>
    public interface IFacilityReportingService
    {
        Task<string> GenerateFacilityReportAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            string generatedByUserId,
            CancellationToken cancellationToken);

        Task<object> GetFacilityKPIsAsync(
            string facilityId,
            CancellationToken cancellationToken);

        Task<object> GetFacilityComplianceMetricsAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<object> GetFacilityOutcomesAsync(
            string facilityId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task<bool> ApproveReportAsync(
            string reportId,
            string approvedByUserId,
            CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface for report export functionality
    /// </summary>
    public interface IReportExportService
    {
        Task<byte[]> ExportReportToPdfAsync(
            string reportId,
            CancellationToken cancellationToken);

        Task<byte[]> ExportReportToExcelAsync(
            string reportId,
            CancellationToken cancellationToken);

        Task<byte[]> ExportReportToCsvAsync(
            string reportId,
            CancellationToken cancellationToken);

        Task<string> GetExportedFilePathAsync(
            string reportId,
            CancellationToken cancellationToken);
    }
}

