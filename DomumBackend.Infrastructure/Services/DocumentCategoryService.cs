using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class DocumentCategoryService : IDocumentCategoryService
    {
        private readonly ApplicationDbContext _context;

        public DocumentCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentCategoryDTO> CreateCategoryAsync(string facilityId, string categoryName, string categoryCode, string description)
        {
            var category = new DocumentCategory
            {
                FacilityId = facilityId,
                CategoryName = categoryName,
                CategoryCode = categoryCode,
                Description = description,
                IsActive = true
            };

            _context.DocumentCategories.Add(category);
            await _context.SaveChangesAsync();

            return MapToDTO(category);
        }

        public async Task<DocumentCategoryDTO> GetCategoryByCodeAsync(string facilityId, string categoryCode)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.FacilityId == facilityId && c.CategoryCode == categoryCode);
            if (category == null) throw new Exception("Category not found");

            return MapToDTO(category);
        }

        public async Task<DocumentCategoryDTO> GetCategoryByNameAsync(string facilityId, string categoryName)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.FacilityId == facilityId && c.CategoryName == categoryName);
            if (category == null) throw new Exception("Category not found");

            return MapToDTO(category);
        }

        public async Task<List<DocumentCategoryDTO>> GetAllCategoriesAsync(string facilityId, bool includeInactive = false)
        {
            var query = _context.DocumentCategories.Where(c => c.FacilityId == facilityId);
            if (!includeInactive) query = query.Where(c => c.IsActive);

            return await query.Select(c => MapToDTO(c)).ToListAsync();
        }

        public async Task UpdateCategoryAsync(string facilityId, string categoryCode, DocumentCategoryDTO updates)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.FacilityId == facilityId && c.CategoryCode == categoryCode);
            if (category == null) throw new Exception("Category not found");

            category.CategoryName = updates.CategoryName;
            category.Description = updates.Description;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string facilityId, string categoryCode)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.FacilityId == facilityId && c.CategoryCode == categoryCode);
            if (category == null) throw new Exception("Category not found");

            category.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetDocumentCountByCategoryAsync(string facilityId, string categoryCode)
        {
            return await _context.Documents
                .CountAsync(d => d.FacilityId == facilityId && d.DocumentCategory == categoryCode);
        }

        public async Task SetDefaultAccessLevelAsync(string facilityId, string categoryCode, string accessLevel, string[]? roles = null)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.FacilityId == facilityId && c.CategoryCode == categoryCode);
            if (category == null) throw new Exception("Category not found");

            category.DefaultAccessLevel = accessLevel;
            await _context.SaveChangesAsync();
        }

        public async Task SetDefaultRetentionAsync(string facilityId, string categoryCode, int retentionDays, string reason)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.FacilityId == facilityId && c.CategoryCode == categoryCode);
            if (category == null) throw new Exception("Category not found");

            category.DefaultRetentionDays = retentionDays;
            await _context.SaveChangesAsync();
        }

        private DocumentCategoryDTO MapToDTO(DocumentCategory category)
        {
            return new DocumentCategoryDTO
            {
                CategoryName = category.CategoryName,
                CategoryCode = category.CategoryCode,
                Description = category.Description,
                DocumentCount = _context.Documents.Count(d => d.DocumentCategory == category.CategoryCode),
                IsActive = category.IsActive,
                DefaultClassificationLevel = category.DefaultClassificationLevel,
                DefaultRetentionDays = category.DefaultRetentionDays
            };
        }
    }

    public class DocumentAccessService : IDocumentAccessService
    {
        private readonly ApplicationDbContext _context;

        public DocumentAccessService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAccessAsync(string facilityId, long documentId, string userId, string accessType, string reason, string ipAddress, string deviceInfo)
        {
            var access = new DocumentAccess
            {
                FacilityId = facilityId,
                DocumentId = documentId,
                AccessedByUserId = userId,
                AccessType = accessType,
                AccessedDate = DateTime.UtcNow,
                AccessReason = reason,
                IPAddress = ipAddress,
                DeviceInfo = deviceInfo,
                WasAuthorized = true
            };

            _context.DocumentAccesses.Add(access);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocumentAccessDTO>> GetAccessLogAsync(long documentId, string facilityId, int pageNumber, int pageSize)
        {
            return await _context.DocumentAccesses
                .Where(a => a.DocumentId == documentId && a.FacilityId == facilityId)
                .OrderByDescending(a => a.AccessedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => MapToDTO(a))
                .ToListAsync();
        }

        public async Task<List<DocumentAccessDTO>> GetUserAccessHistoryAsync(string facilityId, string userId, int pageNumber, int pageSize)
        {
            return await _context.DocumentAccesses
                .Where(a => a.FacilityId == facilityId && a.AccessedByUserId == userId)
                .OrderByDescending(a => a.AccessedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => MapToDTO(a))
                .ToListAsync();
        }

        public async Task<bool> CanUserAccessDocumentAsync(string facilityId, long documentId, string userId, string? requiredRole = null)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) return false;

            if (document.IsPublic) return true;
            if (document.AllowedUserIds?.Contains(userId) == true) return true;
            // TODO: Check roles

            return false;
        }

        public async Task<bool> CanUserDownloadAsync(string facilityId, long documentId, string userId)
        {
            return await CanUserAccessDocumentAsync(facilityId, documentId, userId);
        }

        public async Task<bool> CanUserPrintAsync(string facilityId, long documentId, string userId)
        {
            return await CanUserAccessDocumentAsync(facilityId, documentId, userId);
        }

        public async Task<bool> CanUserShareAsync(string facilityId, long documentId, string userId)
        {
            return await CanUserAccessDocumentAsync(facilityId, documentId, userId);
        }

        public async Task<bool> CanUserApproveAsync(string facilityId, long documentId, string userId)
        {
            // TODO: Check if user has approval role
            return true;
        }

        public async Task<List<DocumentAccessDTO>> GetAnomalousAccessesAsync(string facilityId, int pageNumber = 1, int pageSize = 50)
        {
            return await _context.DocumentAccesses
                .Where(a => a.FacilityId == facilityId && a.IsAnomalousAccess)
                .OrderByDescending(a => a.AccessedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => MapToDTO(a))
                .ToListAsync();
        }

        public async Task<List<DocumentAccessDTO>> GetFlaggedAccessesAsync(string facilityId, int pageNumber = 1, int pageSize = 50)
        {
            return await _context.DocumentAccesses
                .Where(a => a.FacilityId == facilityId && a.FlaggedForReview)
                .OrderByDescending(a => a.AccessedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => MapToDTO(a))
                .ToListAsync();
        }

        public async Task FlagAccessForReviewAsync(long accessLogId, string facilityId, string flagReason)
        {
            var access = await _context.DocumentAccesses.FirstOrDefaultAsync(a => a.Id == accessLogId && a.FacilityId == facilityId);
            if (access == null) throw new Exception("Access log not found");

            access.FlaggedForReview = true;
            access.FlagReason = flagReason;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetAccessCountAsync(long documentId, string facilityId)
        {
            return await _context.DocumentAccesses.CountAsync(a => a.DocumentId == documentId && a.FacilityId == facilityId);
        }

        public async Task<int> GetDownloadCountAsync(long documentId, string facilityId)
        {
            return await _context.DocumentAccesses
                .CountAsync(a => a.DocumentId == documentId && a.FacilityId == facilityId && a.WasDownloaded);
        }

        public async Task<int> GetPrintCountAsync(long documentId, string facilityId)
        {
            return await _context.DocumentAccesses
                .CountAsync(a => a.DocumentId == documentId && a.FacilityId == facilityId && a.WasPrinted);
        }

        public async Task<Dictionary<string, int>> GetAccessTypeDistributionAsync(string facilityId, DateTime fromDate, DateTime toDate)
        {
            return await _context.DocumentAccesses
                .Where(a => a.FacilityId == facilityId && a.AccessedDate >= fromDate && a.AccessedDate <= toDate)
                .GroupBy(a => a.AccessType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Type ?? "Unknown", x => x.Count);
        }

        public async Task<List<string>> GetMostAccessedDocumentsAsync(string facilityId, int topCount = 10)
        {
            return await _context.DocumentAccesses
                .Where(a => a.FacilityId == facilityId)
                .GroupBy(a => a.DocumentId)
                .OrderByDescending(g => g.Count())
                .Take(topCount)
                .Select(g => g.Key.ToString())
                .ToListAsync();
        }

        private DocumentAccessDTO MapToDTO(DocumentAccess access)
        {
            return new DocumentAccessDTO
            {
                Id = access.Id,
                DocumentId = access.DocumentId,
                AccessedByUserId = access.AccessedByUserId,
                AccessType = access.AccessType,
                AccessedDate = access.AccessedDate,
                AccessReason = access.AccessReason,
                IPAddress = access.IPAddress,
                DurationSeconds = access.DurationSeconds,
                AccessViolated = access.AccessViolated
            };
        }
    }

    public class DocumentRetentionService : IDocumentRetentionService
    {
        private readonly ApplicationDbContext _context;

        public DocumentRetentionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentRetentionPolicyDTO> CreatePolicyAsync(string facilityId, string policyName, string documentType, int retentionDays)
        {
            var policy = new DocumentRetention
            {
                FacilityId = facilityId,
                PolicyName = policyName,
                DocumentType = documentType,
                RetentionDays = retentionDays,
                IsActive = true,
                Status = "Active"
            };

            _context.DocumentRetentions.Add(policy);
            await _context.SaveChangesAsync();

            return MapToDTO(policy);
        }

        public async Task<DocumentRetentionPolicyDTO> GetPolicyByIdAsync(long policyId, string facilityId)
        {
            var policy = await _context.DocumentRetentions
                .FirstOrDefaultAsync(p => p.Id == policyId && p.FacilityId == facilityId);
            if (policy == null) throw new Exception("Policy not found");

            return MapToDTO(policy);
        }

        public async Task<List<DocumentRetentionPolicyDTO>> GetPoliciesByFacilityAsync(string facilityId, bool includeInactive = false)
        {
            var query = _context.DocumentRetentions.Where(p => p.FacilityId == facilityId);
            if (!includeInactive) query = query.Where(p => p.IsActive);

            return await query.Select(p => MapToDTO(p)).ToListAsync();
        }

        public async Task<List<DocumentRetentionPolicyDTO>> GetPoliciesByDocumentTypeAsync(string facilityId, string documentType)
        {
            return await _context.DocumentRetentions
                .Where(p => p.FacilityId == facilityId && p.DocumentType == documentType && p.IsActive)
                .Select(p => MapToDTO(p))
                .ToListAsync();
        }

        public async Task UpdatePolicyAsync(long policyId, string facilityId, DocumentRetentionPolicyDTO updates)
        {
            var policy = await _context.DocumentRetentions
                .FirstOrDefaultAsync(p => p.Id == policyId && p.FacilityId == facilityId);
            if (policy == null) throw new Exception("Policy not found");

            await _context.SaveChangesAsync();
        }

        public async Task DeletePolicyAsync(long policyId, string facilityId)
        {
            var policy = await _context.DocumentRetentions
                .FirstOrDefaultAsync(p => p.Id == policyId && p.FacilityId == facilityId);
            if (policy == null) throw new Exception("Policy not found");

            _context.DocumentRetentions.Remove(policy);
            await _context.SaveChangesAsync();
        }

        public async Task ActivatePolicyAsync(long policyId, string facilityId)
        {
            var policy = await _context.DocumentRetentions
                .FirstOrDefaultAsync(p => p.Id == policyId && p.FacilityId == facilityId);
            if (policy == null) throw new Exception("Policy not found");

            policy.IsActive = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeactivatePolicyAsync(long policyId, string facilityId)
        {
            var policy = await _context.DocumentRetentions
                .FirstOrDefaultAsync(p => p.Id == policyId && p.FacilityId == facilityId);
            if (policy == null) throw new Exception("Policy not found");

            policy.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task PlaceLegalHoldAsync(long documentId, string facilityId, string reason, DateTime? expiryDate = null)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            await _context.SaveChangesAsync();
        }

        public async Task RemoveLegalHoldAsync(long documentId, string facilityId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.FacilityId == facilityId);
            if (document == null) throw new Exception("Document not found");

            await _context.SaveChangesAsync();
        }

        public async Task<List<long>> GetDocumentsUnderLegalHoldAsync(string facilityId)
        {
            return new List<long>(); // TODO: Implement
        }

        public async Task<List<DocumentRetentionPolicyDTO>> GetComplianceRequiredPoliciesAsync(string facilityId)
        {
            return await _context.DocumentRetentions
                .Where(p => p.FacilityId == facilityId && p.IsComplianceRequired && p.IsActive)
                .Select(p => MapToDTO(p))
                .ToListAsync();
        }

        public async Task<List<long>> GetScheduledForArchivalAsync(string facilityId)
        {
            return new List<long>(); // TODO: Implement
        }

        public async Task<List<long>> GetScheduledForDeletionAsync(string facilityId)
        {
            return new List<long>(); // TODO: Implement
        }

        public async Task<List<long>> GetExpiredDocumentsAsync(string facilityId)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.ExpiryDate.HasValue && d.ExpiryDate <= DateTime.UtcNow)
                .Select(d => d.Id)
                .ToListAsync();
        }

        public async Task ExecuteArchivalAsync(string facilityId, List<long> documentIds, string executedByUserId)
        {
            // TODO: Implement archival execution
            await Task.CompletedTask;
        }

        public async Task ExecuteDeletionAsync(string facilityId, List<long> documentIds, string executedByUserId, string reason)
        {
            // TODO: Implement deletion execution
            await Task.CompletedTask;
        }

        public async Task AddExceptionAsync(long policyId, string facilityId, long documentId, string reason)
        {
            // TODO: Implement exception logic
            await Task.CompletedTask;
        }

        public async Task RemoveExceptionAsync(long policyId, string facilityId, long documentId)
        {
            // TODO: Implement exception removal
            await Task.CompletedTask;
        }

        public async Task<List<long>> GetDocumentsRequiringDeletionNotificationAsync(string facilityId, int daysUntilDeletion = 7)
        {
            var notificationDate = DateTime.UtcNow.AddDays(daysUntilDeletion);
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.ScheduledDeletionDate.HasValue && d.ScheduledDeletionDate <= notificationDate)
                .Select(d => d.Id)
                .ToListAsync();
        }

        public async Task SendDeletionNotificationAsync(long documentId, string facilityId, List<string> recipientUserIds)
        {
            // TODO: Send notifications
            await Task.CompletedTask;
        }

        public async Task<Dictionary<string, int>> GetRetentionStatisticsAsync(string facilityId)
        {
            var stats = new Dictionary<string, int>
            {
                { "TotalDocuments", await _context.Documents.CountAsync(d => d.FacilityId == facilityId) },
                { "ArchivedDocuments", await _context.Documents.CountAsync(d => d.FacilityId == facilityId && d.IsArchived) },
                { "ExpiringDocuments", await _context.Documents.CountAsync(d => d.FacilityId == facilityId && d.ExpiryDate <= DateTime.UtcNow.AddDays(30)) }
            };

            return stats;
        }

        public async Task<List<(long DocumentId, DateTime ScheduledDeletionDate)>> GetDeletionScheduleAsync(string facilityId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Documents
                .Where(d => d.FacilityId == facilityId && d.ScheduledDeletionDate.HasValue && d.ScheduledDeletionDate >= fromDate && d.ScheduledDeletionDate <= toDate)
                .Select(d => new ValueTuple<long, DateTime>(d.Id, d.ScheduledDeletionDate!.Value))
                .ToListAsync();
        }

        public async Task<int> GetComplianceScoreAsync(string facilityId)
        {
            // TODO: Calculate compliance score
            return 100;
        }

        private DocumentRetentionPolicyDTO MapToDTO(DocumentRetention policy)
        {
            return new DocumentRetentionPolicyDTO
            {
                Id = policy.Id,
                PolicyName = policy.PolicyName,
                DocumentType = policy.DocumentType,
                RetentionDays = policy.RetentionDays,
                EnableAutomaticDeletion = policy.EnableAutomaticDeletion,
                ComplianceStandard = policy.ComplianceStandard,
                IsActive = policy.IsActive,
                DocumentsAffected = policy.DocumentsAffected,
                LastExecutionDate = policy.LastExecutionDate
            };
        }
    }
}
