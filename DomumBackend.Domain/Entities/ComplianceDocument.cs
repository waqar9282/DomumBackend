namespace DomumBackend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComplianceDocuments")]
public class ComplianceDocument : BaseEntity
{
    public long ComplianceAuditId { get; set; }
    public long? NonConformityId { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty; // Policy, Procedure, Evidence, Report, etc.
    public string Description { get; set; } = string.Empty;
    public string DocumentPath { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty; // PDF, Word, Excel, Image, etc.
    public long FileSizeBytes { get; set; } = 0;
    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
    public string UploadedByUserId { get; set; } = string.Empty;
    public DateTime? ReviewedDate { get; set; }
    public long? ReviewedByStaffId { get; set; }
    public string ReviewStatus { get; set; } = "Pending"; // Pending, Approved, Rejected, NeedsRevision
    public string ReviewFeedback { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
    public bool IsCurrentVersion { get; set; } = true;
    public int VersionNumber { get; set; } = 1;
    public string DocumentReference { get; set; } = string.Empty; // Tracking number
    public bool IsPublished { get; set; } = false;
    public DateTime? PublishedDate { get; set; }
    public string AccessLevel { get; set; } = "Internal"; // Internal, Shared, Public, Restricted
    public string Tags { get; set; } = string.Empty;

    [NotMapped]
    public Dictionary<string, object> Metadata { get; set; } = new();

    [NotMapped]
    public List<string> RelatedDocuments { get; set; } = new();
}
