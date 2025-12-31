namespace DomumBackend.Domain.Entities
{
    public class Document : BaseEntity
    {
        public string FacilityId { get; set; }
        public string FileName { get; set; } // e.g., "MedicalReport_20250101.pdf"
        public string FileExtension { get; set; } // e.g., ".pdf"
        public long FileSizeBytes { get; set; }
        public string ContentType { get; set; } // e.g., "application/pdf"
        public string S3Key { get; set; } // AWS S3 storage key path
        public string S3BucketName { get; set; }

        // Document Identification
        public string DocumentType { get; set; } // Medical, Legal, Administrative, Incident, Staff, Financial, Training, Inspection, etc.
        public string DocumentCategory { get; set; } // Subcategory (e.g., "MedicalHistory", "Diagnosis", "Treatment", etc.)
        public string DocumentTitle { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; } // Unique reference number for the document
        public string InternalReference { get; set; } // Link to related entity ID (e.g., IncidentId, YoungPersonId)
        public string ExternalReference { get; set; } // External system reference

        // Document Dates
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
        public string UploadedByUserId { get; set; }
        public DateTime? DocumentDate { get; set; } // When the document was originally created/issued
        public DateTime? EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Versioning
        public int VersionNumber { get; set; } = 1;
        public long? ParentDocumentId { get; set; } // Reference to original version if this is an update
        public string VersionNotes { get; set; }

        // Classification & Security
        public string ClassificationLevel { get; set; } // Public, Internal, Confidential, Restricted, Secret
        public string AccessLevel { get; set; } // Everyone, FacilityStaff, SpecificRoles, SpecificUsers
        public string[] AllowedRoles { get; set; } // Roles that can access this document
        public string[] AllowedUserIds { get; set; } // Specific users who can access
        public bool IsPublic { get; set; } = false;
        public bool RequiresSignature { get; set; } = false;
        public bool IsEncrypted { get; set; } = false;

        // Status & Lifecycle
        public string Status { get; set; } = "Active"; // Active, Draft, Archived, Deleted, Expired, Restricted
        public bool IsArchived { get; set; } = false;
        public DateTime? ArchivedDate { get; set; }
        public string ArchivedByUserId { get; set; }
        public bool IsSoftCopy { get; set; } = true;
        public bool IsHardCopyStored { get; set; } = false;
        public string HardCopyLocation { get; set; }

        // Retention & Compliance
        public int RetentionDays { get; set; } = 2555; // Default 7 years
        public DateTime? ScheduledDeletionDate { get; set; }
        public string RetentionReason { get; set; } // Legal, Regulatory, Historical, Clinical, etc.
        public bool IsComplianceDocument { get; set; } = false;
        public string ComplianceStandard { get; set; } // GDPR, HIPAA, SOC2, ISO27001, etc.
        public string DataClassification { get; set; } // Personal, Sensitive, PII, PHI, etc.

        // Metadata & Search
        public string[] Tags { get; set; }
        public string SearchableText { get; set; } // Indexed for full-text search
        public string Keywords { get; set; }
        public string[] RelatedDocumentIds { get; set; }
        public string SourceSystem { get; set; } // Where the document originated
        public string Author { get; set; }

        // Audit & Tracking
        public int AccessCount { get; set; } = 0;
        public DateTime? LastAccessedDate { get; set; }
        public string LastAccessedByUserId { get; set; }
        public int DownloadCount { get; set; } = 0;
        public int PrintCount { get; set; } = 0;
        public bool HasBeenModified { get; set; } = false;
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByUserId { get; set; }

        // Additional Properties
        public string StorageFormat { get; set; } // PDF, DOC, XLS, Image, etc.
        public bool IsCompressed { get; set; } = false;
        public string CompressionType { get; set; }
        public string Checksum { get; set; } // SHA256 hash for integrity
        public bool RequiresApproval { get; set; } = false;
        public string ApprovalStatus { get; set; } // Pending, Approved, Rejected
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        public string Notes { get; set; }

        // Navigation Properties
        public Facility Facility { get; set; }
    }
}
