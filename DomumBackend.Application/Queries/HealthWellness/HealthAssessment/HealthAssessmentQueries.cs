using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.HealthAssessment
{
    public class GetHealthAssessmentByIdQuery : IRequest<HealthAssessmentResponseDTO>
    {
        public string Id { get; set; }
        public GetHealthAssessmentByIdQuery(string id) => Id = id;
    }

    public class GetHealthAssessmentByIdQueryHandler : IRequestHandler<GetHealthAssessmentByIdQuery, HealthAssessmentResponseDTO>
    {
        private readonly IHealthAssessmentService _service;
        public GetHealthAssessmentByIdQueryHandler(IHealthAssessmentService service) => _service = service;
        public async Task<HealthAssessmentResponseDTO> Handle(GetHealthAssessmentByIdQuery r, CancellationToken ct)
            => await _service.GetHealthAssessmentByIdAsync(r.Id, ct);
    }

    public class GetHealthAssessmentsByYoungPersonQuery : IRequest<List<HealthAssessmentResponseDTO>>
    {
        public string YoungPersonId { get; set; }
        public GetHealthAssessmentsByYoungPersonQuery(string yp) => YoungPersonId = yp;
    }

    public class GetHealthAssessmentsByYoungPersonQueryHandler : IRequestHandler<GetHealthAssessmentsByYoungPersonQuery, List<HealthAssessmentResponseDTO>>
    {
        private readonly IHealthAssessmentService _service;
        public GetHealthAssessmentsByYoungPersonQueryHandler(IHealthAssessmentService s) => _service = s;
        public async Task<List<HealthAssessmentResponseDTO>> Handle(GetHealthAssessmentsByYoungPersonQuery r, CancellationToken ct)
            => await _service.GetHealthAssessmentsByYoungPersonAsync(r.YoungPersonId, ct);
    }

    public class GetHealthAssessmentsByTypeQuery : IRequest<List<HealthAssessmentResponseDTO>>
    {
        public string YoungPersonId { get; set; }
        public int AssessmentType { get; set; }
        public GetHealthAssessmentsByTypeQuery(string yp, int at) { YoungPersonId = yp; AssessmentType = at; }
    }

    public class GetHealthAssessmentsByTypeQueryHandler : IRequestHandler<GetHealthAssessmentsByTypeQuery, List<HealthAssessmentResponseDTO>>
    {
        private readonly IHealthAssessmentService _service;
        public GetHealthAssessmentsByTypeQueryHandler(IHealthAssessmentService s) => _service = s;
        public async Task<List<HealthAssessmentResponseDTO>> Handle(GetHealthAssessmentsByTypeQuery r, CancellationToken ct)
            => await _service.GetHealthAssessmentsByTypeAsync(r.YoungPersonId, r.AssessmentType, ct);
    }
}
