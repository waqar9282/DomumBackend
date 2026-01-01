using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.PhysicalActivityLog
{
    public class GetPhysicalActivityLogByIdQuery : IRequest<PhysicalActivityLogResponseDTO>
    {
        public string? Id { get; set; }
        public GetPhysicalActivityLogByIdQuery(string id) => Id = id;
    }

    public class GetPhysicalActivityLogByIdQueryHandler : IRequestHandler<GetPhysicalActivityLogByIdQuery, PhysicalActivityLogResponseDTO>
    {
        private readonly IPhysicalActivityLogService _service;
        public GetPhysicalActivityLogByIdQueryHandler(IPhysicalActivityLogService service) => _service = service;
        public async Task<PhysicalActivityLogResponseDTO> Handle(GetPhysicalActivityLogByIdQuery r, CancellationToken ct)
            => await _service.GetPhysicalActivityLogByIdAsync(r.Id!, ct);
    }

    public class GetPhysicalActivityLogsByYoungPersonQuery : IRequest<List<PhysicalActivityLogResponseDTO>>
    {
        public string? YoungPersonId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public GetPhysicalActivityLogsByYoungPersonQuery(string yp, DateTime? fd = null, DateTime? td = null)
        { YoungPersonId = yp; FromDate = fd; ToDate = td; }
    }

    public class GetPhysicalActivityLogsByYoungPersonQueryHandler : IRequestHandler<GetPhysicalActivityLogsByYoungPersonQuery, List<PhysicalActivityLogResponseDTO>>
    {
        private readonly IPhysicalActivityLogService _service;
        public GetPhysicalActivityLogsByYoungPersonQueryHandler(IPhysicalActivityLogService s) => _service = s;
        public async Task<List<PhysicalActivityLogResponseDTO>> Handle(GetPhysicalActivityLogsByYoungPersonQuery r, CancellationToken ct)
            => await _service.GetPhysicalActivityLogsByYoungPersonAsync(r.YoungPersonId!, r.FromDate, r.ToDate, ct);
    }

    public class GetPhysicalActivityLogsByTypeQuery : IRequest<List<PhysicalActivityLogResponseDTO>>
    {
        public string? YoungPersonId { get; set; }
        public int ActivityType { get; set; }
        public GetPhysicalActivityLogsByTypeQuery(string yp, int at) { YoungPersonId = yp; ActivityType = at; }
    }

    public class GetPhysicalActivityLogsByTypeQueryHandler : IRequestHandler<GetPhysicalActivityLogsByTypeQuery, List<PhysicalActivityLogResponseDTO>>
    {
        private readonly IPhysicalActivityLogService _service;
        public GetPhysicalActivityLogsByTypeQueryHandler(IPhysicalActivityLogService s) => _service = s;
        public async Task<List<PhysicalActivityLogResponseDTO>> Handle(GetPhysicalActivityLogsByTypeQuery r, CancellationToken ct)
            => await _service.GetPhysicalActivityLogsByTypeAsync(r.YoungPersonId!, r.ActivityType, ct);
    }
}

