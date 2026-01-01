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

#region ComplianceAuditService

public class ComplianceAuditService : IComplianceAuditService
{
    private readonly ApplicationDbContext _context;

    public ComplianceAuditService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ComplianceAuditDTO> CreateAsync(string facilityId, ComplianceAuditCreateDTO request)
    {
        var audit = new ComplianceAudit
        {
            FacilityId = facilityId,
            AuditType = request.AuditType,
            AuditName = request.AuditName,
            Description = request.Description,
            ScheduledDate = request.ScheduledDate,
            Priority = request.Priority,
            Frequency = request.Frequency,
            AssignedToStaffId = request.AssignedToStaffId,
            RegulatoryBody = request.RegulatoryBody,
            ReferenceNumber = request.ReferenceNumber,
            IsRemote = request.IsRemote,
            AuditorName = request.AuditorName,
            AuditorOrganization = request.AuditorOrganization,
            AuditorContactInfo = request.AuditorContactInfo,
            Budget = request.Budget,
            AuditStatus = "Pending",
            CreatedDate = DateTime.UtcNow
        };

        _context.ComplianceAudits.Add(audit);
        await _context.SaveChangesAsync();
        return MapToDTO(audit);
    }

    public async Task<ComplianceAuditDTO?> GetByIdAsync(long id)
    {
        var audit = await _context.ComplianceAudits
            .Include(a => a.ChecklistItems)
            .FirstOrDefaultAsync(a => a.Id == id);
        return audit == null ? null : MapToDTO(audit);
    }

