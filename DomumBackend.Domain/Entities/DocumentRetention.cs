namespace DomumBackend.Domain.Entities
{
    public class DocumentRetention : BaseEntity
    {
        public string? FacilityId { get; set; }
        public string? PolicyName { get; set; }
        public string? Description { get; set; }

        // Policy Configuration
        public string? DocumentType { get; set; } // Applied to documents of this type
        public string? DocumentCategory { get; set; }
        public string? ClassificationLevel { get; set; }
        public bool ApplyToAllDocuments { get; set; } = false;

        // Retention Schedule
        public int RetentionDays { get; set; } // How long to keep the document
        public int MinimumRetentionDays { get; set; } = 0;
        public int MaximumRetentionDays { get; set; } = 3650; // 10 years
        public string? RetentionUnit { get; set; } // Days, Months, Years
        public string? RetentionTrigger { get; set; } // DocumentCreation, DocumentClosure, EventOccurrence, ExpiryDate

        // Archival Policy
        public bool EnableArchival { get; set; } = true;
        public int DaysBeforeArchival { get; set; } // Archive after N days of inactivity
        public string? ArchivalLocation { get; set; } // Cold storage, Glacier, OnSite, etc.
        public bool CompressBeforeArchival { get; set; } = true;
        public bool EncryptBeforeArchival { get; set; } = true;

        // Deletion Policy
        public bool EnableAutomaticDeletion { get; set; } = true;
        public int DaysBeforeDeletion { get; set; } // Delete after retention period + this
        public bool PermanentDelete { get; set; } = true;
        public string? DeletionMethod { get; set; } // Soft, Hard, SecureWipe, CryptographicErase
        public int SecureWipeOverwrites { get; set; } = 7; // DoD standard
        public bool RequireApprovalBeforeDeletion { get; set; } = true;

        // Compliance
        public string? ComplianceStandard { get; set; } // GDPR, HIPAA, SOC2, ISO27001
        public string? JustificationDocumentation { get; set; }
        public bool IsComplianceRequired { get; set; } = false;
        public string? LegalHold { get; set; } // LitHold, Regulatory, Investigation, etc.
        public DateTime? LegalHoldExpiryDate { get; set; }
        public bool BypassDeletionIfLegalHold { get; set; } = true;

        // Audit & Monitoring
        public int DocumentsAffected { get; set; } = 0;
        public int DocumentsArchived { get; set; } = 0;
        public int DocumentsDeleted { get; set; } = 0;
        public DateTime? LastExecutionDate { get; set; }
        public string? LastExecutionStatus { get; set; } // Success, Failed, Partial
        public int? NextScheduledExecutionDays { get; set; }

        // Processing Rules
        public bool AutoExecute { get; set; } = true;
        public string? ExecutionSchedule { get; set; } // Daily, Weekly, Monthly, Quarterly
        public string? ExecutionTime { get; set; } // HH:mm in UTC
        public bool NotifyBeforeDeletion { get; set; } = true;
        public int NotificationDaysBefore { get; set; } = 7;

        // Exceptions
        public bool AllowExceptions { get; set; } = true;
        public int ExceptionCount { get; set; } = 0;
        public string[]? ExceptedDocumentIds { get; set; }
        public string[]? ExceptedUserIds { get; set; }

        // Status
        public bool IsActive { get; set; } = true;
        public bool IsPilot { get; set; } = false;
        public string? Status { get; set; } // Draft, Active, Suspended, Archived
        public DateTime? EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }

        // Audit Trail
        public string? CreatedByUserId { get; set; }
        public string? LastModifiedByUserId { get; set; }
        public string? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }

        // Notes
        public string? Notes { get; set; }
        public string? ImplementationNotes { get; set; }

        // Navigation
        public Facility? Facility { get; set; }
    }
}

