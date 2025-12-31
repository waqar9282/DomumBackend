using MediatR;
using DomumBackend.Application.DTOs;
using DomumBackend.Application.Common.Interfaces;

namespace DomumBackend.Application.Queries.Documents
{
    public class GetDocumentByIdQuery : IRequest<DocumentDetailDTO>
    {
        public long DocumentId { get; set; }
        public string FacilityId { get; set; }
    }

    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, DocumentDetailDTO>
    {
        private readonly IDocumentService _documentService;

        public GetDocumentByIdQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<DocumentDetailDTO> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetDocumentByIdAsync(request.DocumentId, request.FacilityId);
        }
    }

    public class GetDocumentsByFacilityQuery : IRequest<List<DocumentDTO>>
    {
        public string FacilityId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class GetDocumentsByFacilityQueryHandler : IRequestHandler<GetDocumentsByFacilityQuery, List<DocumentDTO>>
    {
        private readonly IDocumentService _documentService;

        public GetDocumentsByFacilityQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentDTO>> Handle(GetDocumentsByFacilityQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetDocumentsByFacilityAsync(request.FacilityId, request.PageNumber, request.PageSize);
        }
    }

    public class GetDocumentsByTypeQuery : IRequest<List<DocumentDTO>>
    {
        public string FacilityId { get; set; }
        public string DocumentType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class GetDocumentsByTypeQueryHandler : IRequestHandler<GetDocumentsByTypeQuery, List<DocumentDTO>>
    {
        private readonly IDocumentService _documentService;

        public GetDocumentsByTypeQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentDTO>> Handle(GetDocumentsByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetDocumentsByTypeAsync(request.FacilityId, request.DocumentType, request.PageNumber, request.PageSize);
        }
    }

    public class SearchDocumentsQuery : IRequest<List<DocumentSearchResultDTO>>
    {
        public string FacilityId { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class SearchDocumentsQueryHandler : IRequestHandler<SearchDocumentsQuery, List<DocumentSearchResultDTO>>
    {
        private readonly IDocumentService _documentService;

        public SearchDocumentsQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentSearchResultDTO>> Handle(SearchDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.SearchDocumentsAsync(request.FacilityId, request.SearchQuery, request.PageNumber, request.PageSize);
        }
    }

    public class GetDocumentVersionsQuery : IRequest<List<DocumentVersionDTO>>
    {
        public long DocumentId { get; set; }
        public string FacilityId { get; set; }
    }

    public class GetDocumentVersionsQueryHandler : IRequestHandler<GetDocumentVersionsQuery, List<DocumentVersionDTO>>
    {
        private readonly IDocumentService _documentService;

        public GetDocumentVersionsQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentVersionDTO>> Handle(GetDocumentVersionsQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetDocumentVersionsAsync(request.DocumentId, request.FacilityId);
        }
    }

    public class GetDocumentAccessLogQuery : IRequest<List<DocumentAccessDTO>>
    {
        public long DocumentId { get; set; }
        public string FacilityId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class GetDocumentAccessLogQueryHandler : IRequestHandler<GetDocumentAccessLogQuery, List<DocumentAccessDTO>>
    {
        private readonly IDocumentAccessService _accessService;

        public GetDocumentAccessLogQueryHandler(IDocumentAccessService accessService)
        {
            _accessService = accessService;
        }

        public async Task<List<DocumentAccessDTO>> Handle(GetDocumentAccessLogQuery request, CancellationToken cancellationToken)
        {
            return await _accessService.GetAccessLogAsync(request.DocumentId, request.FacilityId, request.PageNumber, request.PageSize);
        }
    }

    public class GetExpiringDocumentsQuery : IRequest<List<DocumentDTO>>
    {
        public string FacilityId { get; set; }
        public int DaysUntilExpiry { get; set; } = 30;
    }

    public class GetExpiringDocumentsQueryHandler : IRequestHandler<GetExpiringDocumentsQuery, List<DocumentDTO>>
    {
        private readonly IDocumentService _documentService;

        public GetExpiringDocumentsQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentDTO>> Handle(GetExpiringDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetExpiringDocumentsAsync(request.FacilityId, request.DaysUntilExpiry);
        }
    }

    public class GetDocumentCategoriesQuery : IRequest<List<DocumentCategoryDTO>>
    {
        public string FacilityId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }

    public class GetDocumentCategoriesQueryHandler : IRequestHandler<GetDocumentCategoriesQuery, List<DocumentCategoryDTO>>
    {
        private readonly IDocumentCategoryService _categoryService;

        public GetDocumentCategoriesQueryHandler(IDocumentCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<DocumentCategoryDTO>> Handle(GetDocumentCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllCategoriesAsync(request.FacilityId, request.IncludeInactive);
        }
    }

    public class GetRetentionScheduleQuery : IRequest<List<DocumentRetentionPolicyDTO>>
    {
        public string FacilityId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }

    public class GetRetentionScheduleQueryHandler : IRequestHandler<GetRetentionScheduleQuery, List<DocumentRetentionPolicyDTO>>
    {
        private readonly IDocumentRetentionService _retentionService;

        public GetRetentionScheduleQueryHandler(IDocumentRetentionService retentionService)
        {
            _retentionService = retentionService;
        }

        public async Task<List<DocumentRetentionPolicyDTO>> Handle(GetRetentionScheduleQuery request, CancellationToken cancellationToken)
        {
            return await _retentionService.GetPoliciesByFacilityAsync(request.FacilityId, request.IncludeInactive);
        }
    }

    public class GetAnomalousAccessesQuery : IRequest<List<DocumentAccessDTO>>
    {
        public string FacilityId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class GetAnomalousAccessesQueryHandler : IRequestHandler<GetAnomalousAccessesQuery, List<DocumentAccessDTO>>
    {
        private readonly IDocumentAccessService _accessService;

        public GetAnomalousAccessesQueryHandler(IDocumentAccessService accessService)
        {
            _accessService = accessService;
        }

        public async Task<List<DocumentAccessDTO>> Handle(GetAnomalousAccessesQuery request, CancellationToken cancellationToken)
        {
            return await _accessService.GetAnomalousAccessesAsync(request.FacilityId, request.PageNumber, request.PageSize);
        }
    }
}
