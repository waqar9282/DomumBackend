namespace DomumBackend.Application.DTOs
{
    public class DocumentDTO
    {
        public long Id { get; set; }
        public string? FileName { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentCategory { get; set; }
        public long FileSizeBytes { get; set; }
        public string? Status { get; set; }
        public string? AccessLevel { get; set; }
        public DateTime UploadedDate { get; set; }
        public string? UploadedByUserId { get; set; }
        public int VersionNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? ClassificationLevel { get; set; }
        public int AccessCount { get; set; }
        public DateTime? LastAccessedDate { get; set; }
    }

    public class DocumentDetailDTO : DocumentDTO
    {
        public string? S3Key { get; set; }
        public string? Description { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string[]? Tags { get; set; }
        public string[]? AllowedRoles { get; set; }
        public string[]? AllowedUserIds { get; set; }
        public string? RetentionReason { get; set; }
        public int RetentionDays { get; set; }
        public DateTime? ScheduledDeletionDate { get; set; }
        public string? Checksum { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Notes { get; set; }
    }

    public class DocumentUploadRequestDTO
    {
        public string? FileName { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentCategory { get; set; }
        public string? DocumentTitle { get; set; }
        public string? Description { get; set; }
        public string? ClassificationLevel { get; set; }
        public string? AccessLevel { get; set; }
        public string[]? AllowedRoles { get; set; }
        public string[]? AllowedUserIds { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int RetentionDays { get; set; }
        public string? InternalReference { get; set; }
        public string[]? Tags { get; set; }
        public bool RequiresApproval { get; set; }
    }

    public class DocumentUploadResponseDTO
    {
        public long DocumentId { get; set; }
        public string? FileName { get; set; }
        public long FileSizeBytes { get; set; }
        public string? S3Key { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public DateTime UploadedDate { get; set; }
    }

    public class DocumentSearchResultDTO
    {
        public long Id { get; set; }
        public string? FileName { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentCategory { get; set; }
        public DateTime UploadedDate { get; set; }
        public string? UploadedByUserId { get; set; }
        public int VersionNumber { get; set; }
        public double RelevanceScore { get; set; } // Full-text search relevance
    }

    public class DocumentVersionDTO
    {
        public int VersionNumber { get; set; }
        public long DocumentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedByUserId { get; set; }
        public string? VersionNotes { get; set; }
        public long FileSizeBytes { get; set; }
        public bool IsCurrentVersion { get; set; }
    }

    public class DocumentAccessDTO
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public string? AccessedByUserId { get; set; }
        public string? AccessType { get; set; }
        public DateTime AccessedDate { get; set; }
        public string? AccessReason { get; set; }
        public string? IPAddress { get; set; }
        public int? DurationSeconds { get; set; }
        public bool AccessViolated { get; set; }
    }

    public class DocumentCategoryDTO
    {
        public string? CategoryName { get; set; }
        public string? CategoryCode { get; set; }
        public string? Description { get; set; }
        public int DocumentCount { get; set; }
        public bool IsActive { get; set; }
        public string? DefaultClassificationLevel { get; set; }
        public int DefaultRetentionDays { get; set; }
    }

    public class DocumentRetentionPolicyDTO
    {
        public long Id { get; set; }
        public string? PolicyName { get; set; }
        public string? DocumentType { get; set; }
        public int RetentionDays { get; set; }
        public bool EnableAutomaticDeletion { get; set; }
        public string? ComplianceStandard { get; set; }
        public bool IsActive { get; set; }
        public int DocumentsAffected { get; set; }
        public DateTime? LastExecutionDate { get; set; }
    }

    public class BulkDocumentUploadResponseDTO
    {
        public int TotalFiles { get; set; }
        public int SuccessfulUploads { get; set; }
        public int FailedUploads { get; set; }
        public List<DocumentUploadResponseDTO>? SuccessfulDocuments { get; set; }
        public List<string>? FailedFiles { get; set; }
    }

    public class DocumentMetadataUpdateDTO
    {
        public string? DocumentTitle { get; set; }
        public string? Description { get; set; }
        public string? ClassificationLevel { get; set; }
        public string? AccessLevel { get; set; }
        public string[]? AllowedRoles { get; set; }
        public string[]? AllowedUserIds { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string[]? Tags { get; set; }
        public string? Notes { get; set; }
    }

    public class DocumentShareDTO
    {
        public long DocumentId { get; set; }
        public string[]? UserIds { get; set; }
        public string[]? Roles { get; set; }
        public string? SharingReason { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool CanDownload { get; set; }
        public bool CanPrint { get; set; }
        public bool CanShare { get; set; }
    }
}

