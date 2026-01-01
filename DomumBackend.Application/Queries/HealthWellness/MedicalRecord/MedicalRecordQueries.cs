using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.MedicalRecord
{
    public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, MedicalRecordResponseDTO>
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public GetMedicalRecordByIdQueryHandler(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<MedicalRecordResponseDTO> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
        {
            return await _medicalRecordService.GetMedicalRecordByIdAsync(request.Id!, cancellationToken);
        }
    }

    public class GetMedicalRecordsByYoungPersonQuery : IRequest<List<MedicalRecordResponseDTO>>
    {
        public string? YoungPersonId { get; set; }

        public GetMedicalRecordsByYoungPersonQuery(string youngPersonId)
        {
            YoungPersonId = youngPersonId;
        }
    }

    public class GetMedicalRecordsByYoungPersonQueryHandler : IRequestHandler<GetMedicalRecordsByYoungPersonQuery, List<MedicalRecordResponseDTO>>
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public GetMedicalRecordsByYoungPersonQueryHandler(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<List<MedicalRecordResponseDTO>> Handle(GetMedicalRecordsByYoungPersonQuery request, CancellationToken cancellationToken)
        {
            return await _medicalRecordService.GetMedicalRecordsByYoungPersonAsync(request.YoungPersonId!, cancellationToken);
        }
    }

    public class GetMedicalRecordsByFacilityQuery : IRequest<List<MedicalRecordResponseDTO>>
    {
        public string? FacilityId { get; set; }

        public GetMedicalRecordsByFacilityQuery(string facilityId)
        {
            FacilityId = facilityId;
        }
    }

    public class GetMedicalRecordsByFacilityQueryHandler : IRequestHandler<GetMedicalRecordsByFacilityQuery, List<MedicalRecordResponseDTO>>
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public GetMedicalRecordsByFacilityQueryHandler(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<List<MedicalRecordResponseDTO>> Handle(GetMedicalRecordsByFacilityQuery request, CancellationToken cancellationToken)
        {
            return await _medicalRecordService.GetMedicalRecordsByFacilityAsync(request.FacilityId!, cancellationToken);
        }
    }
}