    public async Task<List<ComplianceAuditDTO>> GetAllAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetByTypeAsync(string facilityId, string auditType)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && a.AuditType == auditType)
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetByStatusAsync(string facilityId, string status)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && a.AuditStatus == status)
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetOverdueAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && a.ScheduledDate < DateTime.UtcNow && a.AuditStatus != "Completed")
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetScheduledForDateRangeAsync(string facilityId, DateTime startDate, DateTime endDate)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && a.ScheduledDate >= startDate && a.ScheduledDate <= endDate)
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetByAssignedStaffAsync(long staffId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.AssignedToStaffId == staffId)
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetByPriorityAsync(string facilityId, int priority)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && a.Priority == priority)
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> GetRequiringFollowUpAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && a.RequiresFollowUp && a.FollowUpStatus != "Completed")
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceAuditDTO>> SearchAsync(string facilityId, string searchTerm)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && 
                (a.AuditName!.Contains(searchTerm) || 
                 (a.Description ?? "").Contains(searchTerm) ||
                 a.AuditType!.Contains(searchTerm)))
            .Include(a => a.ChecklistItems)
            .ToListAsync();
        return audits.Select(MapToDTO).ToList();
    }

    public async Task<ComplianceAuditDTO> UpdateAsync(long id, ComplianceAuditUpdateDTO request)
    {
        var audit = await _context.ComplianceAudits.FindAsync(id);
        if (audit == null) throw new InvalidOperationException($"Audit {id} not found");

        if (!string.IsNullOrEmpty(request.AuditName)) audit.AuditName = request.AuditName;
        if (!string.IsNullOrEmpty(request.Description)) audit.Description = request.Description;
        if (!string.IsNullOrEmpty(request.AuditStatus)) audit.AuditStatus = request.AuditStatus;
        if (request.AssignedToStaffId.HasValue) audit.AssignedToStaffId = request.AssignedToStaffId;
        if (request.ScheduledDate.HasValue) audit.ScheduledDate = request.ScheduledDate.Value;
        if (request.ActualStartDate.HasValue) audit.ActualStartDate = request.ActualStartDate;
        if (request.CompletionDate.HasValue) audit.CompletionDate = request.CompletionDate;
        if (request.Priority.HasValue) audit.Priority = request.Priority.Value;
        if (!string.IsNullOrEmpty(request.Frequency)) audit.Frequency = request.Frequency;
        if (request.FollowUpDate.HasValue) audit.FollowUpDate = request.FollowUpDate;
        if (!string.IsNullOrEmpty(request.FollowUpStatus)) audit.FollowUpStatus = request.FollowUpStatus;
        if (request.ActualCost.HasValue) audit.ActualCost = request.ActualCost;
        if (!string.IsNullOrEmpty(request.Findings)) audit.Findings = request.Findings;
        if (!string.IsNullOrEmpty(request.Recommendations)) audit.Recommendations = request.Recommendations;

        audit.ModifiedDate = DateTime.UtcNow;
        _context.ComplianceAudits.Update(audit);
        await _context.SaveChangesAsync();
        return MapToDTO(audit);
    }

    public async Task<ComplianceAuditDTO> StartAuditAsync(long id)
    {
        var audit = await _context.ComplianceAudits.FindAsync(id);
        if (audit == null) throw new InvalidOperationException($"Audit {id} not found");

        audit.AuditStatus = "InProgress";
        audit.ActualStartDate = DateTime.UtcNow;
        audit.ModifiedDate = DateTime.UtcNow;
        _context.ComplianceAudits.Update(audit);
        await _context.SaveChangesAsync();
        return MapToDTO(audit);
    }

    public async Task<ComplianceAuditDTO> CompleteAuditAsync(long id, string findings)
    {
        var audit = await _context.ComplianceAudits.FindAsync(id);
        if (audit == null) throw new InvalidOperationException($"Audit {id} not found");

        audit.AuditStatus = "Completed";
        audit.CompletionDate = DateTime.UtcNow;
        audit.Findings = findings;
        audit.LastCompletedDate = DateTime.UtcNow;
        audit.ModifiedDate = DateTime.UtcNow;
        _context.ComplianceAudits.Update(audit);
        await _context.SaveChangesAsync();
        return MapToDTO(audit);
    }

    public async Task<ComplianceAuditDTO> RecordFollowUpAsync(long id, string findings)
    {
        var audit = await _context.ComplianceAudits.FindAsync(id);
        if (audit == null) throw new InvalidOperationException($"Audit {id} not found");

        audit.FollowUpStatus = "Completed";
        audit.FollowUpFindings = findings;
        audit.ModifiedDate = DateTime.UtcNow;
        _context.ComplianceAudits.Update(audit);
        await _context.SaveChangesAsync();
        return MapToDTO(audit);
    }

    public async Task DeleteAsync(long id)
    {
        var audit = await _context.ComplianceAudits.FindAsync(id);
        if (audit != null)
        {
            _context.ComplianceAudits.Remove(audit);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetComplianceScoreAsync(long auditId)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId)
            .ToListAsync();

        if (items.Count == 0) return 0;

        var totalWeight = items.Sum(i => i.Weight);
        var compliantWeight = items.Where(i => i.IsCompliant).Sum(i => i.Weight);

        return totalWeight > 0 ? (compliantWeight * 100) / totalWeight : 0;
    }

    public async Task<List<ComplianceAuditSummaryDTO>> GetSummaryAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Include(a => a.ChecklistItems)
            .ToListAsync();

        return audits.Select(a => new ComplianceAuditSummaryDTO
        {
            Id = a.Id,
            AuditType = a.AuditType,
            AuditName = a.AuditName,
            AuditStatus = a.AuditStatus,
            ScheduledDate = a.ScheduledDate,
            CompletionDate = a.CompletionDate,
            ComplianceScore = a.ComplianceScore,
            Priority = a.Priority,
            NonConformityCount = 0,
            RequiresFollowUp = a.RequiresFollowUp
        }).ToList();
    }

    public async Task<Dictionary<string, int>> GetAuditDistributionByTypeAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .GroupBy(a => a.AuditType)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToListAsync();

        return audits.ToDictionary(x => x.Type ?? "Unknown", x => x.Count);
    }

    public async Task<Dictionary<string, int>> GetAuditDistributionByStatusAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .GroupBy(a => a.AuditStatus)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync();

        return audits.ToDictionary(x => x.Status ?? "Unknown", x => x.Count);
    }

    public async Task<List<ComplianceAuditDTO>> GetDueAuditsAsync(string facilityId, int daysThreshold)
    {
        var dueDate = DateTime.UtcNow.AddDays(daysThreshold);
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId && 
                   a.ScheduledDate <= dueDate && 
                   a.AuditStatus != "Completed")
            .Include(a => a.ChecklistItems)
            .ToListAsync();

        return audits.Select(MapToDTO).ToList();
    }

    private ComplianceAuditDTO MapToDTO(ComplianceAudit audit) => new()
    {
        Id = audit.Id,
        FacilityId = audit.FacilityId,
        AuditType = audit.AuditType,
        AuditName = audit.AuditName,
        Description = audit.Description,
        AuditStatus = audit.AuditStatus,
        AssignedToStaffId = audit.AssignedToStaffId,
        ScheduledDate = audit.ScheduledDate,
        ActualStartDate = audit.ActualStartDate,
        CompletionDate = audit.CompletionDate,
        Priority = audit.Priority,
        Frequency = audit.Frequency,
        LastCompletedDate = audit.LastCompletedDate,
        NextScheduledDate = audit.NextScheduledDate,
        ComplianceScore = audit.ComplianceScore,
        TotalChecklistItems = audit.TotalChecklistItems,
        CompletedChecklistItems = audit.CompletedChecklistItems,
        RegulatoryBody = audit.RegulatoryBody,
        ReferenceNumber = audit.ReferenceNumber,
        Findings = audit.Findings,
        Recommendations = audit.Recommendations,
        RequiresFollowUp = audit.RequiresFollowUp,
        FollowUpDate = audit.FollowUpDate,
        FollowUpStatus = audit.FollowUpStatus,
        Budget = audit.Budget,
        ActualCost = audit.ActualCost,
        DocumentReference = audit.DocumentReference,
        IsRemote = audit.IsRemote,
        AuditorName = audit.AuditorName,
        AuditorOrganization = audit.AuditorOrganization,
        AuditorContactInfo = audit.AuditorContactInfo,
        NotesForFollowUp = audit.NotesForFollowUp
    };
}

