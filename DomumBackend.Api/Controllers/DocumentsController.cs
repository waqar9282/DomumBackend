using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using DomumBackend.Application.DTOs;
using DomumBackend.Application.Commands.Documents;
using DomumBackend.Application.Queries.Documents;
using DomumBackend.Application.Common.Interfaces;
using System.Security.Claims;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDocumentService _documentService;
        private readonly IDocumentAccessService _accessService;

        public DocumentsController(IMediator mediator, IDocumentService documentService, IDocumentAccessService accessService)
        {
            _mediator = mediator;
            _documentService = documentService;
            _accessService = accessService;
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";
        }

        private string GetFacilityId()
        {
            return User.FindFirst("facility_id")?.Value ?? "default";
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(
            [FromForm] IFormFile file,
            [FromQuery] string documentType = "General",
            [FromQuery] string category = "Uncategorized",
            [FromQuery] string title = null,
            [FromQuery] string description = null)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided");

            var facilityId = GetFacilityId();
            var userId = GetUserId();

            var request = new DocumentUploadRequestDTO
            {
                FileName = file.FileName,
                DocumentType = documentType,
                DocumentCategory = category,
                DocumentTitle = title ?? file.FileName,
                Description = description
            };

            using (var stream = file.OpenReadStream())
            {
                var command = new UploadDocumentCommand
                {
                    FacilityId = facilityId,
                    UserId = userId,
                    Metadata = request,
                    FileStream = stream
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
        }

        [HttpPost("bulk-upload")]
        public async Task<IActionResult> BulkUploadDocuments([FromForm] IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files provided");

            var facilityId = GetFacilityId();
            var userId = GetUserId();
            var fileList = new List<(DocumentUploadRequestDTO, Stream)>();

            foreach (var file in files)
            {
                var metadata = new DocumentUploadRequestDTO
                {
                    FileName = file.FileName,
                    DocumentType = "General",
                    DocumentCategory = "Uncategorized"
                };
                fileList.Add((metadata, file.OpenReadStream()));
            }

            var command = new BulkUploadDocumentsCommand
            {
                FacilityId = facilityId,
                UserId = userId,
                Files = fileList
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetDocument(long documentId)
        {
            var facilityId = GetFacilityId();
            var userId = GetUserId();

            // Check access
            var hasAccess = await _accessService.CanUserAccessDocumentAsync(facilityId, documentId, userId);
            if (!hasAccess)
                return Forbid();

            var query = new GetDocumentByIdQuery
            {
                DocumentId = documentId,
                FacilityId = facilityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var facilityId = GetFacilityId();

            var query = new GetDocumentsByFacilityQuery
            {
                FacilityId = facilityId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("by-type/{documentType}")]
        public async Task<IActionResult> GetDocumentsByType(string documentType, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var facilityId = GetFacilityId();

            var query = new GetDocumentsByTypeQuery
            {
                FacilityId = facilityId,
                DocumentType = documentType,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchDocuments([FromQuery] string q, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if (string.IsNullOrEmpty(q))
                return BadRequest("Search query is required");

            var facilityId = GetFacilityId();

            var query = new SearchDocumentsQuery
            {
                FacilityId = facilityId,
                SearchQuery = q,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("expiring")]
        public async Task<IActionResult> GetExpiringDocuments([FromQuery] int daysUntilExpiry = 30)
        {
            var facilityId = GetFacilityId();

            var query = new GetExpiringDocumentsQuery
            {
                FacilityId = facilityId,
                DaysUntilExpiry = daysUntilExpiry
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{documentId}/versions")]
        public async Task<IActionResult> GetDocumentVersions(long documentId)
        {
            var facilityId = GetFacilityId();

            var query = new GetDocumentVersionsQuery
            {
                DocumentId = documentId,
                FacilityId = facilityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{documentId}/access-log")]
        [Authorize(Roles = "Admin,Auditor")]
        public async Task<IActionResult> GetDocumentAccessLog(long documentId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var facilityId = GetFacilityId();

            var query = new GetDocumentAccessLogQuery
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{documentId}/metadata")]
        public async Task<IActionResult> UpdateDocumentMetadata(long documentId, [FromBody] DocumentMetadataUpdateDTO metadata)
        {
            var facilityId = GetFacilityId();

            var command = new UpdateDocumentMetadataCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                Metadata = metadata
            };

            await _mediator.Send(command);
            return Ok(new { message = "Document metadata updated successfully" });
        }

        [HttpPut("{documentId}/access-level")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAccessLevel(long documentId, [FromBody] SetAccessLevelRequest request)
        {
            var facilityId = GetFacilityId();

            var command = new SetDocumentAccessLevelCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                AccessLevel = request.AccessLevel,
                AllowedRoles = request.AllowedRoles,
                AllowedUserIds = request.AllowedUserIds
            };

            await _mediator.Send(command);
            return Ok(new { message = "Access level updated successfully" });
        }

        [HttpPost("{documentId}/share")]
        public async Task<IActionResult> ShareDocument(long documentId, [FromBody] DocumentShareDTO shareInfo)
        {
            var facilityId = GetFacilityId();
            var userId = GetUserId();

            var command = new ShareDocumentCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                UserId = userId,
                ShareInfo = shareInfo
            };

            await _mediator.Send(command);
            return Ok(new { message = "Document shared successfully" });
        }

        [HttpDelete("{documentId}/revoke/{revokedUserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RevokeAccess(long documentId, string revokedUserId)
        {
            var facilityId = GetFacilityId();

            var command = new RevokeDocumentAccessCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                RevokedUserId = revokedUserId
            };

            await _mediator.Send(command);
            return Ok(new { message = "Access revoked successfully" });
        }

        [HttpPut("{documentId}/approve")]
        [Authorize(Roles = "Admin,Approver")]
        public async Task<IActionResult> ApproveDocument(long documentId, [FromBody] ApprovalRequest request)
        {
            var facilityId = GetFacilityId();
            var userId = GetUserId();

            var command = new ApproveDocumentCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                ApproverId = userId,
                ApprovalNotes = request.ApprovalNotes
            };

            await _mediator.Send(command);
            return Ok(new { message = "Document approved successfully" });
        }

        [HttpPut("{documentId}/reject")]
        [Authorize(Roles = "Admin,Approver")]
        public async Task<IActionResult> RejectDocument(long documentId, [FromBody] RejectionRequest request)
        {
            var facilityId = GetFacilityId();
            var userId = GetUserId();

            var command = new RejectDocumentCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                RejectorId = userId,
                RejectionReason = request.RejectionReason
            };

            await _mediator.Send(command);
            return Ok(new { message = "Document rejected successfully" });
        }

        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(long documentId, [FromQuery] bool permanent = false, [FromQuery] string reason = null)
        {
            var facilityId = GetFacilityId();
            var userId = GetUserId();

            var command = new DeleteDocumentCommand
            {
                DocumentId = documentId,
                FacilityId = facilityId,
                UserId = userId,
                Reason = reason,
                PermanentDelete = permanent
            };

            await _mediator.Send(command);
            return Ok(new { message = "Document deleted successfully" });
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var facilityId = GetFacilityId();

            var query = new GetDocumentCategoriesQuery
            {
                FacilityId = facilityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("retention/policies")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRetentionPolicies()
        {
            var facilityId = GetFacilityId();

            var query = new GetRetentionScheduleQuery
            {
                FacilityId = facilityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("anomalies")]
        [Authorize(Roles = "Admin,Auditor")]
        public async Task<IActionResult> GetAnomalousAccesses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var facilityId = GetFacilityId();

            var query = new GetAnomalousAccessesQuery
            {
                FacilityId = facilityId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }

    public class SetAccessLevelRequest
    {
        public string AccessLevel { get; set; }
        public string[] AllowedRoles { get; set; }
        public string[] AllowedUserIds { get; set; }
    }

    public class ApprovalRequest
    {
        public string ApprovalNotes { get; set; }
    }

    public class RejectionRequest
    {
        public string RejectionReason { get; set; }
    }
}
