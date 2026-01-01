namespace DomumBackend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComplianceNonConformities")]
public class ComplianceNonConformity : BaseEntity
{
    public long ComplianceAuditId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Severity { get; set; } = "Medium"; // Minor, Major, Critical
    public string RootCause { get; set; } = string.Empty;
    public string CorrectionAction { get; set; } = string.Empty;
    public long? ResponsibleStaffId { get; set; }
    public DateTime IdentifiedDate { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string Status { get; set; } = "Open"; // Open, InProgress, Resolved, Closed, Reopened
    public string Evidence { get; set; } = string.Empty;
    public int Priority { get; set; } = 2; // 1=Low, 2=Medium, 3=High, 4=Critical
    public bool RequiresFollowUp { get; set; } = false;
    public DateTime? FollowUpDate { get; set; }
    public string FollowUpFindings { get; set; } = string.Empty;
    public string RegulatoryReference { get; set; } = string.Empty;
    public decimal? CostToResolve { get; set; }
    public string ResolutionNotes { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public long? VerifiedByStaffId { get; set; }
    public DateTime? VerificationDate { get; set; }

    [NotMapped]
    public List<string> SupportingDocuments { get; set; } = new();

    [NotMapped]
    public Dictionary<string, object> Timeline { get; set; } = new();

    [NotMapped]
    public Dictionary<string, object> Metadata { get; set; } = new();
}
