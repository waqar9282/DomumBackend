namespace DomumBackend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComplianceChecklistItems")]
public class ComplianceChecklistItem : BaseEntity
{
    public long ComplianceAuditId { get; set; }
    public string? ItemName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int ItemNumber { get; set; }
    public string? Category { get; set; } = string.Empty;
    public string? Status { get; set; } = "NotStarted"; // NotStarted, InProgress, Completed, Failed, NotApplicable
    public bool IsCompliant { get; set; } = false;
    public string? EvidenceProvided { get; set; } = string.Empty;
    public DateTime? ReviewedDate { get; set; }
    public long? ReviewedByStaffId { get; set; }
    public string? ReviewerNotes { get; set; } = string.Empty;
    public int Weight { get; set; } = 1; // Importance weighting for compliance scoring
    public string? Requirement { get; set; } = string.Empty; // Regulatory requirement reference
    public string? Guidance { get; set; } = string.Empty; // How to demonstrate compliance
    public bool RequiresEvidenceDocument { get; set; } = false;
    public string? DocumentReference { get; set; } = string.Empty;
    public string? NonComplianceReason { get; set; } = string.Empty;
    public string? CorrectionAction { get; set; } = string.Empty;
    public DateTime? CorrectionDueDate { get; set; }
    public DateTime? CorrectionCompletedDate { get; set; }
    public string? RiskLevel { get; set; } = "Medium"; // Low, Medium, High, Critical

    [NotMapped]
    public Dictionary<string, object> AssessmentDetails { get; set; } = new();

    [NotMapped]
    public List<string> SupportingDocuments { get; set; } = new();

    [NotMapped]
    public Dictionary<string, object> Metadata { get; set; } = new();
}