#endregion

#region ComplianceChecklistService

public class ComplianceChecklistService : IComplianceChecklistService
{
    private readonly ApplicationDbContext _context;

    public ComplianceChecklistService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ComplianceChecklistItemDTO> CreateAsync(long auditId, ComplianceChecklistItemCreateDTO request)
    {
        var item = new ComplianceChecklistItem
        {
            ComplianceAuditId = auditId,
            ItemName = request.ItemName,
            Description = request.Description,
            ItemNumber = request.ItemNumber,
            Category = request.Category,
            Weight = request.Weight,
            Requirement = request.Requirement,
            Guidance = request.Guidance,
            RequiresEvidenceDocument = request.RequiresEvidenceDocument,
            RiskLevel = request.RiskLevel,
            Status = "NotStarted"
        };

        _context.ComplianceChecklistItems.Add(item);
        await _context.SaveChangesAsync();
        return MapToDTO(item);
    }

    public async Task<ComplianceChecklistItemDTO?> GetByIdAsync(long id)
    {
        var item = await _context.ComplianceChecklistItems.FindAsync(id);
        return item == null ? null : MapToDTO(item);
    }

    public async Task<List<ComplianceChecklistItemDTO>> GetByAuditAsync(long auditId)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId)
            .ToListAsync();
        return items.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceChecklistItemDTO>> GetByStatusAsync(long auditId, string status)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId && i.Status == status)
            .ToListAsync();
        return items.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceChecklistItemDTO>> GetNonCompliantAsync(long auditId)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId && !i.IsCompliant)
            .ToListAsync();
        return items.Select(MapToDTO).ToList();
    }

    public async Task<ComplianceChecklistItemDTO> UpdateAsync(long id, ComplianceChecklistItemUpdateDTO request)
    {
        var item = await _context.ComplianceChecklistItems.FindAsync(id);
        if (item == null) throw new InvalidOperationException($"Checklist item {id} not found");

        if (!string.IsNullOrEmpty(request.Status)) item.Status = request.Status;
        if (request.IsCompliant.HasValue) item.IsCompliant = request.IsCompliant.Value;
        if (!string.IsNullOrEmpty(request.EvidenceProvided)) item.EvidenceProvided = request.EvidenceProvided;
        if (request.ReviewedByStaffId.HasValue) item.ReviewedByStaffId = request.ReviewedByStaffId;
        if (!string.IsNullOrEmpty(request.ReviewerNotes)) item.ReviewerNotes = request.ReviewerNotes;
        if (!string.IsNullOrEmpty(request.NonComplianceReason)) item.NonComplianceReason = request.NonComplianceReason;
        if (!string.IsNullOrEmpty(request.CorrectionAction)) item.CorrectionAction = request.CorrectionAction;
        if (request.CorrectionDueDate.HasValue) item.CorrectionDueDate = request.CorrectionDueDate;
        if (request.CorrectionCompletedDate.HasValue) item.CorrectionCompletedDate = request.CorrectionCompletedDate;

        _context.ComplianceChecklistItems.Update(item);
        await _context.SaveChangesAsync();
        return MapToDTO(item);
    }

    public async Task<ComplianceChecklistItemDTO> ReviewAsync(long id, long reviewedByStaffId, string notes)
    {
        var item = await _context.ComplianceChecklistItems.FindAsync(id);
        if (item == null) throw new InvalidOperationException($"Checklist item {id} not found");

        item.ReviewedDate = DateTime.UtcNow;
        item.ReviewedByStaffId = reviewedByStaffId;
        item.ReviewerNotes = notes;
        item.Status = "Completed";

        _context.ComplianceChecklistItems.Update(item);
        await _context.SaveChangesAsync();
        return MapToDTO(item);
    }

    public async Task<ComplianceChecklistItemDTO> MarkCompliantAsync(long id, string evidence)
    {
        var item = await _context.ComplianceChecklistItems.FindAsync(id);
        if (item == null) throw new InvalidOperationException($"Checklist item {id} not found");

        item.IsCompliant = true;
        item.Status = "Completed";
        item.EvidenceProvided = evidence;

        _context.ComplianceChecklistItems.Update(item);
        await _context.SaveChangesAsync();
        return MapToDTO(item);
    }

    public async Task<ComplianceChecklistItemDTO> MarkNonCompliantAsync(long id, string reason, string correctionAction, DateTime dueDate)
    {
        var item = await _context.ComplianceChecklistItems.FindAsync(id);
        if (item == null) throw new InvalidOperationException($"Checklist item {id} not found");

        item.IsCompliant = false;
        item.Status = "Failed";
        item.NonComplianceReason = reason;
        item.CorrectionAction = correctionAction;
        item.CorrectionDueDate = dueDate;

        _context.ComplianceChecklistItems.Update(item);
        await _context.SaveChangesAsync();
        return MapToDTO(item);
    }

    public async Task DeleteAsync(long id)
    {
        var item = await _context.ComplianceChecklistItems.FindAsync(id);
        if (item != null)
        {
            _context.ComplianceChecklistItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetCompliancePercentageAsync(long auditId)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId)
            .ToListAsync();

        if (items.Count == 0) return 0;
        var compliantCount = items.Count(i => i.IsCompliant);
        return (compliantCount * 100) / items.Count;
    }

    public async Task<List<ComplianceChecklistItemDTO>> GetByCategoryAsync(long auditId, string category)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId && i.Category == category)
            .ToListAsync();
        return items.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceChecklistItemDTO>> GetRequiringEvidenceAsync(long auditId)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId && i.RequiresEvidenceDocument && string.IsNullOrEmpty(i.EvidenceProvided))
            .ToListAsync();
        return items.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceChecklistItemDTO>> GetOverdueCorrectionAsync(long auditId)
    {
        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId && 
                   i.CorrectionDueDate < DateTime.UtcNow && 
                   i.CorrectionCompletedDate == null)
            .ToListAsync();
        return items.Select(MapToDTO).ToList();
    }

    public async Task UpdateChecklistComplianceScoreAsync(long auditId)
    {
        var audit = await _context.ComplianceAudits.FindAsync(auditId);
        if (audit == null) return;

        var items = await _context.ComplianceChecklistItems
            .Where(i => i.ComplianceAuditId == auditId)
            .ToListAsync();

        audit.TotalChecklistItems = items.Count;
        audit.CompletedChecklistItems = items.Count(i => i.Status == "Completed");

        if (items.Count > 0)
        {
            var totalWeight = items.Sum(i => i.Weight);
            var compliantWeight = items.Where(i => i.IsCompliant).Sum(i => i.Weight);
            audit.ComplianceScore = totalWeight > 0 ? (compliantWeight * 100) / totalWeight : 0;
        }

        _context.ComplianceAudits.Update(audit);
        await _context.SaveChangesAsync();
    }

    private ComplianceChecklistItemDTO MapToDTO(ComplianceChecklistItem item) => new()
    {
        Id = item.Id,
        ComplianceAuditId = item.ComplianceAuditId,
        ItemName = item.ItemName,
        Description = item.Description,
        ItemNumber = item.ItemNumber,
        Category = item.Category,
        Status = item.Status,
        IsCompliant = item.IsCompliant,
        EvidenceProvided = item.EvidenceProvided,
        ReviewedDate = item.ReviewedDate,
        ReviewedByStaffId = item.ReviewedByStaffId,
        ReviewerNotes = item.ReviewerNotes,
        Weight = item.Weight,
        Requirement = item.Requirement,
        Guidance = item.Guidance,
        RequiresEvidenceDocument = item.RequiresEvidenceDocument,
        DocumentReference = item.DocumentReference,
        NonComplianceReason = item.NonComplianceReason,
        CorrectionAction = item.CorrectionAction,
        CorrectionDueDate = item.CorrectionDueDate,
        CorrectionCompletedDate = item.CorrectionCompletedDate,
        RiskLevel = item.RiskLevel
    };
}

