namespace DomumBackend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComplianceAudits")]
public class ComplianceAudit : BaseEntity
{
    public string FacilityId { get; set; } = string.Empty;
    public string AuditType { get; set; } = string.Empty; // SafeguardingAudit, HealthAndSafety, DATAProtection, FinancialCompliance, Staffing, etc.
    public string AuditName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuditStatus { get; set; } = "Pending"; // Pending, InProgress, Completed, Overdue
    public long? AssignedToStaffId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public int Priority { get; set; } = 1; // 1=Low, 2=Medium, 3=High, 4=Critical
    public string Frequency { get; set; } = "Annual"; // Annual, Biannual, Quarterly, Monthly, Weekly
    public DateTime? LastCompletedDate { get; set; }
    public DateTime? NextScheduledDate { get; set; }
    public int ComplianceScore { get; set; } = 0; // 0-100
    public int TotalChecklistItems { get; set; } = 0;
    public int CompletedChecklistItems { get; set; } = 0;
    public string RegulatoryBody { get; set; } = string.Empty; // Ofsted, CQC, HSE, ICO, etc.
    public string ReferenceNumber { get; set; } = string.Empty; // Audit reference or case number
    public string Findings { get; set; } = string.Empty; // Summary of findings
    public string Recommendations { get; set; } = string.Empty; // Recommended actions
    public bool RequiresFollowUp { get; set; } = false;
    public DateTime? FollowUpDate { get; set; }
    public string FollowUpStatus { get; set; } = string.Empty; // NotRequired, Pending, InProgress, Completed
    public string FollowUpFindings { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public decimal? ActualCost { get; set; }
    public string DocumentReference { get; set; } = string.Empty; // Link to audit document
    public bool IsRemote { get; set; } = false;
    public string AuditorName { get; set; } = string.Empty;
    public string AuditorOrganization { get; set; } = string.Empty;
    public string AuditorContactInfo { get; set; } = string.Empty;
    public string NotesForFollowUp { get; set; } = string.Empty;
    public string CreatedByUserId { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }

    [NotMapped]
    public List<ComplianceChecklistItem> ChecklistItems { get; set; } = new();

    [NotMapped]
    public Dictionary<string, object> NonConformities { get; set; } = new();

    [NotMapped]
    public List<string> CorrectionActions { get; set; } = new();

    [NotMapped]
    public Dictionary<string, object> EvidenceDocuments { get; set; } = new();

    [NotMapped]
    public Dictionary<string, object> Metadata { get; set; } = new();
}
