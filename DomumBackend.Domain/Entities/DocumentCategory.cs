namespace DomumBackend.Domain.Entities
{
    public class DocumentCategory : BaseEntity
    {
        public string FacilityId { get; set; }
        public string CategoryName { get; set; } // Medical, Legal, Administrative, Incident, Staff, Financial, Training, Inspection
        public string CategoryCode { get; set; } // Unique code (e.g., MED, LEG, ADM, INC, STF, FIN, TRN, INS)
        public string Description { get; set; }
        public string ParentCategoryId { get; set; } // For nested categories

        // Classification
        public string DefaultClassificationLevel { get; set; } // Public, Internal, Confidential, Restricted, Secret
        public string DefaultAccessLevel { get; set; }
        public bool RequiresApproval { get; set; } = false;

        // Retention Policy
        public int DefaultRetentionDays { get; set; } = 2555; // 7 years default
        public string RetentionReason { get; set; }
        public string ComplianceStandard { get; set; }

        // Configuration
        public bool AllowVersioning { get; set; } = true;
        public bool AllowMultipleVersions { get; set; } = true;
        public bool RequireSignature { get; set; } = false;
        public bool AllowSharing { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool AllowPublicAccess { get; set; } = false;

        // Metadata
        public int DocumentCount { get; set; } = 0;
        public DateTime? LastUsedDate { get; set; }
        public string[] AllowedFileTypes { get; set; } // pdf, doc, xls, image, etc.
        public long MaxFileSizeBytes { get; set; } = 10737418240; // 10 GB default

        // Audit
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }

        // Navigation
        public Facility Facility { get; set; }
    }
}