#endregion

#region ComplianceNonConformityService

public class ComplianceNonConformityService : IComplianceNonConformityService
{
    private readonly ApplicationDbContext _context;

    public ComplianceNonConformityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ComplianceNonConformityDTO> CreateAsync(long auditId, ComplianceNonConformityCreateDTO request)
    {
        var nonConformity = new ComplianceNonConformity
        {
            ComplianceAuditId = auditId,
            Description = request.Description,
            Category = request.Category,
            Severity = request.Severity,
            RootCause = request.RootCause,
            CorrectionAction = request.CorrectionAction,
            ResponsibleStaffId = request.ResponsibleStaffId,
            DueDate = request.DueDate,
            Priority = request.Priority,
            RegulatoryReference = request.RegulatoryReference,
            Status = "Open",
            IdentifiedDate = DateTime.UtcNow
        };

        _context.ComplianceNonConformities.Add(nonConformity);
        await _context.SaveChangesAsync();
        return MapToDTO(nonConformity);
    }

    public async Task<ComplianceNonConformityDTO?> GetByIdAsync(long id)
    {
        var nonConformity = await _context.ComplianceNonConformities.FindAsync(id);
        return nonConformity == null ? null : MapToDTO(nonConformity);
    }

    public async Task<List<ComplianceNonConformityDTO>> GetByAuditAsync(long auditId)
    {
        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => n.ComplianceAuditId == auditId)
            .ToListAsync();
        return nonConformities.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceNonConformityDTO>> GetByStatusAsync(long auditId, string status)
    {
        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => n.ComplianceAuditId == auditId && n.Status == status)
            .ToListAsync();
        return nonConformities.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceNonConformityDTO>> GetOpenAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId) && n.Status == "Open")
            .ToListAsync();
        return nonConformities.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceNonConformityDTO>> GetBySeverityAsync(string facilityId, string severity)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId) && n.Severity == severity)
            .ToListAsync();
        return nonConformities.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceNonConformityDTO>> GetOverdueAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId) &&
                   n.DueDate < DateTime.UtcNow &&
                   n.Status != "Resolved")
            .ToListAsync();
        return nonConformities.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceNonConformityDTO>> GetByResponsibleStaffAsync(long staffId)
    {
        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => n.ResponsibleStaffId == staffId)
            .ToListAsync();
        return nonConformities.Select(MapToDTO).ToList();
    }

    public async Task<ComplianceNonConformityDTO> UpdateAsync(long id, ComplianceNonConformityUpdateDTO request)
    {
        var nonConformity = await _context.ComplianceNonConformities.FindAsync(id);
        if (nonConformity == null) throw new InvalidOperationException($"Non-conformity {id} not found");

        if (!string.IsNullOrEmpty(request.Status)) nonConformity.Status = request.Status;
        if (!string.IsNullOrEmpty(request.RootCause)) nonConformity.RootCause = request.RootCause;
        if (!string.IsNullOrEmpty(request.CorrectionAction)) nonConformity.CorrectionAction = request.CorrectionAction;
        if (request.ResponsibleStaffId.HasValue) nonConformity.ResponsibleStaffId = request.ResponsibleStaffId;
        if (request.DueDate.HasValue) nonConformity.DueDate = request.DueDate;
        if (!string.IsNullOrEmpty(request.Evidence)) nonConformity.Evidence = request.Evidence;
        if (request.CompletedDate.HasValue) nonConformity.CompletedDate = request.CompletedDate;
        if (!string.IsNullOrEmpty(request.FollowUpFindings)) nonConformity.FollowUpFindings = request.FollowUpFindings;

        _context.ComplianceNonConformities.Update(nonConformity);
        await _context.SaveChangesAsync();
        return MapToDTO(nonConformity);
    }

    public async Task<ComplianceNonConformityDTO> ResolveAsync(long id, string resolutionNotes)
    {
        var nonConformity = await _context.ComplianceNonConformities.FindAsync(id);
        if (nonConformity == null) throw new InvalidOperationException($"Non-conformity {id} not found");

        nonConformity.Status = "Resolved";
        nonConformity.CompletedDate = DateTime.UtcNow;
        nonConformity.ResolutionNotes = resolutionNotes;

        _context.ComplianceNonConformities.Update(nonConformity);
        await _context.SaveChangesAsync();
        return MapToDTO(nonConformity);
    }

    public async Task<ComplianceNonConformityDTO> VerifyResolutionAsync(long id, long verifiedByStaffId)
    {
        var nonConformity = await _context.ComplianceNonConformities.FindAsync(id);
        if (nonConformity == null) throw new InvalidOperationException($"Non-conformity {id} not found");

        nonConformity.IsVerified = true;
        nonConformity.VerifiedByStaffId = verifiedByStaffId;
        nonConformity.VerificationDate = DateTime.UtcNow;
        nonConformity.Status = "Closed";

        _context.ComplianceNonConformities.Update(nonConformity);
        await _context.SaveChangesAsync();
        return MapToDTO(nonConformity);
    }

    public async Task<ComplianceNonConformityDTO> ReopenAsync(long id, string reason)
    {
        var nonConformity = await _context.ComplianceNonConformities.FindAsync(id);
        if (nonConformity == null) throw new InvalidOperationException($"Non-conformity {id} not found");

        nonConformity.Status = "Reopened";
        nonConformity.CompletedDate = null;
        nonConformity.IsVerified = false;

        _context.ComplianceNonConformities.Update(nonConformity);
        await _context.SaveChangesAsync();
        return MapToDTO(nonConformity);
    }

    public async Task DeleteAsync(long id)
    {
        var nonConformity = await _context.ComplianceNonConformities.FindAsync(id);
        if (nonConformity != null)
        {
            _context.ComplianceNonConformities.Remove(nonConformity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetOpenCountAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        return await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId) && n.Status == "Open")
            .CountAsync();
    }

    public async Task<int> GetOverdueCountAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        return await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId) && 
                   n.DueDate < DateTime.UtcNow &&
                   n.Status != "Resolved")
            .CountAsync();
    }

    public async Task<Dictionary<string, int>> GetDistributionBySeverityAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId))
            .GroupBy(n => n.Severity)
            .Select(g => new { Severity = g.Key, Count = g.Count() })
            .ToListAsync();

        return nonConformities.ToDictionary(x => x.Severity ?? "Unknown", x => x.Count);
    }

    public async Task<Dictionary<string, int>> GetDistributionByStatusAsync(string facilityId)
    {
        var audits = await _context.ComplianceAudits
            .Where(a => a.FacilityId == facilityId)
            .Select(a => a.Id)
            .ToListAsync();

        var nonConformities = await _context.ComplianceNonConformities
            .Where(n => audits.Contains(n.ComplianceAuditId))
            .GroupBy(n => n.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync();

        return nonConformities.ToDictionary(x => x.Status ?? "Unknown", x => x.Count);
    }

    private ComplianceNonConformityDTO MapToDTO(ComplianceNonConformity nc) => new()
    {
        Id = nc.Id,
        ComplianceAuditId = nc.ComplianceAuditId,
        Description = nc.Description,
        Category = nc.Category,
        Severity = nc.Severity,
        RootCause = nc.RootCause,
        CorrectionAction = nc.CorrectionAction,
        ResponsibleStaffId = nc.ResponsibleStaffId,
        IdentifiedDate = nc.IdentifiedDate,
        DueDate = nc.DueDate,
        CompletedDate = nc.CompletedDate,
        Status = nc.Status,
        Evidence = nc.Evidence,
        Priority = nc.Priority,
        RequiresFollowUp = nc.RequiresFollowUp,
        FollowUpDate = nc.FollowUpDate,
        FollowUpFindings = nc.FollowUpFindings,
        RegulatoryReference = nc.RegulatoryReference,
        CostToResolve = nc.CostToResolve,
        ResolutionNotes = nc.ResolutionNotes,
        IsVerified = nc.IsVerified,
        VerifiedByStaffId = nc.VerifiedByStaffId,
        VerificationDate = nc.VerificationDate
    };
}

