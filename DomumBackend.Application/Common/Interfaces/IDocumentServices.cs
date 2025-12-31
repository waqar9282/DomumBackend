using DomumBackend.Application.DTOs;

namespace DomumBackend.Application.Common.Interfaces
{
    public interface IDocumentService
    {
        // Upload Operations
        Task<DocumentUploadResponseDTO> UploadDocumentAsync(string facilityId, string userId, DocumentUploadRequestDTO request, Stream fileStream);
        Task<BulkDocumentUploadResponseDTO> BulkUploadDocumentsAsync(string facilityId, string userId, List<(DocumentUploadRequestDTO metadata, Stream fileStream)> files);
        Task<string> GeneratePresignedUrlAsync(long documentId, string facilityId, int expirationMinutes = 60);

        // Document Retrieval
        Task<DocumentDetailDTO> GetDocumentByIdAsync(long documentId, string facilityId);
        Task<Stream> DownloadDocumentAsync(long documentId, string facilityId, string userId);
        Task<List<DocumentDTO>> GetDocumentsByFacilityAsync(string facilityId, int pageNumber, int pageSize);
        Task<List<DocumentDTO>> GetDocumentsByTypeAsync(string facilityId, string documentType, int pageNumber, int pageSize);
        Task<List<DocumentDTO>> GetDocumentsByCategoryAsync(string facilityId, string category, int pageNumber, int pageSize);
        Task<List<DocumentDTO>> GetDocumentsByUserAsync(string facilityId, string userId, int pageNumber, int pageSize);

        // Search & Filter
        Task<List<DocumentSearchResultDTO>> SearchDocumentsAsync(string facilityId, string searchQuery, int pageNumber, int pageSize);
        Task<List<DocumentDTO>> FilterDocumentsAsync(string facilityId, DocumentFilterCriteria criteria, int pageNumber, int pageSize);
        Task<List<DocumentDTO>> GetExpiringDocumentsAsync(string facilityId, int daysUntilExpiry = 30);
        Task<List<DocumentDTO>> GetArchivedDocumentsAsync(string facilityId, int pageNumber, int pageSize);

        // Versioning
        Task<DocumentVersionDTO> CreateVersionAsync(long documentId, string facilityId, string userId, Stream newFileStream, string versionNotes);
        Task<List<DocumentVersionDTO>> GetDocumentVersionsAsync(long documentId, string facilityId);
        Task<DocumentDetailDTO> RestoreVersionAsync(long documentId, int versionNumber, string facilityId, string userId);

        // Metadata Operations
        Task UpdateDocumentMetadataAsync(long documentId, string facilityId, DocumentMetadataUpdateDTO metadata);
        Task SetAccessLevelAsync(long documentId, string facilityId, string accessLevel, string[] allowedRoles = null, string[] allowedUserIds = null);
        Task AddTagsAsync(long documentId, string facilityId, string[] tags);
        Task RemoveTagsAsync(long documentId, string facilityId, string[] tags);

        // Sharing
        Task ShareDocumentAsync(long documentId, string facilityId, string userId, DocumentShareDTO shareInfo);
        Task RevokeAccessAsync(long documentId, string facilityId, string revokedUserId);
        Task<List<string>> GetDocumentAccessListAsync(long documentId, string facilityId);

        // Status Operations
        Task ApproveDocumentAsync(long documentId, string facilityId, string approverId, string approvalNotes);
        Task RejectDocumentAsync(long documentId, string facilityId, string rejectorId, string rejectionReason);
        Task ArchiveDocumentAsync(long documentId, string facilityId, string userId);
        Task RestoreDocumentAsync(long documentId, string facilityId, string userId);

        // Compliance
        Task PlaceLegalHoldAsync(long documentId, string facilityId, string reason, DateTime? expiryDate = null);
        Task RemoveLegalHoldAsync(long documentId, string facilityId);
        Task<List<DocumentRetentionPolicyDTO>> GetApplicableRetentionPoliciesAsync(string facilityId, string documentType);

        // Deletion
        Task SoftDeleteDocumentAsync(long documentId, string facilityId, string userId, string reason);
        Task PermanentlyDeleteDocumentAsync(long documentId, string facilityId, string userId, string reason);
        Task<List<DocumentDTO>> GetScheduledForDeletionAsync(string facilityId);

        // Audit & Access Tracking
        Task LogAccessAsync(long documentId, string facilityId, string userId, string accessType, string reason);
    }

    public interface IDocumentCategoryService
    {
        Task<DocumentCategoryDTO> CreateCategoryAsync(string facilityId, string categoryName, string categoryCode, string description);
        Task<DocumentCategoryDTO> GetCategoryByCodeAsync(string facilityId, string categoryCode);
        Task<DocumentCategoryDTO> GetCategoryByNameAsync(string facilityId, string categoryName);
        Task<List<DocumentCategoryDTO>> GetAllCategoriesAsync(string facilityId, bool includeInactive = false);
        Task UpdateCategoryAsync(string facilityId, string categoryCode, DocumentCategoryDTO updates);
        Task DeleteCategoryAsync(string facilityId, string categoryCode);
        Task<int> GetDocumentCountByCategoryAsync(string facilityId, string categoryCode);
        Task SetDefaultAccessLevelAsync(string facilityId, string categoryCode, string accessLevel, string[] roles = null);
        Task SetDefaultRetentionAsync(string facilityId, string categoryCode, int retentionDays, string reason);
    }

