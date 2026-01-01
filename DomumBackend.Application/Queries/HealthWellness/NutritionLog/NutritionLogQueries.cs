using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.NutritionLog
{
    public class GetNutritionLogByIdQuery : IRequest<NutritionLogResponseDTO>
    {
        public string? Id { get; set; }
        public GetNutritionLogByIdQuery(string id) => Id = id;
    }

    public class GetNutritionLogByIdQueryHandler : IRequestHandler<GetNutritionLogByIdQuery, NutritionLogResponseDTO>
    {
        private readonly INutritionLogService _service;
        public GetNutritionLogByIdQueryHandler(INutritionLogService service) => _service = service;
        public async Task<NutritionLogResponseDTO> Handle(GetNutritionLogByIdQuery r, CancellationToken ct)
            => await _service.GetNutritionLogByIdAsync(r.Id!, ct);
    }

    public class GetNutritionLogsByYoungPersonQuery : IRequest<List<NutritionLogResponseDTO>>
    {
        public string? YoungPersonId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public GetNutritionLogsByYoungPersonQuery(string yp, DateTime? fd = null, DateTime? td = null)
        { YoungPersonId = yp; FromDate = fd; ToDate = td; }
    }

    public class GetNutritionLogsByYoungPersonQueryHandler : IRequestHandler<GetNutritionLogsByYoungPersonQuery, List<NutritionLogResponseDTO>>
    {
        private readonly INutritionLogService _service;
        public GetNutritionLogsByYoungPersonQueryHandler(INutritionLogService s) => _service = s;
        public async Task<List<NutritionLogResponseDTO>> Handle(GetNutritionLogsByYoungPersonQuery r, CancellationToken ct)
            => await _service.GetNutritionLogsByYoungPersonAsync(r.YoungPersonId!, r.FromDate, r.ToDate, ct);
    }

    public class GetNutritionLogsByFacilityQuery : IRequest<List<NutritionLogResponseDTO>>
    {
        public string? FacilityId { get; set; }
        public DateTime LogDate { get; set; }
        public GetNutritionLogsByFacilityQuery(string fid, DateTime ld) { FacilityId = fid; LogDate = ld; }
    }

    public class GetNutritionLogsByFacilityQueryHandler : IRequestHandler<GetNutritionLogsByFacilityQuery, List<NutritionLogResponseDTO>>
    {
        private readonly INutritionLogService _service;
        public GetNutritionLogsByFacilityQueryHandler(INutritionLogService s) => _service = s;
        public async Task<List<NutritionLogResponseDTO>> Handle(GetNutritionLogsByFacilityQuery r, CancellationToken ct)
            => await _service.GetNutritionLogsByFacilityAsync(r.FacilityId!, r.LogDate, ct);
    }
}

