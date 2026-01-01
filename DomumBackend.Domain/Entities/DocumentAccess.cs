namespace DomumBackend.Domain.Entities
{
    public class DocumentAccess : BaseEntity
    {
        public string? FacilityId { get; set; }
        public long DocumentId { get; set; }
        public string? AccessedByUserId { get; set; }
        public string? AccessedByUserName { get; set; }
        public string? AccessedByUserRole { get; set; }

        // Access Details
        public string? AccessType { get; set; } // View, Download, Print, Edit, Share, Delete, Approve
        public DateTime AccessedDate { get; set; } = DateTime.UtcNow;
        public string? AccessReason { get; set; }
        public string? IPAddress { get; set; }
        public string? DeviceInfo { get; set; } // User agent, device type
        public string? SessionId { get; set; }

        // Access Duration
        public DateTime? AccessStartTime { get; set; }
        public DateTime? AccessEndTime { get; set; }
        public int? DurationSeconds { get; set; }

        // Authorization Details
        public bool WasAuthorized { get; set; } = true;
        public string? AuthorizationLevel { get; set; } // Full, Limited, ReadOnly
        public string? AuthorizationReason { get; set; }

        // Document State at Access
        public string? DocumentVersionAtAccess { get; set; }
        public string? DocumentStatusAtAccess { get; set; }

        // Additional Context
        public string? ApplicationName { get; set; } // Which app/module accessed it
        public string? ApiEndpoint { get; set; } // Which API endpoint was called
        public string? RequestMethod { get; set; } // GET, POST, etc.
        public string? QueryParameters { get; set; }

        // Download/Print Specifics
        public bool WasDownloaded { get; set; } = false;
        public string? DownloadFormat { get; set; } // Original, PDF, etc.
        public bool WasPrinted { get; set; } = false;
        public int? PrintPageCount { get; set; }
        public bool WasEdited { get; set; } = false;

        // Sharing Context
        public bool WasShared { get; set; } = false;
        public string? SharedWithUserId { get; set; }
        public string[]? SharedWithUserIds { get; set; }
        public string? SharingExpiration { get; set; }

        // Security & Compliance
        public bool AccessViolated { get; set; } = false;
        public string? AccessViolationReason { get; set; }
        public bool IsAnomalousAccess { get; set; } = false;
        public string? AnomalyIndicators { get; set; }
        public bool WasEncrypted { get; set; } = false;
        public bool WasMasked { get; set; } = false;
        public bool DataExfiltrationDetected { get; set; } = false;

        // Audit Notes
        public string? Notes { get; set; }
        public bool FlaggedForReview { get; set; } = false;
        public string? FlagReason { get; set; }

        // Navigation
        public Facility? Facility { get; set; }
        public Document? Document { get; set; }
    }
}

