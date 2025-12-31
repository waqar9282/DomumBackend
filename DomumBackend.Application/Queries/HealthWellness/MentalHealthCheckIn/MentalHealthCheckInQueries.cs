using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.MentalHealthCheckIn
{
    public class GetMentalHealthCheckInByIdQuery : IRequest<MentalHealthCheckInResponseDTO>
    {
        public string Id { get; set; }
        public GetMentalHealthCheckInByIdQuery(string id) => Id = id;
    }

    public class GetMentalHealthCheckInByIdQueryHandler : IRequestHandler<GetMentalHealthCheckInByIdQuery, MentalHealthCheckInResponseDTO>
    {
        private readonly IMentalHealthCheckInService _service;
        public GetMentalHealthCheckInByIdQueryHandler(IMentalHealthCheckInService service) => _service = service;
        public async Task<MentalHealthCheckInResponseDTO> Handle(GetMentalHealthCheckInByIdQuery r, CancellationToken ct)
            => await _service.GetMentalHealthCheckInByIdAsync(r.Id, ct);
    }

    public class GetMentalHealthCheckInsByYoungPersonQuery : IRequest<List<MentalHealthCheckInResponseDTO>>
    {
        public string YoungPersonId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public GetMentalHealthCheckInsByYoungPersonQuery(string yp, DateTime? fd = null, DateTime? td = null)
        { YoungPersonId = yp; FromDate = fd; ToDate = td; }
    }

    public class GetMentalHealthCheckInsByYoungPersonQueryHandler : IRequestHandler<GetMentalHealthCheckInsByYoungPersonQuery, List<MentalHealthCheckInResponseDTO>>
    {
        private readonly IMentalHealthCheckInService _service;
        public GetMentalHealthCheckInsByYoungPersonQueryHandler(IMentalHealthCheckInService s) => _service = s;
        public async Task<List<MentalHealthCheckInResponseDTO>> Handle(GetMentalHealthCheckInsByYoungPersonQuery r, CancellationToken ct)
            => await _service.GetMentalHealthCheckInsByYoungPersonAsync(r.YoungPersonId, r.FromDate, r.ToDate, ct);
    }

    public class GetRecentMentalHealthCheckInsQuery : IRequest<List<MentalHealthCheckInResponseDTO>>
    {
        public string FacilityId { get; set; }
        public int Days { get; set; } = 7;
        public GetRecentMentalHealthCheckInsQuery(string fid, int d = 7) { FacilityId = fid; Days = d; }
    }

    public class GetRecentMentalHealthCheckInsQueryHandler : IRequestHandler<GetRecentMentalHealthCheckInsQuery, List<MentalHealthCheckInResponseDTO>>
    {
        private readonly IMentalHealthCheckInService _service;
        public GetRecentMentalHealthCheckInsQueryHandler(IMentalHealthCheckInService s) => _service = s;
        public async Task<List<MentalHealthCheckInResponseDTO>> Handle(GetRecentMentalHealthCheckInsQuery r, CancellationToken ct)
            => await _service.GetRecentMentalHealthCheckInsAsync(r.FacilityId, r.Days, ct);
    }
}
