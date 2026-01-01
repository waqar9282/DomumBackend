using MediatR;
using DomumBackend.Application.DTOs;
using DomumBackend.Application.Common.Interfaces;

namespace DomumBackend.Application.Commands.Documents
{
    public class UploadDocumentCommand : IRequest<DocumentUploadResponseDTO>
    {
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
        public DocumentUploadRequestDTO? Metadata { get; set; }
        public Stream? FileStream { get; set; }
    }

    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, DocumentUploadResponseDTO>
    {
        private readonly IDocumentService _documentService;

        public UploadDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<DocumentUploadResponseDTO> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            return await _documentService.UploadDocumentAsync(request.FacilityId!, request.UserId!, request.Metadata!, request.FileStream!);
        }
    }

    public class BulkUploadDocumentsCommand : IRequest<BulkDocumentUploadResponseDTO>
    {
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
        public List<(DocumentUploadRequestDTO metadata, Stream fileStream)>? Files { get; set; }
    }

    public class BulkUploadDocumentsCommandHandler : IRequestHandler<BulkUploadDocumentsCommand, BulkDocumentUploadResponseDTO>
    {
        private readonly IDocumentService _documentService;

        public BulkUploadDocumentsCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<BulkDocumentUploadResponseDTO> Handle(BulkUploadDocumentsCommand request, CancellationToken cancellationToken)
        {
            return await _documentService.BulkUploadDocumentsAsync(request.FacilityId!, request.UserId!, request.Files!);
        }
    }

    public class UpdateDocumentMetadataCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public DocumentMetadataUpdateDTO? Metadata { get; set; }
    }

    public class UpdateDocumentMetadataCommandHandler : IRequestHandler<UpdateDocumentMetadataCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public UpdateDocumentMetadataCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(UpdateDocumentMetadataCommand request, CancellationToken cancellationToken)
        {
            await _documentService.UpdateDocumentMetadataAsync(request.DocumentId, request.FacilityId!, request.Metadata!);
            return true;
        }
    }

    public class SetDocumentAccessLevelCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? AccessLevel { get; set; }
        public string[]? AllowedRoles { get; set; }
        public string[]? AllowedUserIds { get; set; }
    }

    public class SetDocumentAccessLevelCommandHandler : IRequestHandler<SetDocumentAccessLevelCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public SetDocumentAccessLevelCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(SetDocumentAccessLevelCommand request, CancellationToken cancellationToken)
        {
            await _documentService.SetAccessLevelAsync(request.DocumentId, request.FacilityId!, request.AccessLevel!, request.AllowedRoles, request.AllowedUserIds);
            return true;
        }
    }

    public class ShareDocumentCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
        public DocumentShareDTO? ShareInfo { get; set; }
    }

    public class ShareDocumentCommandHandler : IRequestHandler<ShareDocumentCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public ShareDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(ShareDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.ShareDocumentAsync(request.DocumentId, request.FacilityId!, request.UserId!, request.ShareInfo!);
            return true;
        }
    }

    public class RevokeDocumentAccessCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? RevokedUserId { get; set; }
    }

    public class RevokeDocumentAccessCommandHandler : IRequestHandler<RevokeDocumentAccessCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public RevokeDocumentAccessCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(RevokeDocumentAccessCommand request, CancellationToken cancellationToken)
        {
            await _documentService.RevokeAccessAsync(request.DocumentId, request.FacilityId!, request.RevokedUserId!);
            return true;
        }
    }

    public class ApproveDocumentCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? ApproverId { get; set; }
        public string? ApprovalNotes { get; set; }
    }

    public class ApproveDocumentCommandHandler : IRequestHandler<ApproveDocumentCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public ApproveDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(ApproveDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.ApproveDocumentAsync(request.DocumentId, request.FacilityId!, request.ApproverId!, request.ApprovalNotes!);
            return true;
        }
    }

    public class RejectDocumentCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? RejectorId { get; set; }
        public string? RejectionReason { get; set; }
    }

    public class RejectDocumentCommandHandler : IRequestHandler<RejectDocumentCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public RejectDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(RejectDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.RejectDocumentAsync(request.DocumentId, request.FacilityId!, request.RejectorId!, request.RejectionReason!);
            return true;
        }
    }

    public class DeleteDocumentCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
        public string? Reason { get; set; }
        public bool PermanentDelete { get; set; } = false;
    }

    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public DeleteDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            if (request.PermanentDelete)
            {
                await _documentService.PermanentlyDeleteDocumentAsync(request.DocumentId, request.FacilityId!, request.UserId!, request.Reason!);
            }
            else
            {
                await _documentService.SoftDeleteDocumentAsync(request.DocumentId, request.FacilityId!, request.UserId!, request.Reason!);
            }
            return true;
        }
    }

    public class CreateDocumentVersionCommand : IRequest<DocumentVersionDTO>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
        public Stream? FileStream { get; set; }
        public string? VersionNotes { get; set; }
    }

    public class CreateDocumentVersionCommandHandler : IRequestHandler<CreateDocumentVersionCommand, DocumentVersionDTO>
    {
        private readonly IDocumentService _documentService;

        public CreateDocumentVersionCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<DocumentVersionDTO> Handle(CreateDocumentVersionCommand request, CancellationToken cancellationToken)
        {
            return await _documentService.CreateVersionAsync(request.DocumentId, request.FacilityId!, request.UserId!, request.FileStream!, request.VersionNotes);
        }
    }

    public class ArchiveDocumentCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
    }

    public class ArchiveDocumentCommandHandler : IRequestHandler<ArchiveDocumentCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public ArchiveDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(ArchiveDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.ArchiveDocumentAsync(request.DocumentId, request.FacilityId!, request.UserId!);
            return true;
        }
    }

    public class RestoreDocumentCommand : IRequest<bool>
    {
        public long DocumentId { get; set; }
        public string? FacilityId { get; set; }
        public string? UserId { get; set; }
    }

    public class RestoreDocumentCommandHandler : IRequestHandler<RestoreDocumentCommand, bool>
    {
        private readonly IDocumentService _documentService;

        public RestoreDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<bool> Handle(RestoreDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.RestoreDocumentAsync(request.DocumentId, request.FacilityId!, request.UserId!);
            return true;
        }
    }
}