#endregion

#region ComplianceDocumentService

public class ComplianceDocumentService : IComplianceDocumentService
{
    private readonly ApplicationDbContext _context;

    public ComplianceDocumentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ComplianceDocumentDTO> CreateAsync(ComplianceDocumentCreateDTO request)
    {
        var document = new ComplianceDocument
        {
            ComplianceAuditId = request.ComplianceAuditId,
            NonConformityId = request.NonConformityId,
            DocumentName = request.DocumentName,
            DocumentType = request.DocumentType,
            Description = request.Description,
            DocumentPath = request.DocumentPath,
            FileType = request.FileType,
            FileSizeBytes = request.FileSizeBytes,
            ExpiryDate = request.ExpiryDate,
            AccessLevel = request.AccessLevel,
            Tags = request.Tags,
            UploadedDate = DateTime.UtcNow,
            ReviewStatus = "Pending",
            IsCurrentVersion = true,
            VersionNumber = 1
        };

        _context.ComplianceDocuments.Add(document);
        await _context.SaveChangesAsync();
        return MapToDTO(document);
    }

    public async Task<ComplianceDocumentDTO?> GetByIdAsync(long id)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        return document == null ? null : MapToDTO(document);
    }

    public async Task<List<ComplianceDocumentDTO>> GetByAuditAsync(long auditId)
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.ComplianceAuditId == auditId)
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> GetByNonConformityAsync(long nonConformityId)
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.NonConformityId == nonConformityId)
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> GetByTypeAsync(string documentType)
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.DocumentType == documentType)
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> GetByAccessLevelAsync(string accessLevel)
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.AccessLevel == accessLevel)
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> GetExpiredAsync()
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.ExpiryDate < DateTime.UtcNow)
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> GetPendingReviewAsync()
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.ReviewStatus == "Pending")
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> GetPublishedAsync()
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.IsPublished)
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<List<ComplianceDocumentDTO>> SearchByTagsAsync(string tags)
    {
        var documents = await _context.ComplianceDocuments
            .Where(d => d.Tags != null && d.Tags.Contains(tags))
            .ToListAsync();
        return documents.Select(MapToDTO).ToList();
    }

    public async Task<ComplianceDocumentDTO> UpdateAsync(long id, ComplianceDocumentUpdateDTO request)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        if (document == null) throw new InvalidOperationException($"Document {id} not found");

        if (!string.IsNullOrEmpty(request.DocumentName)) document.DocumentName = request.DocumentName;
        if (!string.IsNullOrEmpty(request.Description)) document.Description = request.Description;
        if (!string.IsNullOrEmpty(request.ReviewStatus)) document.ReviewStatus = request.ReviewStatus;
        if (!string.IsNullOrEmpty(request.ReviewFeedback)) document.ReviewFeedback = request.ReviewFeedback;
        if (request.ExpiryDate.HasValue) document.ExpiryDate = request.ExpiryDate;
        if (request.IsPublished.HasValue) document.IsPublished = request.IsPublished.Value;
        if (!string.IsNullOrEmpty(request.AccessLevel)) document.AccessLevel = request.AccessLevel;
        if (!string.IsNullOrEmpty(request.Tags)) document.Tags = request.Tags;

        _context.ComplianceDocuments.Update(document);
        await _context.SaveChangesAsync();
        return MapToDTO(document);
    }

    public async Task<ComplianceDocumentDTO> PublishAsync(long id)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        if (document == null) throw new InvalidOperationException($"Document {id} not found");

        document.IsPublished = true;
        document.PublishedDate = DateTime.UtcNow;

        _context.ComplianceDocuments.Update(document);
        await _context.SaveChangesAsync();
        return MapToDTO(document);
    }

    public async Task<ComplianceDocumentDTO> UnpublishAsync(long id)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        if (document == null) throw new InvalidOperationException($"Document {id} not found");

        document.IsPublished = false;

        _context.ComplianceDocuments.Update(document);
        await _context.SaveChangesAsync();
        return MapToDTO(document);
    }

    public async Task<ComplianceDocumentDTO> ArchiveAsync(long id)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        if (document == null) throw new InvalidOperationException($"Document {id} not found");

        document.IsCurrentVersion = false;

        _context.ComplianceDocuments.Update(document);
        await _context.SaveChangesAsync();
        return MapToDTO(document);
    }

    public async Task<ComplianceDocumentDTO> ReviewAsync(long id, long reviewedByStaffId, string status, string feedback)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        if (document == null) throw new InvalidOperationException($"Document {id} not found");

        document.ReviewStatus = status;
        document.ReviewFeedback = feedback;
        document.ReviewedDate = DateTime.UtcNow;
        document.ReviewedByStaffId = reviewedByStaffId;

        _context.ComplianceDocuments.Update(document);
        await _context.SaveChangesAsync();
        return MapToDTO(document);
    }

    public async Task<ComplianceDocumentDTO> CreateNewVersionAsync(long originalDocumentId, ComplianceDocumentCreateDTO request)
    {
        var originalDocument = await _context.ComplianceDocuments.FindAsync(originalDocumentId);
        if (originalDocument == null) throw new InvalidOperationException($"Original document {originalDocumentId} not found");

        originalDocument.IsCurrentVersion = false;
        _context.ComplianceDocuments.Update(originalDocument);

        var newVersion = new ComplianceDocument
        {
            ComplianceAuditId = originalDocument.ComplianceAuditId,
            NonConformityId = originalDocument.NonConformityId,
            DocumentName = request.DocumentName,
            DocumentType = request.DocumentType,
            Description = request.Description,
            DocumentPath = request.DocumentPath,
            FileType = request.FileType,
            FileSizeBytes = request.FileSizeBytes,
            ExpiryDate = request.ExpiryDate,
            AccessLevel = request.AccessLevel,
            Tags = request.Tags,
            UploadedDate = DateTime.UtcNow,
            ReviewStatus = "Pending",
            IsCurrentVersion = true,
            VersionNumber = originalDocument.VersionNumber + 1
        };

        _context.ComplianceDocuments.Add(newVersion);
        await _context.SaveChangesAsync();
        return MapToDTO(newVersion);
    }

    public async Task DeleteAsync(long id)
    {
        var document = await _context.ComplianceDocuments.FindAsync(id);
        if (document != null)
        {
            _context.ComplianceDocuments.Remove(document);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetDocumentCountAsync(long auditId)
    {
        return await _context.ComplianceDocuments
            .Where(d => d.ComplianceAuditId == auditId)
            .CountAsync();
    }

    public async Task<List<ComplianceDocumentDTO>> GetVersionHistoryAsync(long documentId)
    {
        var document = await _context.ComplianceDocuments.FindAsync(documentId);
        if (document == null) return new();

        var versions = await _context.ComplianceDocuments
            .Where(d => d.ComplianceAuditId == document.ComplianceAuditId && d.DocumentName == document.DocumentName)
            .OrderByDescending(d => d.VersionNumber)
            .ToListAsync();

        return versions.Select(MapToDTO).ToList();
    }

    private ComplianceDocumentDTO MapToDTO(ComplianceDocument doc) => new()
    {
        Id = doc.Id,
        ComplianceAuditId = doc.ComplianceAuditId,
        NonConformityId = doc.NonConformityId,
        DocumentName = doc.DocumentName,
        DocumentType = doc.DocumentType,
        Description = doc.Description,
        DocumentPath = doc.DocumentPath,
        FileType = doc.FileType,
        FileSizeBytes = doc.FileSizeBytes,
        UploadedDate = doc.UploadedDate,
        UploadedByUserId = doc.UploadedByUserId,
        ReviewedDate = doc.ReviewedDate,
        ReviewedByStaffId = doc.ReviewedByStaffId,
        ReviewStatus = doc.ReviewStatus,
        ReviewFeedback = doc.ReviewFeedback,
        ExpiryDate = doc.ExpiryDate,
        IsCurrentVersion = doc.IsCurrentVersion,
        VersionNumber = doc.VersionNumber,
        DocumentReference = doc.DocumentReference,
        IsPublished = doc.IsPublished,
        PublishedDate = doc.PublishedDate,
        AccessLevel = doc.AccessLevel,
        Tags = doc.Tags
    };
}

#endregion
