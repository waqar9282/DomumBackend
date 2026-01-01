using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentUploadResponseDTO> UploadDocumentAsync(string facilityId, string userId, DocumentUploadRequestDTO request, Stream fileStream)
        {
            var document = new Document
            {
                FacilityId = facilityId,
                FileName = request.FileName,
                FileExtension = Path.GetExtension(request.FileName),
                DocumentType = request.DocumentType,
                DocumentCategory = request.DocumentCategory,
                DocumentTitle = request.DocumentTitle,
                Description = request.Description,
                ClassificationLevel = request.ClassificationLevel,
                AccessLevel = request.AccessLevel,
                AllowedRoles = request.AllowedRoles,
                AllowedUserIds = request.AllowedUserIds,
                DocumentDate = request.DocumentDate,
                ExpiryDate = request.ExpiryDate,
                RetentionDays = request.RetentionDays,
                UploadedByUserId = userId,
                UploadedDate = DateTime.UtcNow,
                Status = request.RequiresApproval ? "Draft" : "Active",
                RequiresApproval = request.RequiresApproval,
                Tags = request.Tags,
                InternalReference = request.InternalReference
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return new DocumentUploadResponseDTO
            {
                DocumentId = document.Id,
                FileName = document.FileName,
                FileSizeBytes = fileStream.Length,
                Status = document.Status,
                UploadedDate = document.UploadedDate,
                Message = "Document uploaded successfully"
            };
        }

        public async Task<BulkDocumentUploadResponseDTO> BulkUploadDocumentsAsync(string facilityId, string userId, List<(DocumentUploadRequestDTO metadata, Stream fileStream)> files)
        {
            var response = new BulkDocumentUploadResponseDTO
            {
                TotalFiles = files.Count,
                SuccessfulDocuments = new List<DocumentUploadResponseDTO>(),
                FailedFiles = new List<string>()
            };

            foreach (var (metadata, fileStream) in files)
            {
                try
                {
                    var result = await UploadDocumentAsync(facilityId, userId, metadata, fileStream);
                    response.SuccessfulDocuments.Add(result);
                    response.SuccessfulUploads++;
                }
                catch
                {
                    response.FailedFiles.Add(metadata.FileName ?? "Unknown");
                    response.FailedUploads++;
                }
            }

            return response;
        }

        public async Task<string> GeneratePresignedUrlAsync(long documentId, string facilityId, int expirationMinutes = 60)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            // TODO: Implement S3 presigned URL generation
            return $"https://s3.amazonaws.com/{document.S3BucketName}/{document.S3Key}?expires={expirationMinutes}";
        }

        public async Task<DocumentDetailDTO> GetDocumentByIdAsync(long documentId, string facilityId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            return MapToDetailDTO(document);
        }

        public async Task<Stream> DownloadDocumentAsync(long documentId, string facilityId, string userId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            await LogAccessAsync(documentId, facilityId, userId, "Download", "User download request");
            
            // TODO: Retrieve from S3
            return new MemoryStream();
        }

        public async Task<List<DocumentDTO>> GetDocumentsByFacilityAsync(string facilityId, int pageNumber, int pageSize)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.Status != "Deleted")
                .OrderByDescending(d => d.UploadedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<List<DocumentDTO>> GetDocumentsByTypeAsync(string facilityId, string documentType, int pageNumber, int pageSize)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.DocumentType == documentType && d.Status != "Deleted")
                .OrderByDescending(d => d.UploadedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<List<DocumentDTO>> GetDocumentsByCategoryAsync(string facilityId, string category, int pageNumber, int pageSize)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.DocumentCategory == category && d.Status != "Deleted")
                .OrderByDescending(d => d.UploadedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<List<DocumentDTO>> GetDocumentsByUserAsync(string facilityId, string userId, int pageNumber, int pageSize)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.UploadedByUserId == userId && d.Status != "Deleted")
                .OrderByDescending(d => d.UploadedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<List<DocumentSearchResultDTO>> SearchDocumentsAsync(string facilityId, string searchQuery, int pageNumber, int pageSize)
        {
            var query = searchQuery.ToLower();
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.Status != "Deleted" &&
                    (d.FileName!.ToLower().Contains(query) || d.DocumentTitle!.ToLower().Contains(query) || (d.SearchableText ?? "").ToLower().Contains(query)))
                .OrderByDescending(d => d.UploadedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DocumentSearchResultDTO
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    DocumentType = d.DocumentType,
                    DocumentCategory = d.DocumentCategory,
                    UploadedDate = d.UploadedDate,
                    UploadedByUserId = d.UploadedByUserId,
                    VersionNumber = d.VersionNumber,
                    RelevanceScore = 1.0
                })
                .ToListAsync();
        }

        public async Task<List<DocumentDTO>> FilterDocumentsAsync(string facilityId, DocumentFilterCriteria criteria, int pageNumber, int pageSize)
        {
            var query = _context.Documents.Where(d => d.FacilityId == facilityId && d.Status != "Deleted");

            if (!string.IsNullOrEmpty(criteria.DocumentType))
                query = query.Where(d => d.DocumentType == criteria.DocumentType);
            if (!string.IsNullOrEmpty(criteria.DocumentCategory))
                query = query.Where(d => d.DocumentCategory == criteria.DocumentCategory);
            if (!string.IsNullOrEmpty(criteria.Status))
                query = query.Where(d => d.Status == criteria.Status);
            if (!string.IsNullOrEmpty(criteria.ClassificationLevel))
                query = query.Where(d => d.ClassificationLevel == criteria.ClassificationLevel);
            if (criteria.FromDate.HasValue)
                query = query.Where(d => d.UploadedDate >= criteria.FromDate);
            if (criteria.ToDate.HasValue)
                query = query.Where(d => d.UploadedDate <= criteria.ToDate);

            return await query
                .OrderByDescending(d => d.UploadedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<List<DocumentDTO>> GetExpiringDocumentsAsync(string facilityId, int daysUntilExpiry = 30)
        {
            var expiryDate = DateTime.UtcNow.AddDays(daysUntilExpiry);
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.ExpiryDate.HasValue && d.ExpiryDate <= expiryDate && d.Status == "Active")
                .OrderBy(d => d.ExpiryDate)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<List<DocumentDTO>> GetArchivedDocumentsAsync(string facilityId, int pageNumber, int pageSize)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.IsArchived)
                .OrderByDescending(d => d.ArchivedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<DocumentVersionDTO> CreateVersionAsync(long documentId, string facilityId, string userId, Stream newFileStream, string? versionNotes)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.VersionNumber++;
            document.VersionNotes = versionNotes;
            await _context.SaveChangesAsync();

            return new DocumentVersionDTO
            {
                VersionNumber = document.VersionNumber,
                DocumentId = document.Id,
                CreatedDate = DateTime.UtcNow,
                CreatedByUserId = userId,
                VersionNotes = versionNotes,
                IsCurrentVersion = true
            };
        }

        public async Task<List<DocumentVersionDTO>> GetDocumentVersionsAsync(long documentId, string facilityId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            // TODO: Retrieve versions from version history table
            return new List<DocumentVersionDTO>
            {
                new DocumentVersionDTO
                {
                    VersionNumber = document.VersionNumber,
                    DocumentId = document.Id,
                    CreatedDate = document.CreatedDate,
                    CreatedByUserId = document.UploadedByUserId,
                    IsCurrentVersion = true
                }
            };
        }

        public async Task<DocumentDetailDTO> RestoreVersionAsync(long documentId, int versionNumber, string facilityId, string userId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            // TODO: Restore from version history
            return MapToDetailDTO(document);
        }

        public async Task UpdateDocumentMetadataAsync(long documentId, string facilityId, DocumentMetadataUpdateDTO metadata)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            if (!string.IsNullOrEmpty(metadata.DocumentTitle))
                document.DocumentTitle = metadata.DocumentTitle;
            if (!string.IsNullOrEmpty(metadata.Description))
                document.Description = metadata.Description;
            if (!string.IsNullOrEmpty(metadata.ClassificationLevel))
                document.ClassificationLevel = metadata.ClassificationLevel;
            if (!string.IsNullOrEmpty(metadata.AccessLevel))
                document.AccessLevel = metadata.AccessLevel;
            if (metadata.AllowedRoles != null)
                document.AllowedRoles = metadata.AllowedRoles;
            if (metadata.AllowedUserIds != null)
                document.AllowedUserIds = metadata.AllowedUserIds;
            if (metadata.ExpiryDate.HasValue)
                document.ExpiryDate = metadata.ExpiryDate;
            if (metadata.Tags != null)
                document.Tags = metadata.Tags;
            if (!string.IsNullOrEmpty(metadata.Notes))
                document.Notes = metadata.Notes;

            document.HasBeenModified = true;
            document.LastModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task SetAccessLevelAsync(long documentId, string facilityId, string accessLevel, string[]? allowedRoles = null, string[]? allowedUserIds = null)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.AccessLevel = accessLevel;
            document.AllowedRoles = allowedRoles;
            document.AllowedUserIds = allowedUserIds;
            document.IsPublic = accessLevel == "Everyone";
            await _context.SaveChangesAsync();
        }

        public async Task AddTagsAsync(long documentId, string facilityId, string[] tags)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.Tags = (document.Tags ?? Array.Empty<string>()).Union(tags).ToArray();
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagsAsync(long documentId, string facilityId, string[] tags)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.Tags = (document.Tags ?? Array.Empty<string>()).Except(tags).ToArray();
            await _context.SaveChangesAsync();
        }

        public async Task ShareDocumentAsync(long documentId, string facilityId, string userId, DocumentShareDTO shareInfo)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.AllowedUserIds = (document.AllowedUserIds ?? Array.Empty<string>()).Union(shareInfo.UserIds ?? Array.Empty<string>()).ToArray();
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAccessAsync(long documentId, string facilityId, string revokedUserId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.AllowedUserIds = (document.AllowedUserIds ?? Array.Empty<string>()).Where(u => u != revokedUserId).ToArray();
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetDocumentAccessListAsync(long documentId, string facilityId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            return (document.AllowedUserIds ?? Array.Empty<string>()).ToList();
        }

        public async Task ApproveDocumentAsync(long documentId, string facilityId, string approverId, string approvalNotes)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.ApprovalStatus = "Approved";
            document.ApprovedByUserId = approverId;
            document.ApprovalDate = DateTime.UtcNow;
            document.Status = "Active";
            await _context.SaveChangesAsync();
        }

        public async Task RejectDocumentAsync(long documentId, string facilityId, string rejectorId, string rejectionReason)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.ApprovalStatus = "Rejected";
            document.Status = "Draft";
            document.RejectionReason = rejectionReason;
            await _context.SaveChangesAsync();
        }

        public async Task ArchiveDocumentAsync(long documentId, string facilityId, string userId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.IsArchived = true;
            document.ArchivedDate = DateTime.UtcNow;
            document.ArchivedByUserId = userId;
            document.Status = "Archived";
            await _context.SaveChangesAsync();
        }

        public async Task RestoreDocumentAsync(long documentId, string facilityId, string userId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.IsArchived = false;
            document.ArchivedDate = null;
            document.ArchivedByUserId = null;
            document.Status = "Active";
            await _context.SaveChangesAsync();
        }

        public async Task PlaceLegalHoldAsync(long documentId, string facilityId, string reason, DateTime? expiryDate = null)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            // Document legal hold implementation
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLegalHoldAsync(long documentId, string facilityId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            // Remove legal hold
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocumentRetentionPolicyDTO>> GetApplicableRetentionPoliciesAsync(string facilityId, string documentType)
        {
            // TODO: Implement retention policy retrieval
            return new List<DocumentRetentionPolicyDTO>();
        }

        public async Task SoftDeleteDocumentAsync(long documentId, string facilityId, string userId, string reason)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            document.Status = "Deleted";
            document.Notes = reason;
            await _context.SaveChangesAsync();
        }

        public async Task PermanentlyDeleteDocumentAsync(long documentId, string facilityId, string userId, string reason)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocumentDTO>> GetScheduledForDeletionAsync(string facilityId)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.ScheduledDeletionDate.HasValue && d.ScheduledDeletionDate <= DateTime.UtcNow)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task LogAccessAsync(long documentId, string facilityId, string userId, string accessType, string reason)
        {
            var access = new DocumentAccess
            {
                FacilityId = facilityId,
                DocumentId = documentId,
                AccessedByUserId = userId,
                AccessType = accessType,
                AccessedDate = DateTime.UtcNow,
                AccessReason = reason,
                WasAuthorized = true
            };

            _context.DocumentAccesses.Add(access);
            
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId);
            if (document != null)
            {
                document.AccessCount++;
                document.LastAccessedDate = DateTime.UtcNow;
                document.LastAccessedByUserId = userId;
            }

            await _context.SaveChangesAsync();
        }

        private DocumentDTO MapToDTO(Document document)
        {
            return new DocumentDTO
            {
                Id = document.Id,
                FileName = document.FileName,
                DocumentType = document.DocumentType,
                DocumentCategory = document.DocumentCategory,
                FileSizeBytes = document.FileSizeBytes,
                Status = document.Status,
                AccessLevel = document.AccessLevel,
                UploadedDate = document.UploadedDate,
                UploadedByUserId = document.UploadedByUserId,
                VersionNumber = document.VersionNumber,
                ExpiryDate = document.ExpiryDate,
                ClassificationLevel = document.ClassificationLevel,
                AccessCount = document.AccessCount,
                LastAccessedDate = document.LastAccessedDate
            };
        }

        private DocumentDetailDTO MapToDetailDTO(Document document)
        {
            return new DocumentDetailDTO
            {
                Id = document.Id,
                FileName = document.FileName,
                DocumentType = document.DocumentType,
                DocumentCategory = document.DocumentCategory,
                FileSizeBytes = document.FileSizeBytes,
                Status = document.Status,
                AccessLevel = document.AccessLevel,
                UploadedDate = document.UploadedDate,
                UploadedByUserId = document.UploadedByUserId,
                VersionNumber = document.VersionNumber,
                ExpiryDate = document.ExpiryDate,
                ClassificationLevel = document.ClassificationLevel,
                AccessCount = document.AccessCount,
                LastAccessedDate = document.LastAccessedDate,
                S3Key = document.S3Key,
                Description = document.Description,
                DocumentNumber = document.DocumentNumber,
                DocumentDate = document.DocumentDate,
                Tags = document.Tags ?? Array.Empty<string>(),
                AllowedRoles = document.AllowedRoles ?? Array.Empty<string>(),
                AllowedUserIds = document.AllowedUserIds ?? Array.Empty<string>(),
                RetentionReason = document.RetentionReason,
                RetentionDays = document.RetentionDays,
                ScheduledDeletionDate = document.ScheduledDeletionDate,
                Checksum = document.Checksum,
                ApprovalStatus = document.ApprovalStatus,
                Notes = document.Notes
            };
        }
    }
}
