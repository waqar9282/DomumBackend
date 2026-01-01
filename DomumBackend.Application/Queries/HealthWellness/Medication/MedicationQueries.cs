using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.Medication
{
    public class GetMedicationByIdQuery : IRequest<MedicationResponseDTO>
    {
        public string? Id { get; set; }
        public GetMedicationByIdQuery(string id) => Id = id;
    }

    public class GetMedicationByIdQueryHandler : IRequestHandler<GetMedicationByIdQuery, MedicationResponseDTO>
    {
        private readonly IMedicationService _medicationService;
        public GetMedicationByIdQueryHandler(IMedicationService medicationService) => _medicationService = medicationService;
        public async Task<MedicationResponseDTO> Handle(GetMedicationByIdQuery request, CancellationToken cancellationToken)
            => await _medicationService.GetMedicationByIdAsync(request.Id!, cancellationToken);
    }

    public class GetMedicationsByYoungPersonQuery : IRequest<List<MedicationResponseDTO>>
    {
        public string? YoungPersonId { get; set; }
        public GetMedicationsByYoungPersonQuery(string youngPersonId) => YoungPersonId = youngPersonId;
    }

    public class GetMedicationsByYoungPersonQueryHandler : IRequestHandler<GetMedicationsByYoungPersonQuery, List<MedicationResponseDTO>>
    {
        private readonly IMedicationService _medicationService;
        public GetMedicationsByYoungPersonQueryHandler(IMedicationService medicationService) => _medicationService = medicationService;
        public async Task<List<MedicationResponseDTO>> Handle(GetMedicationsByYoungPersonQuery request, CancellationToken cancellationToken)
            => await _medicationService.GetMedicationsByYoungPersonAsync(request.YoungPersonId!, cancellationToken);
    }

    public class GetActiveMedicationsByYoungPersonQuery : IRequest<List<MedicationResponseDTO>>
    {
        public string? YoungPersonId { get; set; }
        public GetActiveMedicationsByYoungPersonQuery(string youngPersonId) => YoungPersonId = youngPersonId;
    }

    public class GetActiveMedicationsByYoungPersonQueryHandler : IRequestHandler<GetActiveMedicationsByYoungPersonQuery, List<MedicationResponseDTO>>
    {
        private readonly IMedicationService _medicationService;
        public GetActiveMedicationsByYoungPersonQueryHandler(IMedicationService medicationService) => _medicationService = medicationService;
        public async Task<List<MedicationResponseDTO>> Handle(GetActiveMedicationsByYoungPersonQuery request, CancellationToken cancellationToken)
            => await _medicationService.GetActiveMedicationsByYoungPersonAsync(request.YoungPersonId!, cancellationToken);
    }
}

