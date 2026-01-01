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
public class ComplianceController : ControllerBase
{
    private readonly IComplianceAuditService _auditService;
    private readonly IComplianceChecklistService _checklistService;
    private readonly IComplianceNonConformityService _nonConformityService;
    private readonly IComplianceDocumentService _documentService;

    public ComplianceController(
        IComplianceAuditService auditService,
        IComplianceChecklistService checklistService,
        IComplianceNonConformityService nonConformityService,
        IComplianceDocumentService documentService)
    {
        _auditService = auditService;
        _checklistService = checklistService;
        _nonConformityService = nonConformityService;
        _documentService = documentService;
    }

    private string GetFacilityId()
    {
        var facilityId = User.FindFirst("FacilityId");
        return facilityId?.Value ?? string.Empty;
    }

    #region Compliance Audits

    [HttpPost("audits")]
    public async Task<IActionResult> CreateAudit([FromBody] ComplianceAuditCreateDTO request)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.CreateAsync(facilityId, request);
        return CreatedAtAction(nameof(GetAuditById), new { id = result.Id }, result);
    }

    [HttpGet("audits/{id}")]
    public async Task<IActionResult> GetAuditById(long id)
    {
        var result = await _auditService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("audits")]
    public async Task<IActionResult> GetAllAudits()
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetAllAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("audits/type/{auditType}")]
    public async Task<IActionResult> GetAuditsByType(string auditType)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetByTypeAsync(facilityId, auditType);
        return Ok(result);
    }

    [HttpGet("audits/status/{status}")]
    public async Task<IActionResult> GetAuditsByStatus(string status)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetByStatusAsync(facilityId, status);
        return Ok(result);
    }

    [HttpGet("audits/priority/{priority}")]
    public async Task<IActionResult> GetAuditsByPriority(int priority)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetByPriorityAsync(facilityId, priority);
        return Ok(result);
    }

    [HttpGet("audits/overdue")]
    public async Task<IActionResult> GetOverdueAudits()
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetOverdueAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("audits/due")]
    public async Task<IActionResult> GetDueAudits([FromQuery] int daysThreshold = 30)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetDueAuditsAsync(facilityId, daysThreshold);
        return Ok(result);
    }

    [HttpGet("audits/follow-up")]
    public async Task<IActionResult> GetAuditsRequiringFollowUp()
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetRequiringFollowUpAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("audits/date-range")]
    public async Task<IActionResult> GetAuditsForDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetScheduledForDateRangeAsync(facilityId, startDate, endDate);
        return Ok(result);
    }

    [HttpGet("audits/staff/{staffId}")]
    public async Task<IActionResult> GetAuditsByAssignedStaff(long staffId)
    {
        var result = await _auditService.GetByAssignedStaffAsync(staffId);
        return Ok(result);
    }

    [HttpGet("audits/search")]
    public async Task<IActionResult> SearchAudits([FromQuery] string searchTerm)
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.SearchAsync(facilityId, searchTerm);
        return Ok(result);
    }

    [HttpPut("audits/{id}")]
    public async Task<IActionResult> UpdateAudit(long id, [FromBody] ComplianceAuditUpdateDTO request)
    {
        var result = await _auditService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpPost("audits/{id}/start")]
    public async Task<IActionResult> StartAudit(long id)
    {
        var result = await _auditService.StartAuditAsync(id);
        return Ok(result);
    }

    [HttpPost("audits/{id}/complete")]
    public async Task<IActionResult> CompleteAudit(long id, [FromBody] string findings)
    {
        var result = await _auditService.CompleteAuditAsync(id, findings);
        return Ok(result);
    }

    [HttpPost("audits/{id}/follow-up")]
    public async Task<IActionResult> RecordFollowUp(long id, [FromBody] string findings)
    {
        var result = await _auditService.RecordFollowUpAsync(id, findings);
        return Ok(result);
    }

    [HttpDelete("audits/{id}")]
    public async Task<IActionResult> DeleteAudit(long id)
    {
        await _auditService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("audits/{id}/compliance-score")]
    public async Task<IActionResult> GetComplianceScore(long id)
    {
        var score = await _auditService.GetComplianceScoreAsync(id);
        return Ok(new { complianceScore = score });
    }

    [HttpGet("audits/summary")]
    public async Task<IActionResult> GetAuditsSummary()
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetSummaryAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("audits/distribution/type")]
    public async Task<IActionResult> GetAuditDistributionByType()
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetAuditDistributionByTypeAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("audits/distribution/status")]
    public async Task<IActionResult> GetAuditDistributionByStatus()
    {
        var facilityId = GetFacilityId();
        var result = await _auditService.GetAuditDistributionByStatusAsync(facilityId);
        return Ok(result);
    }

    #endregion

    #region Compliance Checklist

    [HttpPost("audits/{auditId}/checklist")]
    public async Task<IActionResult> CreateChecklistItem(long auditId, [FromBody] ComplianceChecklistItemCreateDTO request)
    {
        var result = await _checklistService.CreateAsync(auditId, request);
        return CreatedAtAction(nameof(GetChecklistItemById), new { id = result.Id }, result);
    }

    [HttpGet("checklist/{id}")]
    public async Task<IActionResult> GetChecklistItemById(long id)
    {
        var result = await _checklistService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/checklist")]
    public async Task<IActionResult> GetChecklistItems(long auditId)
    {
        var result = await _checklistService.GetByAuditAsync(auditId);
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/checklist/status/{status}")]
    public async Task<IActionResult> GetChecklistItemsByStatus(long auditId, string status)
    {
        var result = await _checklistService.GetByStatusAsync(auditId, status);
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/checklist/non-compliant")]
    public async Task<IActionResult> GetNonCompliantItems(long auditId)
    {
        var result = await _checklistService.GetNonCompliantAsync(auditId);
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/checklist/category/{category}")]
    public async Task<IActionResult> GetChecklistItemsByCategory(long auditId, string category)
    {
        var result = await _checklistService.GetByCategoryAsync(auditId, category);
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/checklist/requiring-evidence")]
    public async Task<IActionResult> GetItemsRequiringEvidence(long auditId)
    {
        var result = await _checklistService.GetRequiringEvidenceAsync(auditId);
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/checklist/overdue-correction")]
    public async Task<IActionResult> GetOverdueCorrectionItems(long auditId)
    {
        var result = await _checklistService.GetOverdueCorrectionAsync(auditId);
        return Ok(result);
    }

    [HttpPut("checklist/{id}")]
    public async Task<IActionResult> UpdateChecklistItem(long id, [FromBody] ComplianceChecklistItemUpdateDTO request)
    {
        var result = await _checklistService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpPost("checklist/{id}/review")]
    public async Task<IActionResult> ReviewChecklistItem(long id, [FromBody] dynamic request)
    {
        var result = await _checklistService.ReviewAsync(id, request.reviewedByStaffId, request.notes);
        return Ok(result);
    }

    [HttpPost("checklist/{id}/mark-compliant")]
    public async Task<IActionResult> MarkChecklistItemCompliant(long id, [FromBody] string evidence)
    {
        var result = await _checklistService.MarkCompliantAsync(id, evidence);
        return Ok(result);
    }

    [HttpPost("checklist/{id}/mark-non-compliant")]
    public async Task<IActionResult> MarkChecklistItemNonCompliant(long id, [FromBody] dynamic request)
    {
        var result = await _checklistService.MarkNonCompliantAsync(id, request.reason, request.correctionAction, request.dueDate);
        return Ok(result);
    }

    [HttpDelete("checklist/{id}")]
    public async Task<IActionResult> DeleteChecklistItem(long id)
    {
        await _checklistService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("audits/{auditId}/checklist/compliance-percentage")]
    public async Task<IActionResult> GetChecklistCompliancePercentage(long auditId)
    {
        var result = await _checklistService.GetCompliancePercentageAsync(auditId);
        return Ok(new { compliancePercentage = result });
    }

    #endregion

    #region Non-Conformities

    [HttpPost("audits/{auditId}/non-conformities")]
    public async Task<IActionResult> CreateNonConformity(long auditId, [FromBody] ComplianceNonConformityCreateDTO request)
    {
        var result = await _nonConformityService.CreateAsync(auditId, request);
        return CreatedAtAction(nameof(GetNonConformityById), new { id = result.Id }, result);
    }

    [HttpGet("non-conformities/{id}")]
    public async Task<IActionResult> GetNonConformityById(long id)
    {
        var result = await _nonConformityService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/non-conformities")]
    public async Task<IActionResult> GetNonConformitiesByAudit(long auditId)
    {
        var result = await _nonConformityService.GetByAuditAsync(auditId);
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/non-conformities/status/{status}")]
    public async Task<IActionResult> GetNonConformitiesByStatus(long auditId, string status)
    {
        var result = await _nonConformityService.GetByStatusAsync(auditId, status);
        return Ok(result);
    }

    [HttpGet("non-conformities/open")]
    public async Task<IActionResult> GetOpenNonConformities()
    {
        var facilityId = GetFacilityId();
        var result = await _nonConformityService.GetOpenAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("non-conformities/overdue")]
    public async Task<IActionResult> GetOverdueNonConformities()
    {
        var facilityId = GetFacilityId();
        var result = await _nonConformityService.GetOverdueAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("non-conformities/severity/{severity}")]
    public async Task<IActionResult> GetNonConformitiesBySeverity(string severity)
    {
        var facilityId = GetFacilityId();
        var result = await _nonConformityService.GetBySeverityAsync(facilityId, severity);
        return Ok(result);
    }

    [HttpGet("non-conformities/staff/{staffId}")]
    public async Task<IActionResult> GetNonConformitiesByResponsibleStaff(long staffId)
    {
        var result = await _nonConformityService.GetByResponsibleStaffAsync(staffId);
        return Ok(result);
    }

    [HttpPut("non-conformities/{id}")]
    public async Task<IActionResult> UpdateNonConformity(long id, [FromBody] ComplianceNonConformityUpdateDTO request)
    {
        var result = await _nonConformityService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpPost("non-conformities/{id}/resolve")]
    public async Task<IActionResult> ResolveNonConformity(long id, [FromBody] string resolutionNotes)
    {
        var result = await _nonConformityService.ResolveAsync(id, resolutionNotes);
        return Ok(result);
    }

    [HttpPost("non-conformities/{id}/verify")]
    public async Task<IActionResult> VerifyResolution(long id, [FromBody] long verifiedByStaffId)
    {
        var result = await _nonConformityService.VerifyResolutionAsync(id, verifiedByStaffId);
        return Ok(result);
    }

    [HttpPost("non-conformities/{id}/reopen")]
    public async Task<IActionResult> ReopenNonConformity(long id, [FromBody] string reason)
    {
        var result = await _nonConformityService.ReopenAsync(id, reason);
        return Ok(result);
    }

    [HttpDelete("non-conformities/{id}")]
    public async Task<IActionResult> DeleteNonConformity(long id)
    {
        await _nonConformityService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("non-conformities/count/open")]
    public async Task<IActionResult> GetOpenNonConformityCount()
    {
        var facilityId = GetFacilityId();
        var count = await _nonConformityService.GetOpenCountAsync(facilityId);
        return Ok(new { openCount = count });
    }

    [HttpGet("non-conformities/count/overdue")]
    public async Task<IActionResult> GetOverdueNonConformityCount()
    {
        var facilityId = GetFacilityId();
        var count = await _nonConformityService.GetOverdueCountAsync(facilityId);
        return Ok(new { overdueCount = count });
    }

    [HttpGet("non-conformities/distribution/severity")]
    public async Task<IActionResult> GetNonConformityDistributionBySeverity()
    {
        var facilityId = GetFacilityId();
        var result = await _nonConformityService.GetDistributionBySeverityAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("non-conformities/distribution/status")]
    public async Task<IActionResult> GetNonConformityDistributionByStatus()
    {
        var facilityId = GetFacilityId();
        var result = await _nonConformityService.GetDistributionByStatusAsync(facilityId);
        return Ok(result);
    }

    #endregion

    #region Compliance Documents

    [HttpPost("documents")]
    public async Task<IActionResult> CreateDocument([FromBody] ComplianceDocumentCreateDTO request)
    {
        var result = await _documentService.CreateAsync(request);
        return CreatedAtAction(nameof(GetDocumentById), new { id = result.Id }, result);
    }

    [HttpGet("documents/{id}")]
    public async Task<IActionResult> GetDocumentById(long id)
    {
        var result = await _documentService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("audits/{auditId}/documents")]
    public async Task<IActionResult> GetDocumentsByAudit(long auditId)
    {
        var result = await _documentService.GetByAuditAsync(auditId);
        return Ok(result);
    }

    [HttpGet("non-conformities/{nonConformityId}/documents")]
    public async Task<IActionResult> GetDocumentsByNonConformity(long nonConformityId)
    {
        var result = await _documentService.GetByNonConformityAsync(nonConformityId);
        return Ok(result);
    }

    [HttpGet("documents/type/{documentType}")]
    public async Task<IActionResult> GetDocumentsByType(string documentType)
    {
        var result = await _documentService.GetByTypeAsync(documentType);
        return Ok(result);
    }

    [HttpGet("documents/access-level/{accessLevel}")]
    public async Task<IActionResult> GetDocumentsByAccessLevel(string accessLevel)
    {
        var result = await _documentService.GetByAccessLevelAsync(accessLevel);
        return Ok(result);
    }

    [HttpGet("documents/expired")]
    public async Task<IActionResult> GetExpiredDocuments()
    {
        var result = await _documentService.GetExpiredAsync();
        return Ok(result);
    }

    [HttpGet("documents/pending-review")]
    public async Task<IActionResult> GetPendingReviewDocuments()
    {
        var result = await _documentService.GetPendingReviewAsync();
        return Ok(result);
    }

    [HttpGet("documents/published")]
    public async Task<IActionResult> GetPublishedDocuments()
    {
        var result = await _documentService.GetPublishedAsync();
        return Ok(result);
    }

    [HttpGet("documents/search")]
    public async Task<IActionResult> SearchDocumentsByTags([FromQuery] string tags)
    {
        var result = await _documentService.SearchByTagsAsync(tags);
        return Ok(result);
    }

    [HttpPut("documents/{id}")]
    public async Task<IActionResult> UpdateDocument(long id, [FromBody] ComplianceDocumentUpdateDTO request)
    {
        var result = await _documentService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpPost("documents/{id}/publish")]
    public async Task<IActionResult> PublishDocument(long id)
    {
        var result = await _documentService.PublishAsync(id);
        return Ok(result);
    }

    [HttpPost("documents/{id}/unpublish")]
    public async Task<IActionResult> UnpublishDocument(long id)
    {
        var result = await _documentService.UnpublishAsync(id);
        return Ok(result);
    }

    [HttpPost("documents/{id}/archive")]
    public async Task<IActionResult> ArchiveDocument(long id)
    {
        var result = await _documentService.ArchiveAsync(id);
        return Ok(result);
    }

    [HttpPost("documents/{id}/review")]
    public async Task<IActionResult> ReviewDocument(long id, [FromBody] dynamic request)
    {
        var result = await _documentService.ReviewAsync(id, request.reviewedByStaffId, request.status, request.feedback);
        return Ok(result);
    }

    [HttpPost("documents/{id}/new-version")]
    public async Task<IActionResult> CreateDocumentNewVersion(long id, [FromBody] ComplianceDocumentCreateDTO request)
    {
        var result = await _documentService.CreateNewVersionAsync(id, request);
        return CreatedAtAction(nameof(GetDocumentById), new { id = result.Id }, result);
    }

    [HttpDelete("documents/{id}")]
    public async Task<IActionResult> DeleteDocument(long id)
    {
        await _documentService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("audits/{auditId}/documents/count")]
    public async Task<IActionResult> GetDocumentCount(long auditId)
    {
        var count = await _documentService.GetDocumentCountAsync(auditId);
        return Ok(new { documentCount = count });
    }

    [HttpGet("documents/{id}/version-history")]
    public async Task<IActionResult> GetDocumentVersionHistory(long id)
    {
        var result = await _documentService.GetVersionHistoryAsync(id);
        return Ok(result);
    }

    #endregion
}