    public interface IDocumentAccessService
    {
        // Access Logging
        Task LogAccessAsync(string facilityId, long documentId, string userId, string accessType, string reason, string ipAddress, string deviceInfo);
        Task<List<DocumentAccessDTO>> GetAccessLogAsync(long documentId, string facilityId, int pageNumber, int pageSize);
        Task<List<DocumentAccessDTO>> GetUserAccessHistoryAsync(string facilityId, string userId, int pageNumber, int pageSize);

        // Access Control Verification
        Task<bool> CanUserAccessDocumentAsync(string facilityId, long documentId, string userId, string requiredRole = null);
        Task<bool> CanUserDownloadAsync(string facilityId, long documentId, string userId);
        Task<bool> CanUserPrintAsync(string facilityId, long documentId, string userId);
        Task<bool> CanUserShareAsync(string facilityId, long documentId, string userId);
        Task<bool> CanUserApproveAsync(string facilityId, long documentId, string userId);

        // Anomaly Detection
        Task<List<DocumentAccessDTO>> GetAnomalousAccessesAsync(string facilityId, int pageNumber = 1, int pageSize = 50);
        Task<List<DocumentAccessDTO>> GetFlaggedAccessesAsync(string facilityId, int pageNumber = 1, int pageSize = 50);
        Task FlagAccessForReviewAsync(long accessLogId, string facilityId, string flagReason);

        // Statistics
        Task<int> GetAccessCountAsync(long documentId, string facilityId);
        Task<int> GetDownloadCountAsync(long documentId, string facilityId);
        Task<int> GetPrintCountAsync(long documentId, string facilityId);
        Task<Dictionary<string, int>> GetAccessTypeDistributionAsync(string facilityId, DateTime fromDate, DateTime toDate);
        Task<List<string>> GetMostAccessedDocumentsAsync(string facilityId, int topCount = 10);
    }

    public interface IDocumentRetentionService
    {
        // Policy Management
        Task<DocumentRetentionPolicyDTO> CreatePolicyAsync(string facilityId, string policyName, string documentType, int retentionDays);
        Task<DocumentRetentionPolicyDTO> GetPolicyByIdAsync(long policyId, string facilityId);
        Task<List<DocumentRetentionPolicyDTO>> GetPoliciesByFacilityAsync(string facilityId, bool includeInactive = false);
        Task<List<DocumentRetentionPolicyDTO>> GetPoliciesByDocumentTypeAsync(string facilityId, string documentType);
        Task UpdatePolicyAsync(long policyId, string facilityId, DocumentRetentionPolicyDTO updates);
        Task DeletePolicyAsync(long policyId, string facilityId);
        Task ActivatePolicyAsync(long policyId, string facilityId);
        Task DeactivatePolicyAsync(long policyId, string facilityId);

        // Compliance
        Task PlaceLegalHoldAsync(long documentId, string facilityId, string reason, DateTime? expiryDate = null);
        Task RemoveLegalHoldAsync(long documentId, string facilityId);
        Task<List<long>> GetDocumentsUnderLegalHoldAsync(string facilityId);
        Task<List<DocumentRetentionPolicyDTO>> GetComplianceRequiredPoliciesAsync(string facilityId);

        // Scheduling & Execution
        Task<List<long>> GetScheduledForArchivalAsync(string facilityId);
        Task<List<long>> GetScheduledForDeletionAsync(string facilityId);
        Task<List<long>> GetExpiredDocumentsAsync(string facilityId);
        Task ExecuteArchivalAsync(string facilityId, List<long> documentIds, string executedByUserId);
        Task ExecuteDeletionAsync(string facilityId, List<long> documentIds, string executedByUserId, string reason);
        Task AddExceptionAsync(long policyId, string facilityId, long documentId, string reason);
        Task RemoveExceptionAsync(long policyId, string facilityId, long documentId);

        // Notifications
        Task<List<long>> GetDocumentsRequiringDeletionNotificationAsync(string facilityId, int daysUntilDeletion = 7);
        Task SendDeletionNotificationAsync(long documentId, string facilityId, List<string> recipientUserIds);

        // Audit
        Task<Dictionary<string, int>> GetRetentionStatisticsAsync(string facilityId);
        Task<List<(long DocumentId, DateTime ScheduledDeletionDate)>> GetDeletionScheduleAsync(string facilityId, DateTime fromDate, DateTime toDate);
        Task<int> GetComplianceScoreAsync(string facilityId);
    }

    public class DocumentFilterCriteria
    {
        public string DocumentType { get; set; }
        public string DocumentCategory { get; set; }
        public string Status { get; set; }
        public string ClassificationLevel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string UploadedByUserId { get; set; }
        public string[] Tags { get; set; }
    }
}
