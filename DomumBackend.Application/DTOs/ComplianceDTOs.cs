namespace DomumBackend.Application.DTOs;

using System;
using System.Collections.Generic;

#region ComplianceAudit DTOs

public class ComplianceAuditDTO
{
    public long Id { get; set; }
    public string? FacilityId { get; set; } = string.Empty;
    public string? AuditType { get; set; } = string.Empty;
    public string? AuditName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? AuditStatus { get; set; } = string.Empty;
    public long? AssignedToStaffId { get; set; }
    public string? AssignedToStaffName { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public int Priority { get; set; }
    public string? Frequency { get; set; } = string.Empty;
    public DateTime? LastCompletedDate { get; set; }
    public DateTime? NextScheduledDate { get; set; }
    public int ComplianceScore { get; set; }
    public int TotalChecklistItems { get; set; }
    public int CompletedChecklistItems { get; set; }
    public string? RegulatoryBody { get; set; } = string.Empty;
    public string? ReferenceNumber { get; set; } = string.Empty;
    public string? Findings { get; set; } = string.Empty;
    public string? Recommendations { get; set; } = string.Empty;
    public bool RequiresFollowUp { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpStatus { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public decimal? ActualCost { get; set; }
    public string? DocumentReference { get; set; } = string.Empty;
    public bool IsRemote { get; set; }
    public string? AuditorName { get; set; } = string.Empty;
    public string? AuditorOrganization { get; set; } = string.Empty;
    public string? AuditorContactInfo { get; set; } = string.Empty;
    public string? NotesForFollowUp { get; set; } = string.Empty;
    public List<ComplianceChecklistItemDTO> ChecklistItems { get; set; } = new();
    public int NonConformityCount { get; set; } = 0;
    public int DocumentCount { get; set; } = 0;
}

public class ComplianceAuditCreateDTO
{
    public string? AuditType { get; set; } = string.Empty;
    public string? AuditName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public int Priority { get; set; } = 2;
    public string? Frequency { get; set; } = "Annual";
    public long? AssignedToStaffId { get; set; }
    public string? RegulatoryBody { get; set; } = string.Empty;
    public string? ReferenceNumber { get; set; } = string.Empty;
    public bool IsRemote { get; set; } = false;
    public string? AuditorName { get; set; } = string.Empty;
    public string? AuditorOrganization { get; set; } = string.Empty;
    public string? AuditorContactInfo { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
}

public class ComplianceAuditUpdateDTO
{
    public string? AuditName { get; set; }
    public string? Description { get; set; }
    public string? AuditStatus { get; set; }
    public long? AssignedToStaffId { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public int? Priority { get; set; }
    public string? Frequency { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpStatus { get; set; }
    public decimal? ActualCost { get; set; }
    public string? Findings { get; set; }
    public string? Recommendations { get; set; }
}

public class ComplianceAuditSummaryDTO
{
    public long Id { get; set; }
    public string? AuditType { get; set; } = string.Empty;
    public string? AuditName { get; set; } = string.Empty;
    public string? AuditStatus { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public int ComplianceScore { get; set; }
    public int Priority { get; set; }
    public int NonConformityCount { get; set; }
    public bool RequiresFollowUp { get; set; }
}

#endregion

#region ComplianceChecklistItem DTOs

public class ComplianceChecklistItemDTO
{
    public long Id { get; set; }
    public long ComplianceAuditId { get; set; }
    public string? ItemName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int ItemNumber { get; set; }
    public string? Category { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public bool IsCompliant { get; set; }
    public string? EvidenceProvided { get; set; } = string.Empty;
    public DateTime? ReviewedDate { get; set; }
    public long? ReviewedByStaffId { get; set; }
    public string? ReviewedByStaffName { get; set; } = string.Empty;
    public string? ReviewerNotes { get; set; } = string.Empty;
    public int Weight { get; set; }
    public string? Requirement { get; set; } = string.Empty;
    public string? Guidance { get; set; } = string.Empty;
    public bool RequiresEvidenceDocument { get; set; }
    public string? DocumentReference { get; set; } = string.Empty;
    public string? NonComplianceReason { get; set; } = string.Empty;
    public string? CorrectionAction { get; set; } = string.Empty;
    public DateTime? CorrectionDueDate { get; set; }
    public DateTime? CorrectionCompletedDate { get; set; }
    public string? RiskLevel { get; set; } = string.Empty;
}

public class ComplianceChecklistItemCreateDTO
{
    public string? ItemName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int ItemNumber { get; set; }
    public string? Category { get; set; } = string.Empty;
    public int Weight { get; set; } = 1;
    public string? Requirement { get; set; } = string.Empty;
    public string? Guidance { get; set; } = string.Empty;
    public bool RequiresEvidenceDocument { get; set; } = false;
    public string? RiskLevel { get; set; } = "Medium";
}

public class ComplianceChecklistItemUpdateDTO
{
    public string? Status { get; set; }
    public bool? IsCompliant { get; set; }
    public string? EvidenceProvided { get; set; }
    public long? ReviewedByStaffId { get; set; }
    public string? ReviewerNotes { get; set; }
    public string? NonComplianceReason { get; set; }
    public string? CorrectionAction { get; set; }
    public DateTime? CorrectionDueDate { get; set; }
    public DateTime? CorrectionCompletedDate { get; set; }
}

#endregion

#region ComplianceNonConformity DTOs

public class ComplianceNonConformityDTO
{
    public long Id { get; set; }
    public long ComplianceAuditId { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? Category { get; set; } = string.Empty;
    public string? Severity { get; set; } = string.Empty;
    public string? RootCause { get; set; } = string.Empty;
    public string? CorrectionAction { get; set; } = string.Empty;
    public long? ResponsibleStaffId { get; set; }
    public string? ResponsibleStaffName { get; set; } = string.Empty;
    public DateTime IdentifiedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? Status { get; set; } = string.Empty;
    public string? Evidence { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool RequiresFollowUp { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpFindings { get; set; } = string.Empty;
    public string? RegulatoryReference { get; set; } = string.Empty;
    public decimal? CostToResolve { get; set; }
    public string? ResolutionNotes { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public long? VerifiedByStaffId { get; set; }
    public string? VerifiedByStaffName { get; set; } = string.Empty;
    public DateTime? VerificationDate { get; set; }
}

public class ComplianceNonConformityCreateDTO
{
    public string? Description { get; set; } = string.Empty;
    public string? Category { get; set; } = string.Empty;
    public string? Severity { get; set; } = "Medium";
    public string? RootCause { get; set; } = string.Empty;
    public string? CorrectionAction { get; set; } = string.Empty;
    public long? ResponsibleStaffId { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; } = 2;
    public string? RegulatoryReference { get; set; } = string.Empty;
}

public class ComplianceNonConformityUpdateDTO
{
    public string? Status { get; set; }
    public string? RootCause { get; set; }
    public string? CorrectionAction { get; set; }
    public long? ResponsibleStaffId { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Evidence { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? FollowUpFindings { get; set; }
}

#endregion

#region ComplianceDocument DTOs

public class ComplianceDocumentDTO
{
    public long Id { get; set; }
    public long ComplianceAuditId { get; set; }
    public long? NonConformityId { get; set; }
    public string? DocumentName { get; set; } = string.Empty;
    public string? DocumentType { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? DocumentPath { get; set; } = string.Empty;
    public string? FileType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public DateTime UploadedDate { get; set; }
    public string? UploadedByUserId { get; set; } = string.Empty;
    public DateTime? ReviewedDate { get; set; }
    public long? ReviewedByStaffId { get; set; }
    public string? ReviewStatus { get; set; } = string.Empty;
    public string? ReviewFeedback { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
    public bool IsCurrentVersion { get; set; }
    public int VersionNumber { get; set; }
    public string? DocumentReference { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? AccessLevel { get; set; } = string.Empty;
    public string? Tags { get; set; } = string.Empty;
}

public class ComplianceDocumentCreateDTO
{
    public long ComplianceAuditId { get; set; }
    public long? NonConformityId { get; set; }
    public string? DocumentName { get; set; } = string.Empty;
    public string? DocumentType { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? DocumentPath { get; set; } = string.Empty;
    public string? FileType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? AccessLevel { get; set; } = "Internal";
    public string? Tags { get; set; } = string.Empty;
}

public class ComplianceDocumentUpdateDTO
{
    public string? DocumentName { get; set; }
    public string? Description { get; set; }
    public string? ReviewStatus { get; set; }
    public string? ReviewFeedback { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool? IsPublished { get; set; }
    public string? AccessLevel { get; set; }
    public string? Tags { get; set; }
}

#endregion

