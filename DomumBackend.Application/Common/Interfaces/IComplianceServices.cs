namespace DomumBackend.Application.Common.Interfaces;

using DomumBackend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IComplianceAuditService
{
    Task<ComplianceAuditDTO> CreateAsync(string facilityId, ComplianceAuditCreateDTO request);
    Task<ComplianceAuditDTO?> GetByIdAsync(long id);
    Task<List<ComplianceAuditDTO>> GetAllAsync(string facilityId);
    Task<List<ComplianceAuditDTO>> GetByTypeAsync(string facilityId, string auditType);
    Task<List<ComplianceAuditDTO>> GetByStatusAsync(string facilityId, string status);
    Task<List<ComplianceAuditDTO>> GetOverdueAsync(string facilityId);
    Task<List<ComplianceAuditDTO>> GetScheduledForDateRangeAsync(string facilityId, DateTime startDate, DateTime endDate);
    Task<List<ComplianceAuditDTO>> GetByAssignedStaffAsync(long staffId);
    Task<List<ComplianceAuditDTO>> GetByPriorityAsync(string facilityId, int priority);
    Task<List<ComplianceAuditDTO>> GetRequiringFollowUpAsync(string facilityId);
    Task<List<ComplianceAuditDTO>> SearchAsync(string facilityId, string searchTerm);
    Task<ComplianceAuditDTO> UpdateAsync(long id, ComplianceAuditUpdateDTO request);
    Task<ComplianceAuditDTO> StartAuditAsync(long id);
    Task<ComplianceAuditDTO> CompleteAuditAsync(long id, string findings);
    Task<ComplianceAuditDTO> RecordFollowUpAsync(long id, string findings);
    Task DeleteAsync(long id);
    Task<int> GetComplianceScoreAsync(long auditId);
    Task<List<ComplianceAuditSummaryDTO>> GetSummaryAsync(string facilityId);
    Task<Dictionary<string, int>> GetAuditDistributionByTypeAsync(string facilityId);
    Task<Dictionary<string, int>> GetAuditDistributionByStatusAsync(string facilityId);
    Task<List<ComplianceAuditDTO>> GetDueAuditsAsync(string facilityId, int daysThreshold);
}

public interface IComplianceChecklistService
{
    Task<ComplianceChecklistItemDTO> CreateAsync(long auditId, ComplianceChecklistItemCreateDTO request);
    Task<ComplianceChecklistItemDTO?> GetByIdAsync(long id);
    Task<List<ComplianceChecklistItemDTO>> GetByAuditAsync(long auditId);
    Task<List<ComplianceChecklistItemDTO>> GetByStatusAsync(long auditId, string status);
    Task<List<ComplianceChecklistItemDTO>> GetNonCompliantAsync(long auditId);
    Task<ComplianceChecklistItemDTO> UpdateAsync(long id, ComplianceChecklistItemUpdateDTO request);
    Task<ComplianceChecklistItemDTO> ReviewAsync(long id, long reviewedByStaffId, string notes);
    Task<ComplianceChecklistItemDTO> MarkCompliantAsync(long id, string evidence);
    Task<ComplianceChecklistItemDTO> MarkNonCompliantAsync(long id, string reason, string correctionAction, DateTime dueDate);
    Task DeleteAsync(long id);
    Task<int> GetCompliancePercentageAsync(long auditId);
    Task<List<ComplianceChecklistItemDTO>> GetByCategoryAsync(long auditId, string category);
    Task<List<ComplianceChecklistItemDTO>> GetRequiringEvidenceAsync(long auditId);
    Task<List<ComplianceChecklistItemDTO>> GetOverdueCorrectionAsync(long auditId);
    Task UpdateChecklistComplianceScoreAsync(long auditId);
}

public interface IComplianceNonConformityService
{
    Task<ComplianceNonConformityDTO> CreateAsync(long auditId, ComplianceNonConformityCreateDTO request);
    Task<ComplianceNonConformityDTO?> GetByIdAsync(long id);
    Task<List<ComplianceNonConformityDTO>> GetByAuditAsync(long auditId);
    Task<List<ComplianceNonConformityDTO>> GetByStatusAsync(long auditId, string status);
    Task<List<ComplianceNonConformityDTO>> GetOpenAsync(string facilityId);
    Task<List<ComplianceNonConformityDTO>> GetBySeverityAsync(string facilityId, string severity);
    Task<List<ComplianceNonConformityDTO>> GetOverdueAsync(string facilityId);
    Task<List<ComplianceNonConformityDTO>> GetByResponsibleStaffAsync(long staffId);
    Task<ComplianceNonConformityDTO> UpdateAsync(long id, ComplianceNonConformityUpdateDTO request);
    Task<ComplianceNonConformityDTO> ResolveAsync(long id, string resolutionNotes);
    Task<ComplianceNonConformityDTO> VerifyResolutionAsync(long id, long verifiedByStaffId);
    Task<ComplianceNonConformityDTO> ReopenAsync(long id, string reason);
    Task DeleteAsync(long id);
    Task<int> GetOpenCountAsync(string facilityId);
    Task<int> GetOverdueCountAsync(string facilityId);
    Task<Dictionary<string, int>> GetDistributionBySeverityAsync(string facilityId);
    Task<Dictionary<string, int>> GetDistributionByStatusAsync(string facilityId);
}

public interface IComplianceDocumentService
{
    Task<ComplianceDocumentDTO> CreateAsync(ComplianceDocumentCreateDTO request);
    Task<ComplianceDocumentDTO?> GetByIdAsync(long id);
    Task<List<ComplianceDocumentDTO>> GetByAuditAsync(long auditId);
    Task<List<ComplianceDocumentDTO>> GetByNonConformityAsync(long nonConformityId);
    Task<List<ComplianceDocumentDTO>> GetByTypeAsync(string documentType);
    Task<List<ComplianceDocumentDTO>> GetByAccessLevelAsync(string accessLevel);
    Task<List<ComplianceDocumentDTO>> GetExpiredAsync();
    Task<List<ComplianceDocumentDTO>> GetPendingReviewAsync();
    Task<List<ComplianceDocumentDTO>> GetPublishedAsync();
    Task<List<ComplianceDocumentDTO>> SearchByTagsAsync(string tags);
    Task<ComplianceDocumentDTO> UpdateAsync(long id, ComplianceDocumentUpdateDTO request);
    Task<ComplianceDocumentDTO> PublishAsync(long id);
    Task<ComplianceDocumentDTO> UnpublishAsync(long id);
    Task<ComplianceDocumentDTO> ArchiveAsync(long id);
    Task<ComplianceDocumentDTO> ReviewAsync(long id, long reviewedByStaffId, string status, string feedback);
    Task<ComplianceDocumentDTO> CreateNewVersionAsync(long originalDocumentId, ComplianceDocumentCreateDTO request);
    Task DeleteAsync(long id);
    Task<int> GetDocumentCountAsync(long auditId);
    Task<List<ComplianceDocumentDTO>> GetVersionHistoryAsync(long documentId);
}
